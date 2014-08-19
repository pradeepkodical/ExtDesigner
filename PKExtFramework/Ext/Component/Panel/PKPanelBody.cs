using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Visitors;
using PKExtFramework.Ext.Layout;
using PKExtFramework.Ext.Drawing;
using System.ComponentModel;

namespace PKExtFramework.Ext.Component.Panel
{
    public class PKPanelBody:PKContainer
    {
        public PKPanelBody()            
        {
            this.Name = "Body";
            this.Layout = new PKFit();
        }

        public override void DoLayout()
        {
            this.Flex = 1;
            base.DoLayout();
        }

        public override void Accept(PKVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
