using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BranchCheck.Core.Configuration;
using LibGit2Sharp;

namespace BranchCheck.Core
{
    public class Git
    {
        private readonly string repositoryPath;
        private readonly Repository repo;

        public Git(ref ManagerConfig managerConfig)
        {
            repositoryPath = managerConfig.repoLocation;
            repo = new Repository(repositoryPath);
        }

        public List<CascadeBranch> RemoteBranches()
        {
            var branches = repo.Branches;
            return (from b in branches
                    where b.IsRemote
                    select new CascadeBranch(b)).OrderBy(x => x.LastCommittedDate).ToList();
        }

        public List<CascadeBranch> LocalBranches()
        {
            var branches = repo.Branches;
            return (from b in branches
                    where !b.IsRemote
                    select new CascadeBranch(b)).OrderBy(x => x.LastCommittedDate).ToList();
        }
    }
}
