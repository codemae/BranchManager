using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
