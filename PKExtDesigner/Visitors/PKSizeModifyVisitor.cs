using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Visitors;
using PKExtFramework.Ext.Component;
using PKExtFramework.Ext.Component.Fields;
using PKExtFramework.Ext.Component.Tab;
using PKExtFramework.Ext.Component.Panel;
using PKExtFramework.Ext.Component.Grid;

namespace PKExtDesigner.Visitors
{
    internal class PKSizeModifyVisitor: PKVisitor
    {
        private int width;
        private int height;
        private int flex;

        public PKSizeModifyVisitor(int width, int height, int flex)
        {
            this.width = width<0?0:width;
            this.height = height<0?0:height;
            this.flex = flex<0?0:flex;
        }

        private void UpdateSize(PKBoxItem item)
        {
            item.Width = width>=0?width:item.Width;
            item.Height = height>=0?height:item.Height;
            item.Flex = flex>=0?flex:item.Flex;
        }

        public override void Visit(PKControl item)
        {            
        }

        public override void Visit(PKContainer item)
        {
            UpdateSize(item);
        }

        public override void Visit(PKTabPanel item)
        {
            UpdateSize(item);
        }

        public override void Visit(PKTabBar item)
        {
            UpdateSize(item);
        }

        public override void Visit(PKTabPanelBody item)
        {
            UpdateSize(item);
        }

        public override void Visit(PKPanel item)
        {
            UpdateSize(item);
        }

        public override void Visit(PKField item)
        {
            UpdateSize(item);
        }

        public override void Visit(PKGrid item)
        {
            UpdateSize(item);
        }

        public override void Visit(PKGridColumnRow item)
        {
            UpdateSize(item);
        }

        public override void Visit(PKText item)
        {
            UpdateSize(item);
        }

        public override void Visit(PKButton item)
        {
            UpdateSize(item);
        }

        public override void Visit(PKColumn item)
        {
            UpdateSize(item);
        }

        public override void Visit(PKFieldItem item)
        {
            UpdateSize(item);
        }

        public override void Visit(PKPanelHeader item)
        {
        }

        public override void Visit(PKPanelFooter item)
        {
        }

        public override void Visit(PKToolbar item)
        {            
        }

        public override void Visit(PKBottombar item)
        {            
        }
    }
}
