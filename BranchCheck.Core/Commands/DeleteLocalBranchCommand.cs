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
        private class DeleteLocalBranchCommand : Command
        {
            private string Remote { get; set; }
            private string Branch { get; set; }

            public DeleteLocalBranchCommand(Process consoleProcess, string promptLine, string remote, string branch)
                : base(consoleProcess, promptLine)
            {
                Remote = remote;
                Branch = branch;
            }

            public override void Execute()
            {
                command = String.Format("git branch -D {0}", Branch);
                consoleProcess.StandardInput.WriteLine(command);
                Wait();
            }

            protected override void OnDataReceived(string data)
            {
                base.OnDataReceived(data);

                // custom error handling
            }
        }
    }
}
