using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BranchCheck.Core.GitConsole
{
    public partial class GitConsole
    {
        private static class CommandFactory
        {
            public static ICommand CommandLookup(CommandType commandType,
                                                 Process consoleProcess,
                                                 string promptLine,
                                                 int timeout,
                                                 string user,
                                                 string server,
                                                 string remoteRepositoryName,
                                                 string branch)
            {
                var commandLookup = new Dictionary<CommandType, Func<ICommand>>
                {
                    {
                        CommandType.DeleteRemoteBranch,
                        () => { return new DeleteRemoteBranchCommand(consoleProcess,
                                                                     promptLine,
                                                                     timeout,
                                                                     user,
                                                                     server,
                                                                     remoteRepositoryName,
                                                                     branch); }
                    },
                    {
                        CommandType.DeleteLocalBranch,
                        () => { return new DeleteLocalBranchCommand(consoleProcess,
                                                                    promptLine,
                                                                    timeout,
                                                                    user,
                                                                    server,
                                                                    remoteRepositoryName,
                                                                    branch); }
                    },
                    {
                        CommandType.Prune,
                        () => { return new PruneCommand(consoleProcess, 
                                                        promptLine,
                                                        timeout,
                                                        user,
                                                        server,
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
