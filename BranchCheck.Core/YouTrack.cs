using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BranchCheck.Core.Configuration;

namespace BranchCheck.Core
{
    public class YouTrack
    {
        private class Issue
        {
            public string Id { get; private set; }

            public Issue(string id)
            {
                Id = id;
            }
        }

        private readonly string baseUrl = "youtrack:9111";

        private List<Issue> InProgressIssues = new List<Issue>();

        public string BaseUrl { get { return baseUrl; } }

        public YouTrack(ref ManagerConfig managerConfig)
        {
            this.baseUrl = managerConfig.YouTrackBaseURL;

            using (var client = new WebClient())
            {
                var addr = string.Format("http://{0}/rest/issue?filter=State%3A{{In+Progress}}&with=&max=1000", baseUrl);

                var response = client.DownloadString(addr);
                var xDoc = XDocument.Parse(response);

                InProgressIssues = (from e in xDoc.Descendants("issue")
                                    select new Issue(e.Attribute("id").Value)).ToList();
            }
        }

        public bool IsInProgress(CascadeBranch branch)
        {
            if (branch == null) throw new ArgumentNullException("branch");

            return InProgressIssues.Exists(x => string.Equals(x.Id, branch.Id, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
