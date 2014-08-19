using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Visitors;
using PKExtFramework.Ext.Component;
using PKExtFramework.Ext.Component.Fields;
using PKExtFramework.Ext.Component.Panel;
using PKExtFramework.Ext.Component.Grid;
using PKExtFramework.Ext.Component.Tab;

namespace PKExtDesigner.Visitors
{
    internal class PKItemAddVisitor: PKVisitor
    {
        private PKBoxItem newItem;
        private PKItemCanAddVisitor canAddVisitor;
        public bool Added { get; set; }
        public PKItemAddVisitor(PKBoxItem newItem)
        {
            this.newItem = newItem;
            this.Added = false;            
        }

        public override void Visit(PKContainer item)
        {
            canAddVisitor = new PKItemCanAddVisitor(newItem);
            item.Accept(canAddVisitor);
            if (canAddVisitor.CanAdd)
            {
                item.Add(newItem);
                this.Added = true;
            }
        }

        public override void Visit(PKBottombar item)
        {
            Visit(item as PKContainer);
        }

        public override void Visit(PKToolbar item)
        {
            Visit(item as PKContainer);
        }

        public override void Visit(PKPanelFooter item)
        {
            Visit(item as PKContainer);
        }
                
        public override void Visit(PKControl item)
        {
            canAddVisitor = new PKItemCanAddVisitor(newItem);
            item.Accept(canAddVisitor);
            if (canAddVisitor.CanAdd)
            {
                if (newItem is PKControl)
                {
                    (newItem as PKControl).Items.ToList().ForEach(x =>
                    {
                        item.Add(x);
                    });
                }
                else
                {
                    item.Add(newItem);
                }
                this.Added = true;
            }
        }
        
        public override void Visit(PKGridColumnRow item)
        {
            canAddVisitor = new PKItemCanAddVisitor(newItem);
            item.Accept(canAddVisitor);
            if (canAddVisitor.CanAdd)
            {
                item.Add(newItem as PKColumn);
                this.Added = true;
            }
        }

        public override void Visit(PKGrid item)
        {
            canAddVisitor = new PKItemCanAddVisitor(newItem);
            item.Accept(canAddVisitor);
            if (canAddVisitor.CanAdd)
            {
                item.ColumnRow.Add(newItem as PKColumn);
                this.Added = true;
            }
        }

        public override void Visit(PKTabPanel item)
        {
            canAddVisitor = new PKItemCanAddVisitor(newItem);
            item.Accept(canAddVisitor);
            if (canAddVisitor.CanAdd)
            {
                item.Body.Add(newItem);
                this.Added = true;
            }
        }

        public override void Visit(PKTabBar item)
        {
            item.Parent.Accept(this);
        }

        public override void Visit(PKTabPanelBody item)
        {
            canAddVisitor = new PKItemCanAddVisitor(newItem);
            item.Accept(canAddVisitor);
            if (canAddVisitor.CanAdd)
            {
                item.Add(newItem);
                this.Added = true;
            }
        }

        public override void Visit(PKPanel item)
        {
            canAddVisitor = new PKItemCanAddVisitor(newItem);
            item.Accept(canAddVisitor);
            if (canAddVisitor.CanAdd)
            {
                item.Body.Add(newItem);
                this.Added = true;
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
