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
        private class DeleteRemoteBranchCommand : Command
        {
            private string Remote { get; set; }
            private string Branch { get; set; }

            public DeleteRemoteBranchCommand(Process consoleProcess, string promptLine, string remote, string branch)
                : base(consoleProcess, promptLine)
            {
                Remote = remote;
                Branch = branch;
            }

            public override void Execute()
            {
                command = String.Format("git push {0} :{1}", Remote, Branch);
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