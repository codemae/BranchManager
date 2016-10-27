using System;
using System.Diagnostics;
using System.Threading;

namespace BranchCheck.Core.GitConsole
{
    public partial class GitConsole : IDisposable
    {
        public class PasswordHelper
        {
            private EventWaitHandle waitingForConsole;
            private string passwordPrompt;
            private bool success;

            public string PromptLine { get; private set; }

            public string User { get; private set; }

            public string Server { get; private set; }

            public Process ConsoleProcess { get; private set; }

            public PasswordHelper(Process consoleProcess, string promptLine, string user, string server)
            {
                ConsoleProcess = consoleProcess;
                PromptLine = promptLine;
                User = user;
                Server = server;
                this.passwordPrompt = string.Format("{0}@{1}'s password:", user, server);
                waitingForConsole = new EventWaitHandle(false, EventResetMode.ManualReset);
            }

            public bool ContainsPasswordPrompt(string line)
            {
                return (line.Contains(passwordPrompt)) ? true : false;
            }

            public bool TryPassword(string password)
            {
                success = true;

                ConsoleProcess.StandardInput.WriteLine(password);
                ConsoleProcess.OutputDataReceived += ConsoleProcess_OutputDataReceived;

                return success;
            }

            private void Wait()
            {
                waitingForConsole.WaitOne();
            }

            private void Continue()
            {
                waitingForConsole.Set();
            }

            private void OnDataReceived(string data)
            {
                if (success)
                    success = ContainsPasswordPrompt(data);

                if (data.Contains(PromptLine))
                    Continue();
            }

            private void ConsoleProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
            {
                if (string.IsNullOrEmpty(e.Data))
                    return;

                OnDataReceived(e.Data);
            }
        }
    }
}
