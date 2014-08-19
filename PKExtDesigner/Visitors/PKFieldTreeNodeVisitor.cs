using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Visitors;
using System.Windows.Forms;
using PKExtFramework.Ext.Component;
using PKExtFramework.Ext.Component.Fields;

namespace PKExtDesigner.Visitors
{
    internal class PKFieldTreeNodeVisitor: PKFieldVisitor
    {
        private TreeNodeCollection nodes;

        public PKFieldTreeNodeVisitor(TreeNodeCollection nodes)
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

        public override void Visit(PKLabelField item)
        {
            var node = AddNode(item, 18);
        }

        public override void Visit(PKCheckField item)
        {
            var node = AddNode(item, 15);
        }

        public override void Visit(PKComboField item)
        {
            var node = AddNode(item, 12);
        }

        public override void Visit(PKDateField item)
        {
            var node = AddNode(item, 13);
        }

        public override void Visit(PKTextField item)
        {
            var node = AddNode(item, 19);
        }

        public override void Visit(PKNumberField item)
        {
            var node = AddNode(item, 19);
        }

        public override void Visit(PKTextAreaField item)
        {
            var node = AddNode(item, 19);
        }

        public override void Visit(PKRadioField item)
        {
            var node = AddNode(item, 16);
        }
    }
}
