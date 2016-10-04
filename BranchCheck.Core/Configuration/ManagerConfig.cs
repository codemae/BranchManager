using System;

namespace BranchCheck.Core.Configuration
{
    public class ManagerConfig
    {
        public string GitLocation { get; private set; }

        public string CmdLocation { get; private set; }

        public string RepoLocation { get; private set; }

        public string RemoteRepositoryName { get; private set; }

        public string User { get; private set; }

        public string YouTrackBaseURL { get; private set; }

        public int GitTimeout { get; private set; }

        public ManagerConfig(
            string gitLocation, 
            string cmdLocation, 
            string repoLocation, 
            string remoteRepositoryName,
            string user,
            string youTrackBaseURL,
            string gitTimeoutMilliseconds
            )
        {
            int timeout;
            if(!int.TryParse(gitTimeoutMilliseconds, out timeout))
            {
                throw new InvalidCastException("Invalid GitTimeoutMilliseconds in BranchManager.config.");
            }
            this.GitLocation = gitLocation;
            this.CmdLocation = cmdLocation;
            this.RepoLocation = repoLocation;
            this.RemoteRepositoryName = remoteRepositoryName;
            this.User = user;
            this.YouTrackBaseURL = youTrackBaseURL;
            this.GitTimeout = timeout;
        }
    }                             
}
