using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Visitors;
using PKExtFramework.Ext.Component;
using PKExtFramework.Ext.Component.Fields;
using PKExtFramework.Ext.Component.Panel;
using PKExtFramework.Ext.Component.Tab;
using PKExtFramework.Ext.Component.Grid;

namespace PKExtDesigner.Visitors
{
    internal class PKItemCanAddVisitor: PKVisitor
    {
        private PKBoxItem childItem;
        public bool CanAdd { get; set; }
        public PKItemCanAddVisitor(PKBoxItem childItem)
        {
            this.childItem = childItem;
            this.CanAdd = false;
        }

        private bool AreSameType<T>(PKBoxItem childItem)
        {
            return typeof(T) == childItem.GetType();
        }

        private void IsUserControl()
        {
            if (AreSameType<PKControl>(childItem))
            {
                if ((childItem as PKControl).IsComponent)
                {
                    this.CanAdd = true;
                }
            }
        }

        public override void Visit(PKControl item)
        {
            if (
                AreSameType<PKContainer>(childItem) ||
                AreSameType<PKTabPanel>(childItem) ||
                AreSameType<PKPanel>(childItem) ||
                AreSameType<PKGrid>(childItem))
            {
                this.CanAdd = true;
            }            
        }

        public override void Visit(PKContainer item)
        {
            if (AreSameType<PKContainer>(childItem) ||
                AreSameType<PKPanel>(childItem)||
                AreSameType<PKTabPanel>(childItem) ||
                AreSameType<PKGrid>(childItem) ||
                AreSameType<PKField>(childItem) ||
                AreSameType<PKButton>(childItem) ||
                AreSameType<PKText>(childItem))
            {
                this.CanAdd = true;
            }
            IsUserControl();
        }

        public override void Visit(PKPanelFooter item)
        {
            if (AreSameType<PKContainer>(childItem) ||
                AreSameType<PKField>(childItem) ||
                AreSameType<PKButton>(childItem) ||
                AreSameType<PKText>(childItem))
            {
                this.CanAdd = true;
            }
        }

        public override void Visit(PKBottombar item)
        {
            if (AreSameType<PKContainer>(childItem) ||
                AreSameType<PKField>(childItem) ||
                AreSameType<PKButton>(childItem) ||
                AreSameType<PKText>(childItem))
            {
                this.CanAdd = true;
            }
        }

        public override void Visit(PKToolbar item)
        {
            if (AreSameType<PKContainer>(childItem) ||
                AreSameType<PKField>(childItem) ||
                AreSameType<PKButton>(childItem) ||
                AreSameType<PKText>(childItem))
            {
                this.CanAdd = true;
            }
        }
        
        public override void Visit(PKGridColumnRow item)
        {
            if (AreSameType<PKColumn>(childItem))
            {   
                this.CanAdd = true;
            }
        }

        public override void Visit(PKGrid item)
        {
            if (AreSameType<PKColumn>(childItem))
            {
                this.CanAdd = true;
            }
        }

        public override void Visit(PKPanel item)
        {
            if (AreSameType<PKContainer>(childItem) ||
                AreSameType<PKPanel>(childItem) ||
                AreSameType<PKTabPanel>(childItem) ||
                AreSameType<PKGrid>(childItem) ||
                AreSameType<PKField>(childItem) ||
                AreSameType<PKButton>(childItem) ||
                AreSameType<PKText>(childItem))
            {
                this.CanAdd = true;
            }
            IsUserControl();
        }

        public override void Visit(PKTabPanel item)
        {
            if (AreSameType<PKTab>(childItem))
            {
                this.CanAdd = true;
            }
        }

        public override void Visit(PKTabBar item)
        {
            item.Parent.Accept(this);
        }

        public override void Visit(PKTabPanelBody item)
        {
            if (AreSameType<PKTab>(childItem))
            {
                this.CanAdd = true;
            }
        }

        public override void Visit(PKColumn item)
        {
            
        }

        public override void Visit(PKField item)
        {
            
        }

        public override void Visit(PKText item)
        {
            
        }

        public override void Visit(PKPanelHeader item)
        { 
        }

        public override void Visit(PKButton item)
        {

        }

        public override void Visit(PKFieldItem item)
        {
            
        }
    }
}
