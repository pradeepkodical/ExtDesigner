using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PKExtFramework.Ext.Component;
using PKExtFramework.Persistance;
using PKExtFramework.Ext.Component.Fields;
using PKExtFramework.Ext.Component.Panel;
using PKExtFramework.Ext.Component.Tab;
using PKExtFramework.Ext.Component.Grid;

namespace PKExtFramework.Visitors
{
    internal class PKElementParentVistor:PKVisitor
    {
        private List<PKFlatItem> flatItems;        
        private PKBoxItem currentItem;

        private bool AreSameType<T>(PKBoxItem childItem)
        {
            return typeof(T) == childItem.GetType();
        }

        public PKElementParentVistor(List<PKFlatItem> flatItems, PKBoxItem currentItem)
        {
            this.flatItems = flatItems;
            this.currentItem = currentItem;            
        }

        public override void Visit(PKControl item)
        {
            item.Add(this.currentItem);
        }

        public override void Visit(PKContainer item)
        {
            item.Add(this.currentItem);
        }

        public override void Visit(PKPanelFooter item)
        {
            item.Add(this.currentItem);
        }

        public override void Visit(PKBottombar item)
        {
            item.Add(this.currentItem);
        }

        public override void Visit(PKToolbar item)
        {
            item.Add(this.currentItem);
        }

        public override void Visit(PKTabPanel item)
        {
            if (AreSameType<PKTabPanelBody>(currentItem))
            {
                currentItem.Parent = item;
                item.Body = currentItem as PKTabPanelBody;
            }            
        }

        public override void Visit(PKTabBar item)
        {
            //managed internally
        }

        public override void Visit(PKTabPanelBody item)
        {
            item.Add(this.currentItem);
        }

        public override void Visit(PKPanel item)
        {
            if (AreSameType<PKPanelHeader>(currentItem))
            {
                currentItem.Parent = item;
                item.Header = currentItem as PKPanelHeader;                
            }
            else if (AreSameType<PKToolbar>(currentItem))
            {
                currentItem.Parent = item;
                item.Toolbar = currentItem as PKToolbar;
            }
            else if (AreSameType<PKPanelBody>(currentItem))
            {
                currentItem.Parent = item;
                item.Body = currentItem as PKPanelBody;
            }
            else if (AreSameType<PKPanelFooter>(currentItem))
            {
                currentItem.Parent = item;
                item.Footer = currentItem as PKPanelFooter;
            }
        }

        public override void Visit(PKPanelHeader item)
        {

        }
        
        public override void Visit(PKGrid item)
        {
            if (AreSameType<PKGridColumnRow>(currentItem))
            {                
                currentItem.Parent = item;
                item.ColumnRow = currentItem as PKGridColumnRow;                                
            }
            else if (AreSameType<PKPanelHeader>(currentItem))
            {
                currentItem.Parent = item;
                item.Header = currentItem as PKPanelHeader;                
            }
            else if (AreSameType<PKToolbar>(currentItem))            
            {
                currentItem.Parent = item;
                item.Toolbar = currentItem as PKToolbar;                
            }
            else if (AreSameType<PKBottombar>(currentItem))
            {
                currentItem.Parent = item;
                item.Bottombar = currentItem as PKBottombar;                
            }
        }

        public override void Visit(PKGridColumnRow item)
        {
            if (currentItem is PKColumn)
            {
                item.Add(currentItem as PKColumn);
            }
        }

        public override void Visit(PKField item)
        {
            if (this.currentItem is PKFieldItem)
            {
                item.Add(this.currentItem as PKFieldItem);
            }
        }

        public override void Visit(PKFieldItem item)
        {
            
        }

        public override void Visit(PKText item)
        {
            
        }

        public override void Visit(PKButton item)
        {

        }

        public override void Visit(PKColumn item)
        {
            
        }                      
    }
}
