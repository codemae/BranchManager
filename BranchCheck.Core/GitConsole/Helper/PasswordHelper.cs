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
            private Process consoleProcess;
            private string promptLine;
            private string user;
            private string server;
            private string passwordPrompt;
            private bool success;

            public PasswordHelper(Process consoleProcess, string promptLine, string user, string server)
            {
                this.consoleProcess = consoleProcess;
                this.promptLine = promptLine;
                this.user = user;
                this.server = server;
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

                consoleProcess.StandardInput.WriteLine(password);
                consoleProcess.OutputDataReceived += ConsoleProcess_OutputDataReceived;

                return success;
            }

            private void OnDataReceived(string data)
            {
                if(success)
                    success = ContainsPasswordPrompt(data);

                if(data.Contains(promptLine))
                    Continue();
            }

            private void Wait()
            {
                waitingForConsole.WaitOne();
            }

            private void Continue()
            {
                waitingForConsole.Set();
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
