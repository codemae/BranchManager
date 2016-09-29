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
        private class PruneCommand : Command
        {
            private string Remote { get; set; }

            public PruneCommand(Process consoleProcess, string promptLine, string remote) 
                : base(consoleProcess, promptLine)
            {
                Remote = remote;
            }

            public override void Execute()
            {
                command = String.Format("git remote prune {0}", Remote);
                consoleProcess.StandardInput.WriteLine(command);
                Wait();
            }
        }
    }
}
