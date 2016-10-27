using System;

namespace BranchCheck.Core.GitConsole
{
    public partial class GitConsole
    {
        private interface ICommand
        {
            string ErrorMessages { get; }

            string Messages { get; }

            void Execute();

            event EventHandler<CommandEventArgs> PasswordRequestReceived;
            event EventHandler<EventArgs> AbortCommand;
        }
    }
}
