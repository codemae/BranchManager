using System;
using System.Diagnostics;

namespace BranchCheck.Core
{
    public partial class GitConsole
    {
        private class DeleteLocalBranchCommand : Command
        {
            private string Remote { get; set; }
            private string Branch { get; set; }

            public DeleteLocalBranchCommand(Process consoleProcess, 
                                            string promptLine, 
                                            int timeout, 
                                            string remote, 
                                            string branch)
                : base(consoleProcess, promptLine, timeout)
            {
                Remote = remote;
                Branch = branch;
            }

            public override void Execute()
            {
                command = String.Format("git branch -D {0}", Branch);
                consoleProcess.StandardInput.WriteLine(command);
                Wait(gitTimeout);
            }

            protected override void OnDataReceived(string data)
            {
                base.OnDataReceived(data);

                // custom error handling
            }
        }
    }
}
