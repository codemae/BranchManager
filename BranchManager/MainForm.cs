using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using BranchCheck.Core;
using BranchCheck.Core.Configuration;
using BranchCheck.Core.GitConsole;

namespace BranchManager
{
    public partial class MainForm : Form
    {
        private ConfigFile configFile = null;
        private ManagerConfig managerConfig = null;
        private YouTrack youTrack = null;
        private Git git = null;
        private GitConsole gitConsole = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                configFile = new ConfigFile("Configuration/BranchManager.config");
                configFile.Read(out managerConfig);
                git = new Git(ref managerConfig);
                youTrack = new YouTrack(ref managerConfig);

                gitConsole = new GitConsole(ref managerConfig, git.Config);
                gitConsole.OutputReceived += Console_OutputReceived;
                gitConsole.ErrorMessageReceived += Console_ErrorMessageReceived;
                gitConsole.PasswordRequestReceived += Console_PasswordRequestReceived;

                gitConsole.Prune();
                
                branchList.Columns.Add("Name", 100);
                branchList.Columns.Add("Last Committed By", 200);
                branchList.Columns.Add("Committed Date", 100);
                branchList.Columns.Add("Has local branch", 200);

                var remoteBranches = git.RemoteBranches()
                                        .Where(x => x.Id.StartsWith("test"))
                                        //.Where(x => x.LastCommittedBy == managerConfig.user)
                                        .Where(x => !youTrack.IsInProgress(x))
                                        .Where(x => !x.IsHotfix && !x.IsRelease);

                var localBranches = git.LocalBranches();

                var linked = (from r in remoteBranches
                              join l in localBranches on r.Id equals l.Id into trackingBranches
                              select new
                              {
                                  Remote = r,
                                  Tracking = trackingBranches
                              }).ToList();

                foreach (var branch in linked)
                {
                    var item = branchList.Items.Add(branch.Remote.Id);
                    item.SubItems.Add(branch.Remote.LastCommittedBy);
                    item.SubItems.Add(branch.Remote.LastCommittedDate.ToString("dd/MM/yyyy"));
                    item.SubItems.Add(branch.Tracking.Any() ? "Yes" : "No");

                    item.Tag = branch.Remote;
                    item.ToolTipText = branch.Remote.Message;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void branchList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var item = branchList.GetItemAt(e.Location.X, e.Location.Y);

            if (item == null) return;

            var branch = item.Tag as CascadeBranch;

            if (branch.IdPrefix.Equals("CAS", StringComparison.CurrentCultureIgnoreCase)
                    || branch.IdPrefix.Equals("PY", StringComparison.CurrentCultureIgnoreCase)
                    || branch.IdPrefix.Equals("HR", StringComparison.CurrentCultureIgnoreCase))
            {
                var url = string.Format("http://{0}/issue/{1}", youTrack.BaseUrl, branch.Id);
                System.Diagnostics.Process.Start(url);
            }
        }

        private void localToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedItem = branchList.SelectedItems[0].Tag as CascadeBranch;
            gitConsole.DeleteBranch(ref selectedItem, GitConsole.DeleteType.Local);
        }

        private void remoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedItem = branchList.SelectedItems[0].Tag as CascadeBranch;
            gitConsole.DeleteBranch(ref selectedItem, GitConsole.DeleteType.Remote);
        }

        private void bothToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedItem = branchList.SelectedItems[0].Tag as CascadeBranch;
            gitConsole.DeleteBranch(ref selectedItem, GitConsole.DeleteType.Both);
        }

        private void Console_OutputReceived(object sender, ConsoleEventArgs e)
        {
            MethodInvoker action = delegate
            {
                txtOutput.AppendText(string.Format("{0}\r\n", e.Message));
            };

            this.BeginInvoke(action);
        }

        private void Console_ErrorMessageReceived(object sender, ConsoleEventArgs e)
        {
            MessageBox.Show(e.Message);
        }

        private void Console_PasswordRequestReceived(object sender, GitConsole.CommandEventArgs e)
        {
            MessageBox.Show(string.Format("Please enter {0}:{1}'s password: ", e.User, e.Server));

            e.Password = "test";
        }
    }
}