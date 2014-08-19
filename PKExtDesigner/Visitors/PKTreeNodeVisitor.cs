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
    internal class PKTreeNodeVisitor: PKVisitor
    {
        private TreeNodeCollection nodes;        

        public PKTreeNodeVisitor(TreeNodeCollection nodes)
        {
            this.nodes = nodes;
        }

        private TreeNode CreateNode(PKBoxItem item)
        {
            return new TreeNode 
            {
                Text = string.Format("{0} ({1})", item.Name, item.TypeName),
                Tag = item
            };
        }

        private TreeNode AddNode(PKBoxItem item, int imageIndex)
        {
            var node = CreateNode(item);
            node.ImageIndex = imageIndex;
            node.SelectedImageIndex = imageIndex;
            nodes.Add(node);
            return node;
        }

        public override void Visit(PKControl item)
        {
            var node = AddNode(item, 0);
            
            item.Items.ToList().ForEach(x =>
            {
                x.Accept(new PKTreeNodeVisitor(node.Nodes));
            });
        }

        public override void Visit(PKContainer item)
        {
            var node = AddNode(item, 1);
            item.Items.ToList().ForEach(x=>
            {
                x.Accept(new PKTreeNodeVisitor(node.Nodes));
            });
        }

        public override void Visit(PKBottombar item)
        {
            var node = AddNode(item, 17);
            item.Items.ToList().ForEach(x =>
            {
                x.Accept(new PKTreeNodeVisitor(node.Nodes));
            });
        }

        public override void Visit(PKToolbar item)
        {
            var node = AddNode(item, 17);
            item.Items.ToList().ForEach(x =>
            {
                x.Accept(new PKTreeNodeVisitor(node.Nodes));
            });
        }

        public override void Visit(PKPanelFooter item)
        {
            var node = AddNode(item, 17);
            item.Items.ToList().ForEach(x =>
            {
                x.Accept(new PKTreeNodeVisitor(node.Nodes));
            });
        }

        public override void Visit(PKTabPanel item)
        {
            var node = AddNode(item, 11);                        
            item.Body.Accept(new PKTreeNodeVisitor(node.Nodes));
        }

        public override void Visit(PKTabBar item)
        {
            
        }

        public override void Visit(PKTabPanelBody item)
        {
            var node = AddNode(item, 11);
            item.Items.ToList().ForEach(x =>
            {
                x.Accept(new PKTreeNodeVisitor(node.Nodes));
            });
        }

        public override void Visit(PKPanel item)
        {
            var node = AddNode(item, 11);
            item.Header.Accept(new PKTreeNodeVisitor(node.Nodes));
            item.Toolbar.Accept(new PKTreeNodeVisitor(node.Nodes));
            item.Body.Accept(new PKTreeNodeVisitor(node.Nodes));
            item.Footer.Accept(new PKTreeNodeVisitor(node.Nodes));
        }

        public override void Visit(PKField item)
        {
            var node = AddNode(item, 2);
            item.LabelField.Accept(new PKTreeNodeVisitor(node.Nodes));
            item.ValueField.Accept(new PKFieldTreeNodeVisitor(node.Nodes));
        }

        public override void Visit(PKText item)
        {
            var node = AddNode(item, 3);
        }

        public override void Visit(PKPanelHeader item)
        {
            var node = AddNode(item, 3);
        }

        public override void Visit(PKButton item)
        {
            var node = AddNode(item, 14);
        }

        public override void Visit(PKColumn item)
        {
            var node = AddNode(item, 14);
        }

        public override void Visit(PKFieldItem item)
        {
            var node = AddNode(item, 3);
        }

        public override void Visit(PKGrid item)
        {
            var node = AddNode(item, 4);
            item.Header.Accept(new PKTreeNodeVisitor(node.Nodes));
            item.Toolbar.Accept(new PKTreeNodeVisitor(node.Nodes));
            item.ColumnRow.Accept(new PKTreeNodeVisitor(node.Nodes));
            item.Bottombar.Accept(new PKTreeNodeVisitor(node.Nodes));
        }

        public override void Visit(PKGridColumnRow item)
        {
            var node = AddNode(item, 5);
            item.Cells.ToList().ForEach(x =>
            {
                x.Accept(new PKTreeNodeVisitor(node.Nodes));
            });
        }        
    }
}
