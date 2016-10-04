namespace BranchCheck.Core
{
    public partial class GitConsole
    {
        private interface ICommand
        {
            string ErrorMessages { get; }

            string Messages { get; }

            void Execute();
        }
    }
}
