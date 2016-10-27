using System;
using System.Diagnostics;
using System.Threading;

namespace BranchCheck.Core.GitConsole
{
    public partial class GitConsole
    {
        public abstract class Command : ICommand, IDisposable
        {
            private EventWaitHandle waitingForConsole;
            protected PasswordHelper passwordHelper = null;
            protected Process consoleProcess = null;
            protected string promptLine = string.Empty;
            protected string command = string.Empty;
            protected string user = string.Empty;
            protected string server = string.Empty;
            protected int gitTimeout;

            public event EventHandler<CommandEventArgs> PasswordRequestReceived;
            public event EventHandler<EventArgs> AbortCommand;

            public string ErrorMessages { get; protected set; }

            public string Messages { get; protected set; }

            public Command(Process consoleProcess, string promptLine, int timeout, string user, string server)
            {
                if (consoleProcess == null || consoleProcess.HasExited) throw new ArgumentNullException("consoleProcess");
                if (string.IsNullOrEmpty(promptLine)) throw new ArgumentNullException("promptLine");
                if (string.IsNullOrEmpty(user)) throw new ArgumentNullException("user");
                if (string.IsNullOrEmpty(server)) throw new ArgumentNullException("server");

                this.consoleProcess = consoleProcess;
                this.promptLine = promptLine;
                this.Messages = string.Empty;
                this.ErrorMessages = string.Empty;
                this.gitTimeout = timeout;
                consoleProcess.OutputDataReceived += ConsoleProcess_OutputDataReceived;
                consoleProcess.ErrorDataReceived += ConsoleProcess_OutputDataReceived;
                waitingForConsole = new EventWaitHandle(false, EventResetMode.ManualReset);
                var passwordHelper = new PasswordHelper(consoleProcess, promptLine, user, server);
            }

            public void Dispose()
            {
                consoleProcess.OutputDataReceived -= ConsoleProcess_OutputDataReceived;
                consoleProcess.ErrorDataReceived -= ConsoleProcess_OutputDataReceived;
            }

            public abstract void Execute();

            protected virtual void OnDataReceived(string data)
            {
                if (data == promptLine)
                {
                    Continue();
                    return;
                }

                if (data.ToLower().Contains("error"))
                    ErrorMessages += string.Format("{0}\r\n", data);

                Messages += string.Format("{0}\r\n", data);
            }

            protected virtual void OnPasswordRequestReceived(string user, string server)
            {
                var e = new CommandEventArgs(user, server);
                bool passwordSuccess = false;

                do {
                    PasswordRequestReceived?.Invoke(this, e);
                    passwordSuccess = passwordHelper.TryPassword(e.Password);
                }
                while (passwordSuccess == false && e.Abort == false);

                if (e.Abort)
                    AbortCommand?.Invoke(this, new EventArgs("Failed password"));
                else
                    Execute();
            }

            protected virtual void OnAbortRequestReceived(string message)
            {

            }

            protected void Wait(int timeout = 0)
            {
                waitingForConsole.WaitOne(timeout);
            }

            protected void Continue()
            {
                waitingForConsole.Set();
            }

            private void ConsoleProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
            {
                // code here to deal with messages returned
                if (string.IsNullOrEmpty(e.Data))
                    return;

                OnDataReceived(e.Data);
            }
        }
    }
}