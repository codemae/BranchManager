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
        
        public GitConfig Config { get; private set; }

        public Git(ref ManagerConfig managerConfig)
        {
            repositoryPath = managerConfig.RepoLocation;
            repo = new Repository(repositoryPath);
            Config = new GitConfig(repo);
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

    public class GitConfig
    {
        public string RepositoryName { get; private set; }

        public string User { get; private set; }

        public string Server { get; private set; }

        public GitConfig(Repository repo)
        {
            if (repo == null) throw new ArgumentNullException("repo");

            var config = repo.Config;

            RepositoryName = config.Get<string>("branch.master.remote").Value;

            var item = config.Get<string>(string.Format("remote.{0}.url", RepositoryName)).Value;
            char[] delimiters = { '@', ':' };
            string[] items = item.Split(delimiters);

            User = items[0];
            Server = items[1];
        }
    }
}
