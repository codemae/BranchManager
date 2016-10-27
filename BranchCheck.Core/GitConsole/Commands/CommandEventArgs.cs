using System;

namespace BranchCheck.Core.GitConsole
{
    public partial class GitConsole
    {
        public class CommandEventArgs : EventArgs
        {
            public string User { get; private set; }

            public string Server { get; private set; }
            
            public string Password { get; set; }

            public bool Abort { get; set; }
           
            public CommandEventArgs(string user, string server)
            {
                Abort = false;
                User = user;
                Server = server;
            }
        }
    }
}
