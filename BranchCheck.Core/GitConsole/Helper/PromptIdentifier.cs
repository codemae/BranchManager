using System;
using System.Diagnostics;
using System.Threading;

namespace BranchCheck.Core.GitConsole
{
    public partial class GitConsole : IDisposable
    {
        private class PromptIdentifier : IDisposable
        {
            private EventWaitHandle waitingForConsole;
            private string previousLine;
            private bool lineFound = false;
            private Process consoleProcess = null;

            public PromptIdentifier(Process process)
            {
                if (process == null) throw new ArgumentException("process");
                consoleProcess = process;

                consoleProcess.OutputDataReceived += OutputReceived;
                consoleProcess.ErrorDataReceived += OutputReceived;
                waitingForConsole = new EventWaitHandle(false, EventResetMode.ManualReset);
            }

            public string GetPromptLine()
            {
                consoleProcess.StandardInput.WriteLine("RandomCommand");
                waitingForConsole.WaitOne();

                return previousLine;
            }

            private void OutputReceived(object sender, DataReceivedEventArgs e)
            {
                if (!lineFound && e.Data != "$ RandomCommand")
                {
                    previousLine = e.Data;
                    return;
                }

                lineFound = true;
                waitingForConsole.Set();
            }

            public void Dispose()
            {
                consoleProcess.OutputDataReceived -= OutputReceived;
                consoleProcess.ErrorDataReceived -= OutputReceived;
            }
        }
    }
}
