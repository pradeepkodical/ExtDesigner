using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PKExtFramework.Ext.Component;
using PKExtFramework.Ext.GDI;
using PKExtDesigner.Visitors;


namespace PKExtDesigner.Designer
{
    public partial class ExtDesignerUI : UserControl
    {
        public event Action<PKBoxItem> SelectedChange;
        public event Action<PKBoxItem> ItemAdded;
        public event Action ItemDeleted;
        public event Action ItemMoved;

        private PKBoxResizer resizer = new PKBoxResizer();
        private PKInfoBox infoBox = new PKInfoBox();
        private List<PKBoxItem> selectedItems = new List<PKBoxItem>();

        private PKControl appPage;

        public PKControl AppPage 
        {
            get
            {
                return appPage;
            }            
        }
        public ExtDesignerUI()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            this.Resize += new EventHandler(ExtDesigner_Resize);
            this.Click += new EventHandler(ExtDesigner_Click);

            this.resizer.AfterMouseMove += new Action(resizer_AfterMouseMove);
        }        

        private void resizer_AfterMouseMove()
        {
            this.DoLayout();
            this.Invalidate();
        }

        private void ExtDesigner_Click(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void ExtDesigner_Resize(object sender, EventArgs e)
        {
            this.ResizeApp();
        }

        private void ResizeApp()
        {
            if (this.appPage != null)
            {
                this.appPage.Left = 20;
                this.appPage.Top = 20;
                this.appPage.Width = this.Width - 40;
                this.appPage.Height = this.Height - 40;
                this.appPage.DoLayout();

                this.resizer.SetItem(SelectedItem);
                this.infoBox.SetPageSize(this.Width, this.Height);
            }
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            if (this.appPage != null)
            {
                this.appPage.Render(new PKGDIGraphics(e.Graphics));
                if (this.SelectedItem != null)
                {
                    this.SelectedItem.Render(new PKGDIGraphics(e.Graphics));
                }
                if (this.resizer.IsVisible)
                {
                    this.resizer.Render(e.Graphics);
                }
                this.infoBox.Render(e.Graphics);
            }            
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control && e.Button == MouseButtons.Left)
            {
                this.resizer.IsVisible = false;                
            }
            else
            {
                this.resizer.IsVisible = true;                
            }

            if (this.resizer.IsVisible)
            {
                if (e.Button == MouseButtons.Left)
                {
                    this.resizer.OnMouseDown(e.X, e.Y);
                }
                if (!this.resizer.MouseHit)
                {
                    this.SelectItem(this.AppPage.HitTest(e.X, e.Y), false);
                }
            }
            else
            {
                this.SelectItem(this.AppPage.HitTest(e.X, e.Y), true);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && this.resizer.IsVisible)
            {
                this.resizer.OnMouseUp(e.X, e.Y);
            }
            this.DoLayout();
            this.Invalidate();            
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && this.resizer.IsVisible)
            {
                this.resizer.OnMouseMove(e.X, e.Y);
                this.Invalidate();
            }
        }

        public void DeleteSelected()
        {
            if (this.SelectedItem != null)
            {
                this.selectedItems.ForEach(x => {
                    x.Remove();
                });                
                this.DoLayout();
                if (this.ItemDeleted != null)
                {
                    this.ItemDeleted();
                }
            }
        }

        public void DoLayout()
        {
            if (this.appPage != null)
            {
                this.appPage.DoLayout();
            }
        }

        public PKBoxItem SelectedItem 
        {
            get; private set;
        }

        public void SelectItem(PKBoxItem item, bool multiSelect)
        {
            if (!multiSelect)
            {
                this.selectedItems.ForEach(x => x.IsSelected = false);
                this.selectedItems.Clear();
            }
            if (item != null)
            {
                if (!this.selectedItems.Exists(x => x.ID == item.ID))
                {
                    this.selectedItems.Add(item);
                }
                item.IsSelected = true;
            }
            this.SelectedItem = item;

            if (this.SelectedChange != null && !multiSelect)
            {
                this.SelectedChange(item);
                if (item != null)
                {
                    item.Activate(item);
                }
            }

            this.resizer.SetItem(item);
            this.infoBox.SetItem(this.selectedItems.FirstOrDefault());
            this.Invalidate();
        }

        public void AddItem(PKBoxItem item)
        {
            if (this.SelectedItem != null)
            {
                var visitor = new PKItemAddVisitor(item);
                this.SelectedItem.Accept(visitor);
                if (visitor.Added)
                {
                    this.appPage.DoLayout();

                    if (this.ItemAdded != null)
                    {
                        this.ItemAdded(item);
                    }
                }
            }            
        }

        internal void MoveUp()
        {
            if (this.SelectedItem != null)
            {
                this.SelectedItem.MoveUp();
                BoxChanged(false);
            }
        }

        internal void MoveDown()
        {
            if (this.SelectedItem != null)
            {
                this.SelectedItem.MoveDown();
                BoxChanged(false);
            }
        }

        internal void IncreaseHeight()
        {
            if (this.SelectedItem != null)
            {
                var viz = new PKSizeModifyVisitor(-1, ((this.SelectedItem.Height / 5) + 1) * 5, 0);
                this.selectedItems.ForEach(x =>
                {
                    x.Accept(viz);
                });

                BoxChanged(true);
            }
        }

        internal void DecreaseHeight()
        {
            if (this.SelectedItem != null)
            {
                var viz = new PKSizeModifyVisitor(-1, ((this.SelectedItem.Height / 5) - 1) * 5, 0);
                this.selectedItems.ForEach(x =>
                {
                    x.Accept(viz);
                });

                BoxChanged(true);
            }
        }

        internal void DecreaseWidth()
        {
            if (this.SelectedItem != null)
            {
                var viz = new PKSizeModifyVisitor(((this.SelectedItem.Width / 5) - 1) * 5, -1, 0);
                this.selectedItems.ForEach(x =>
                {
                    x.Accept(viz);
                });

                BoxChanged(true);
            }
        }

        internal void IncreaseWidth()
        {
            if (this.SelectedItem != null)
            {
                var viz = new PKSizeModifyVisitor(((this.SelectedItem.Width / 5) + 1) * 5, -1, 0);
                this.selectedItems.ForEach(x =>
                {
                    x.Accept(viz);
                });

                BoxChanged(true);
            }
        }

        internal void MakeSameSizeFields()
        {            
            var fs = this.selectedItems.FirstOrDefault();
            if (fs != null)
            {
                var viz = new PKSizeModifyVisitor(fs.Width, fs.Height, fs.Flex);
                this.selectedItems.ForEach(x => {
                    x.Accept(viz);
                });

                BoxChanged(true);
            }
        }

        private void BoxChanged(bool flexReset)
        {
            if (flexReset)
            {
                this.SelectedItem.Flex = 0;
            }
            this.appPage.DoLayout();
            this.resizer.SetItem(this.SelectedItem);
            this.Invalidate();
            this.ItemMoved();
        }

        internal void SetAppPage(PKControl pkApp)
        {

            this.appPage = pkApp;
            this.ResizeApp();
            if (this.ItemAdded != null)
            {
                this.ItemAdded(appPage);
            }            
        }
    }

}

