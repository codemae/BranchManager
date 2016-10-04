using System;
using System.Diagnostics;

namespace BranchCheck.Core
{
    public partial class GitConsole
    {
        private class PruneCommand : Command
        {
            private string Remote { get; set; }

            public PruneCommand(Process consoleProcess, string promptLine, int timeout, string remote) 
                : base(consoleProcess, promptLine, timeout)
            {
                Remote = remote;
            }

            public override void Execute()
            {
                command = String.Format("git remote prune {0}", Remote);
                consoleProcess.StandardInput.WriteLine(command);
                Wait(gitTimeout);
            }
        }
    }
}