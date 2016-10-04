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
                                                 int timeout,
                                                 string remoteRepositoryName,
                                                 string branch,
                                                 string user,
                                                 string server)
            {
                var commandLookup = new Dictionary<CommandType, Func<ICommand>>
                {
                    {
                        CommandType.DeleteRemoteBranch,
                        () => { return new DeleteRemoteBranchCommand(consoleProcess,
                                                                     promptLine,
                                                                     timeout,
                                                                     remoteRepositoryName,
                                                                     branch,
                                                                     user,
                                                                     server); }
                    },
                    {
                        CommandType.DeleteLocalBranch,
                        () => { return new DeleteLocalBranchCommand(consoleProcess,
                                                                    promptLine,
                                                                    timeout,
                                                                    remoteRepositoryName,
                                                                    branch); }
                    },
                    {
                        CommandType.Prune,
                        () => { return new PruneCommand(consoleProcess, 
                                                        promptLine,
                                                        timeout,
                                                        remoteRepositoryName); }
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
