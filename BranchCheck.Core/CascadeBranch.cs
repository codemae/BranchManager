using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;

namespace BranchCheck.Core
{
    public class CascadeBranch
    {
        public string FriendlyName { get; private set; }

        public string Id
        {
            get
            {
                return this.FriendlyName.Split('/').Last();
            }
        }

        public string Message { get; private set; }

        public string LastCommittedBy { get; private set; }

        public DateTime LastCommittedDate { get; private set; }

        public string IdPrefix
        {
            get
            {
                return Id.Split('-').First();
            }
        }

        public bool IsHotfix
        {
            get
            {
                return string.Equals(IdPrefix, "hotfix", StringComparison.CurrentCultureIgnoreCase);
            }
        }

        public bool IsRelease
        {
            get
            {
                return IdPrefix.StartsWith("release", StringComparison.CurrentCultureIgnoreCase);
            }
        }

        public CascadeBranch(Branch branch)
        {
            if (branch == null) throw new ArgumentNullException("branch");

            this.FriendlyName = branch.FriendlyName;
            this.Message = branch.Tip.Message;
            this.LastCommittedBy = branch.Tip.Committer.Name;
            this.LastCommittedDate = branch.Tip.Committer.When.Date;
        }

        public override string ToString()
        {
            return string.Format("{0} by {1} on {2:dd/MM/yyyy}", Id, LastCommittedBy, LastCommittedDate);
        }
    }
}
