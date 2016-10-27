using System;
using System.Diagnostics;

namespace BranchCheck.Core.GitConsole
{
    public partial class GitConsole
    {
        public class DeleteRemoteBranchCommand : Command
        {
            private string Remote { get; set; }
            private string Branch { get; set; }

            public DeleteRemoteBranchCommand(Process consoleProcess, 
                                             string promptLine, 
                                             int timeout,
                                             string user,
                                             string server,
                                             string remote, 
                                             string branch)
                : base(consoleProcess, promptLine, timeout, user, server)
            {
                Remote = remote;
                Branch = branch;
            }

            public override void Execute()
            {
                command = string.Format("git push {0} :{1}", Remote, Branch);
                consoleProcess.StandardInput.WriteLine(command);
                Wait(gitTimeout);
            }

            protected override void OnDataReceived(string data)
            {
                base.OnDataReceived(data);

                // custom error handling
                if(passwordHelper.ContainsPasswordPrompt(data))
                    OnPasswordRequestReceived(user, server);
            }
        }
    }
}