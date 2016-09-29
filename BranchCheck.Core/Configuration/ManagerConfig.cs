using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BranchCheck.Core.Configuration
{
    public class ManagerConfig
    {
        public string gitLocation { get; private set; }

        public string cmdLocation { get; private set; }

        public string repoLocation { get; private set; }

        public string remoteRepositoryName { get; private set; }

        public string user { get; private set; }

        public string youTrackBaseURL { get; private set; }

        public ManagerConfig(
            string gitLocation, 
            string cmdLocation, 
            string repoLocation, 
            string remoteRepositoryName,
            string user,
            string youTrackBaseURL
            )
        {
            this.gitLocation = gitLocation;
            this.cmdLocation = cmdLocation;
            this.repoLocation = repoLocation;
            this.remoteRepositoryName = remoteRepositoryName;
            this.user = user;
            this.youTrackBaseURL = youTrackBaseURL;
        }
    }                             
}
