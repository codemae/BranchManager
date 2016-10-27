using System;

namespace BranchCheck.Core.GitConsole
{
    public class ConsoleEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public ConsoleEventArgs(string message)
        {
            Message = message;
        }
    }
}
