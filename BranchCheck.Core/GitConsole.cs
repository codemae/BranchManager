using System;
using System.Diagnostics;
using BranchCheck.Core.Configuration;

namespace BranchCheck.Core.GitConsole
{
    public partial class GitConsole : IDisposable
    {
        private readonly string repositoryPath;
        private readonly string gitPath;
        private readonly string remoteRepositoryName;
        private Process consoleProcess = null;
        private string promptLine = null;
        private int timeout;
        private string user;
        private string server;

        public event EventHandler<ConsoleEventArgs> OutputReceived;
        public event EventHandler<ConsoleEventArgs> ErrorMessageReceived;
        public event EventHandler<CommandEventArgs> PasswordRequestReceived;

        public enum DeleteType
        {
            Remote,
            Local,
            Both
        }
        private enum CommandType
        {
            DeleteRemoteBranch,
            DeleteLocalBranch,
            Prune
        }

        public GitConsole(ref ManagerConfig managerConfig, GitConfig gitConfig)
        {
            if (managerConfig == null) throw new ArgumentNullException("managerConfig");
            if (gitConfig == null) throw new ArgumentNullException("gitConfig");

            repositoryPath = managerConfig.RepoLocation;
            gitPath = managerConfig.GitLocation;
            remoteRepositoryName = managerConfig.RemoteRepositoryName;
            timeout = managerConfig.GitTimeout;
            user = gitConfig.User;
            server = gitConfig.Server;
            remoteRepositoryName = gitConfig.RepositoryName;

            StartConsoleIfNotExists();
        }

        public void Dispose()
        {
            if (consoleProcess == null)
                return;

            consoleProcess.Dispose();
        }

        public void DeleteBranch(ref CascadeBranch branch, DeleteType deleteType)
        {
            if (branch == null) throw new ArgumentNullException("branch");

            string messages = string.Empty;
            string errors = string.Empty;

            if (deleteType != DeleteType.Local)
                ConsoleCommand(CommandType.DeleteRemoteBranch, ref messages, ref errors, branch.FriendlyName);

            if (deleteType != DeleteType.Remote)
                ConsoleCommand(CommandType.DeleteLocalBranch, ref messages, ref errors, branch.FriendlyName);

            if (errors != string.Empty)
                OnErrorMessageReceived(errors);
        }

        public void Prune()
        {
            string messages = string.Empty;
            string errors = string.Empty;
            ConsoleCommand(CommandType.Prune, ref messages, ref errors);

            if (errors != string.Empty)
                OnErrorMessageReceived(errors);
        }

        private void ConsoleCommand(CommandType commandType, ref string messages, ref string errorMessages, string branch = null)
        {
            StartConsoleIfNotExists();
            var currentCommand = CommandFactory.CommandLookup(commandType,
                                                              consoleProcess,
                                                              promptLine,
                                                              timeout,
                                                              remoteRepositoryName,
                                                              branch,
                                                              user,
                                                              server);

            currentCommand.Execute();

            currentCommand.PasswordRequestReceived += Command_PasswordRequestReceived;

            messages += currentCommand.Messages;
            errorMessages += currentCommand.ErrorMessages;
        }

        private void StartConsoleIfNotExists()
        {
            if (consoleProcess != null && !consoleProcess.HasExited)
                return;

            var info = new ProcessStartInfo
            {
                FileName = gitPath,
                Arguments = "--login -i",
                WorkingDirectory = repositoryPath,
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            consoleProcess = new Process { StartInfo = info };
            var promptIdentifier = new PromptIdentifier(consoleProcess);

            consoleProcess.OutputDataReceived += ConsoleProcess_OutputDataReceived;
            consoleProcess.ErrorDataReceived += ConsoleProcess_OutputDataReceived;

            consoleProcess.Start();
            consoleProcess.BeginOutputReadLine();
            consoleProcess.BeginErrorReadLine();

            promptLine = promptIdentifier.GetLine();
        }

        private void ConsoleProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            // code here to deal with messages returned
            if (string.IsNullOrEmpty(e.Data))
                return;

            OnOutputReceived(e.Data);
        }

        private void OnOutputReceived(string data)
        {
            OutputReceived?.Invoke(this, new ConsoleEventArgs(data));
        }

        private void OnErrorMessageReceived(string data)
        {
            ErrorMessageReceived?.Invoke(this, new ConsoleEventArgs(data));
        }

        private void Command_PasswordRequestReceived(object sender, CommandEventArgs e)
        {
            OnPasswordRequestReceived(e);
        }

        private void OnPasswordRequestReceived(CommandEventArgs e)
        {
            PasswordRequestReceived?.Invoke(this, e);
        }
    }
}
