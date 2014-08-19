using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Ext.Drawing;
using System.ComponentModel;
using PKExtFramework.Visitors;

namespace PKExtFramework.Ext.Component.Fields
{
    public abstract class PKFieldItem: PKText
    {
        public PKFieldItem()
        {
            this.Name = "Field Item";

            Value = string.Empty;            
            Align = PKHAlign.Left;

            this.TextBrush = new PKSolidBrush(new PKColor(0, 0, 0, 255), 1);
            this.TextFont = new PKFont("Tahoma", 10, false);            
        }

        public override void Accept(PKVisitor visitor)
        {
            visitor.Visit(this);
        }

        public abstract void Accept(PKFieldVisitor visitor);
    }
}
