using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using PKExtFramework.Ext.Component;
using PKExtFramework.Persistance;

namespace PKExtDesigner
{
    public partial class UserComponentsUI : Form
    {
        public event Action<PKControl> ControlSelected;
        public UserComponentsUI()
        {
            InitializeComponent();
        }

        private void UserComponentsUI_Load(object sender, EventArgs e)
        {
            LoadComponentsAsync();
        }

        private void LoadComponentsAsync()
        {
            BackgroundWorker bWorker = new BackgroundWorker();
            bWorker.DoWork += new DoWorkEventHandler(bWorker_DoWork);
            bWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bWorker_RunWorkerCompleted);
            bWorker.RunWorkerAsync();
        }

        void bWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var userControls = e.Result as List<PKControl>;
            userControls.ForEach(x=>{
                this.listView1.Items.Add(new ListViewItem { 
                    Text = x.Name
                }).Tag = x;
            });            
        }

        void bWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var userControls = new List<PKControl>();
            var list = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "Components"));
            list.ToList().ForEach(file=>{
                string str = File.ReadAllText(file);
                var control = PKStorage.DeserializeComponent(str);                
                control.ComponentFileName = Path.GetFileName(file);
                userControls.Add(control);
            });
            e.Result = userControls;
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.ControlSelected != null && listView1.SelectedItems.Count > 0)
            {
                var selectedControl = listView1.SelectedItems[0].Tag as PKControl;                
                var copyControl = PKStorage.Deserialize(PKStorage.Serialize(selectedControl)) as PKControl;
                copyControl.IsComponent = true;
                copyControl.ComponentFileName = selectedControl.ComponentFileName;
                this.ControlSelected(copyControl);
            }
        }
    }
}
