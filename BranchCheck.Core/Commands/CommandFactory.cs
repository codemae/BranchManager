using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BranchCheck.Core
{
    public partial class GitConsole
    {
        private static class CommandFactory
        {
            public static ICommand CommandLookup(CommandType commandType,
                                                 Process consoleProcess,
                                                 string promptLine,
                                                 string remoteRepositoryName,
                                                 string branch)
            {
                var commandLookup = new Dictionary<CommandType, Func<ICommand>>
                {
                    {
                        CommandType.DeleteRemoteBranch,
                        () => { return new DeleteRemoteBranchCommand(consoleProcess,
                                                                     promptLine,
                                                                     remoteRepositoryName,
                                                                     branch); }
                    },
                    {
                        CommandType.DeleteLocalBranch,
                        () => { return new DeleteLocalBranchCommand(consoleProcess,
                                                                    promptLine,
                                                                    remoteRepositoryName,
                                                                    branch); }
                    },
                    {
                        CommandType.Prune,
                        () => { return new PruneCommand(consoleProcess, promptLine, remoteRepositoryName); }
                    }
                };


                Func<ICommand> command;
                if (!commandLookup.TryGetValue(commandType, out command))
                    throw new ArgumentException("Invalid command type");

                return command();
            }
        }
    }
}
