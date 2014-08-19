using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PKExtFramework.Ext.Component;
using PKExtFramework.Persistance;
using PKExtFramework.Ext.Drawing;
using PKExtFramework.Ext.Component.Fields;
using PKExtFramework.Ext.Component.Panel;
using PKExtFramework.Ext.Component.Tab;
using PKExtFramework.Ext.Component.Grid;

namespace PKExtFramework.Visitors
{
    internal class PKFlatElementCreateVisitor: PKVisitor
    {
        public List<PKFlatItem> FlatItems{ get; private set; }

        public PKFlatElementCreateVisitor()
        {
            this.FlatItems = new List<PKFlatItem>();
        }

        public override void Visit(PKControl item)
        {
            if (item.IsComponent)
            {
                PKFlatItem flatItem = new PKFlatItem();
                this.FlatItems.Add(flatItem);
                UpdateFromBase(item, flatItem);
                flatItem.Text = item.Text;
                flatItem.IsComponent = true;
                flatItem.ComponentFileName = item.ComponentFileName;
            }
            else
            {
                Visit(item as PKContainer);
            }
        }

        public override void Visit(PKContainer item)
        {
            PKFlatItem flatItem = new PKFlatItem();
            this.FlatItems.Add(flatItem);
            UpdateFromBase(item, flatItem);
            flatItem.Text = item.Text;            
            item.Items.ToList().ForEach(x => {
                x.Accept(this);
            });
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
        
        public override void Visit(PKTabPanel item)
        {
            PKFlatItem flatItem = new PKFlatItem();
            this.FlatItems.Add(flatItem);
            UpdateFromBase(item, flatItem);
                        
            item.Body.Accept(this);            
        }

        public override void Visit(PKTabBar item)
        {
            //need not persist
        }

        public override void Visit(PKTabPanelBody item)
        {
            Visit(item as PKContainer);       
        }

        public override void Visit(PKPanel item)
        {
            PKFlatItem flatItem = new PKFlatItem();
            this.FlatItems.Add(flatItem);
            UpdateFromBase(item, flatItem);

            item.Header.Accept(this);
            item.Toolbar.Accept(this);
            item.Body.Accept(this);
            item.Footer.Accept(this);            
        }
                
        public override void Visit(PKGrid item)
        {
            PKFlatItem flatItem = new PKFlatItem();
            this.FlatItems.Add(flatItem);

            UpdateFromBase(item, flatItem);

            item.Header.Accept(this);
            item.Toolbar.Accept(this);
            item.ColumnRow.Accept(this);
            item.Bottombar.Accept(this);
        }
                
        public override void Visit(PKGridColumnRow item)
        {
            PKFlatItem flatItem = new PKFlatItem();
            this.FlatItems.Add(flatItem);

            UpdateFromBase(item, flatItem);
            
            item.Cells.ToList().ForEach(x => {
                x.Accept(this);
            });
        }

        public override void Visit(PKPanelHeader item)
        {
            Visit(item as PKText);
        }

        public override void Visit(PKButton item)
        {
            Visit(item as PKText);
        }

        public override void Visit(PKField item)
        {
            PKFlatItem flatItem = new PKFlatItem();
            this.FlatItems.Add(flatItem);
            UpdateFromBase(item, flatItem);

            item.LabelField.Accept(this);
            item.ValueField.Accept(this);
        }

        public override void Visit(PKText item)
        {
            PKFlatItem flatItem = new PKFlatItem();
            this.FlatItems.Add(flatItem);

            UpdateFromBase(item, flatItem);
            flatItem.HAlign = (int)item.Align;

            flatItem.Value = item.Value;
        }

        public override void Visit(PKFieldItem item)
        {
            PKFlatItem flatItem = new PKFlatItem();
            this.FlatItems.Add(flatItem);

            UpdateFromBase(item, flatItem);
            flatItem.HAlign = (int)item.Align;
            flatItem.Value = item.Value;

            var viz = new PKFlatElementFieldCreateVisitor(flatItem);
            item.Accept(viz);
        }

        public override void Visit(PKColumn item)
        {
            PKFlatItem flatItem = new PKFlatItem();
            this.FlatItems.Add(flatItem);

            UpdateFromBase(item, flatItem);

            flatItem.HAlign = (int)item.Align;
            flatItem.DataProperty = item.DataProperty;
                        
            flatItem.Value = item.Value;            
        }

        #region Private Members

        private void UpdateFromBase(PKBoxItem item, PKFlatItem flatItem)
        {
            flatItem.ClassName = item.GetType().FullName;
            flatItem.ID = item.ID;
            flatItem.Name = item.Name; 
            flatItem.ParentID = item.Parent != null ? item.Parent.ID : null;
                        
            flatItem.Flex = item.Flex;
            flatItem.Height = item.Height;
            flatItem.Width = item.Width;
            flatItem.Padding = item.Padding;

            flatItem.LayoutName = item.Layout.GetType().FullName;
        }        
        
        #endregion
    }
}
