using System;
using System.Diagnostics;

namespace BranchCheck.Core
{
    public partial class GitConsole
    {
        private class DeleteRemoteBranchCommand : Command
        {
            private string Remote { get; set; }
            private string Branch { get; set; }

            private string user;
            private string server;

            public DeleteRemoteBranchCommand(Process consoleProcess, 
                                             string promptLine, 
                                             int timeout, 
                                             string remote, 
                                             string branch, 
                                             string user, 
                                             string server)
                : base(consoleProcess, promptLine, timeout)
            {
                Remote = remote;
                Branch = branch;
                this.user = user;
                this.server = server;
            }

            public override void Execute()
            {
                command = String.Format("git push {0} :{1}", Remote, Branch);
                consoleProcess.StandardInput.WriteLine(command);
                Wait(gitTimeout);
            }

            protected override void OnDataReceived(string data)
            {
                base.OnDataReceived(data);
                bool showPasswordPrompt = false;

                // custom error handling
                var testString = String.Format("{0}@{1}'s password:", user, server);
                if (data.Contains(testString))
                    showPasswordPrompt = true;
            }
        }
    }
}