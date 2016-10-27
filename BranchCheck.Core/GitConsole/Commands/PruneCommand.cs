using System;
using System.Diagnostics;

namespace BranchCheck.Core.GitConsole
{
    public partial class GitConsole
    {
        public class PruneCommand : Command
        {
            private string Remote { get; set; }

            public PruneCommand(Process consoleProcess, string promptLine, int timeout, string remote, string user, string server) 
                : base(consoleProcess, promptLine, timeout, user, server)
            {
                Remote = remote;
            }

            public override void Execute()
            {
                command = string.Format("git remote prune {0}", Remote);
                consoleProcess.StandardInput.WriteLine(command);
                Wait(gitTimeout);
            }
        }
    }
}