using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BranchCheck.Core
{
    public partial class GitConsole
    {
        private abstract class Command : ICommand, IDisposable
        {
            private EventWaitHandle waitingForConsole;
            protected Process consoleProcess = null;
            protected string promptLine = String.Empty;
            protected string command = String.Empty;

            public string ErrorMessages { get; protected set; }

            public string Messages { get; protected set; }

            public Command(Process consoleProcess, string promptLine)
            {
                if (consoleProcess == null || consoleProcess.HasExited) throw new ArgumentNullException("consoleProcess");
                if (promptLine == null) throw new ArgumentNullException("promptLine");

                this.consoleProcess = consoleProcess;
                this.promptLine = promptLine;
                this.Messages = String.Empty;
                this.ErrorMessages = String.Empty;
                consoleProcess.OutputDataReceived += ConsoleProcess_OutputDataReceived;
                consoleProcess.ErrorDataReceived += ConsoleProcess_OutputDataReceived;
                waitingForConsole = new EventWaitHandle(false, EventResetMode.ManualReset);
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
                    ErrorMessages += String.Format("{0}\r\n", data);

                Messages += String.Format("{0}\r\n", data);
            }

            protected void Wait()
            {
                waitingForConsole.WaitOne();
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