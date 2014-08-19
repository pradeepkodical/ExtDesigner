using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PKExtFramework.Ext.Component;
using PKExtFramework.Persistance;
using PKExtFramework.Ext.Drawing;
using PKExtFramework.Ext.Layout;
using PKExtFramework.Ext.Component.Fields;
using PKExtFramework.Ext.Component.Panel;
using PKExtFramework.Ext.Component.Tab;
using PKExtFramework.Ext.Component.Grid;

namespace PKExtFramework.Visitors
{
    internal class PKElementCreateVisitor : PKVisitor
    {
        private PKFlatItem flatItem;

        public PKElementCreateVisitor(PKFlatItem flatItem)
        {
            this.flatItem = flatItem;
        }

        public override void Visit(PKControl item)
        {
            Visit(item as PKContainer);
        }

        public override void Visit(PKContainer item)
        {
            VisitBase(item);
            item.Text = flatItem.Text;
        }

        public override void Visit(PKPanel item)
        {
            VisitBase(item);
        }

        public override void Visit(PKTabPanel item)
        {
            VisitBase(item);
        }

        public override void Visit(PKTabPanelBody item)
        {
            VisitBase(item);
        }

        public override void Visit(PKBottombar item)
        {
            Visit(item as PKContainer);
        }

        public override void Visit(PKTabBar item)
        {
            //managed internally
        }

        public override void Visit(PKToolbar item)
        {
            Visit(item as PKContainer);
        }

        public override void Visit(PKPanelFooter item)
        {
            Visit(item as PKContainer);            
        }

        public override void Visit(PKPanelHeader item)
        {
            Visit(item as PKText);
        }        

        public override void Visit(PKField item)
        {
            VisitBase(item);
        }        

        public override void Visit(PKGrid item)
        {
            VisitBase(item);
        }

        public override void Visit(PKGridColumnRow item)
        {
            VisitBase(item);
        }

        public override void Visit(PKText item)
        {
            VisitBase(item);
            item.Align = (PKHAlign)flatItem.HAlign;            
            item.Value = flatItem.Value;
        }

        public override void Visit(PKButton item)
        {
            Visit(item as PKText);
        }

        public override void Visit(PKColumn item)
        {
            Visit(item as PKText);
            item.Value = flatItem.Value;            
        }

        public override void Visit(PKFieldItem item)
        {
            Visit(item as PKText);
            var viz = new PKFieldElementCreateVisitor(flatItem);
            item.Accept(viz);
        }

        private void VisitBase(PKBoxItem item)
        {
            item.ID = flatItem.ID;

            item.Name = flatItem.Name;

            item.Flex = flatItem.Flex;
            item.Height = flatItem.Height;
            item.Width = flatItem.Width;
            item.Padding = flatItem.Padding;
            item.Layout = (PKLayout)Activator.CreateInstance(Type.GetType(flatItem.LayoutName));            
        }
    }
}
