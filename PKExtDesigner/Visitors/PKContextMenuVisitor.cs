using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Visitors;
using System.Windows.Forms;
using PKExtFramework.Ext.Component;
using PKExtFramework.Ext.Component.Fields;
using PKExtFramework.Ext.Component.Panel;
using PKExtFramework.Ext.Component.Tab;
using PKExtFramework.Ext.Component.Grid;

namespace PKExtDesigner.Visitors
{
    internal class PKContextMenuVisitor: PKVisitor
    {
        private ContextMenuStrip contextMenuStrip;
        private Control control;
        private int x;
        private int y;

        public PKContextMenuVisitor(ContextMenuStrip contextMenuStrip, 
            Control control,
            int x, int y)
        {
            this.contextMenuStrip = contextMenuStrip;
            this.control = control;
            this.x = x;
            this.y = y;
            ShowAll();
        }

        private void SetVisible(string mnuName, bool visible)
        {
            if (contextMenuStrip.Items.ContainsKey(mnuName))
            {
                contextMenuStrip.Items[mnuName].Visible = visible;
            }
        }

        private void ShowAll()
        {
            for (int i = 0; i < contextMenuStrip.Items.Count; i++ )
            {
                contextMenuStrip.Items[i].Visible = true;
            }
        }
        
        private void UpdateFromBase(PKBoxItem item)
        {
            for (int i = 0; i < contextMenuStrip.Items.Count; i++)
            {
                var box = contextMenuStrip.Items[i].Tag as PKBoxItem;
                if(box != null)
                {
                    PKItemCanAddVisitor visitor = new PKItemCanAddVisitor(box);
                    item.Accept(visitor);
                    contextMenuStrip.Items[i].Visible = visitor.CanAdd;
                }
            }
        }

        public override void Visit(PKContainer item)
        {
            UpdateFromBase(item);
            contextMenuStrip.Show(control, x, y);
        }

        public override void Visit(PKPanelFooter item)
        {
            UpdateFromBase(item);
            contextMenuStrip.Show(control, x, y);
        }

        public override void Visit(PKBottombar item)
        {
            UpdateFromBase(item);
            contextMenuStrip.Show(control, x, y);
        }

        public override void Visit(PKToolbar item)
        {
            UpdateFromBase(item);
            contextMenuStrip.Show(control, x, y);
        }

        public override void Visit(PKTabPanel item)
        {
            UpdateFromBase(item);

            SetVisible("mnuPaste", false);

            contextMenuStrip.Show(control, x, y);
        }

        public override void Visit(PKTabBar item)
        {
            UpdateFromBase(item);

            SetVisible("mnuPaste", false);

            contextMenuStrip.Show(control, x, y);
        }

        public override void Visit(PKTabPanelBody item)
        {
            UpdateFromBase(item);

            SetVisible("mnuPaste", false);

            contextMenuStrip.Show(control, x, y);
        }

        public override void Visit(PKPanel item)
        {
            UpdateFromBase(item);
            
            SetVisible("mnuPaste", false);

            contextMenuStrip.Show(control, x, y);
        }
        
        public override void Visit(PKControl item)
        {
            UpdateFromBase(item);

            SetVisible("mnuCut", false);
            SetVisible("mnuDelete", false);
            contextMenuStrip.Show(control, x, y);
        }

        public override void Visit(PKField item)
        {
            UpdateFromBase(item);

            SetVisible("mnuPaste", false);

            contextMenuStrip.Show(control, x, y);
        }

        public override void Visit(PKGrid item)
        {
            UpdateFromBase(item);

            SetVisible("mnuPaste", false);

            contextMenuStrip.Show(control, x, y);
        }

        public override void Visit(PKGridColumnRow item)
        {
            UpdateFromBase(item);

            contextMenuStrip.Show(control, x, y);
        }

        public override void Visit(PKText item)
        {
            UpdateFromBase(item);

            SetVisible("mnuPaste", false);

            contextMenuStrip.Show(control, x, y);
        }

        public override void Visit(PKPanelHeader item)
        {
            UpdateFromBase(item);

            SetVisible("mnuPaste", false);

            contextMenuStrip.Show(control, x, y);
        }

        public override void Visit(PKButton item)
        {
            UpdateFromBase(item);

            SetVisible("mnuPaste", false);

            contextMenuStrip.Show(control, x, y);
        }

        public override void Visit(PKColumn item)
        {
            UpdateFromBase(item);

            SetVisible("mnuPaste", false);

            contextMenuStrip.Show(control, x, y);
        }

        public override void Visit(PKFieldItem item)
        {
            UpdateFromBase(item); 
            
            SetVisible("mnuPaste", false);

            contextMenuStrip.Show(control, x, y);
        }
    }
}
