using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Ext.Layout;
using PKExtFramework.Ext.Drawing;
using System.ComponentModel;
using PKExtFramework.Visitors;


namespace PKExtFramework.Ext.Component.Fields
{
    public class PKLabelField : PKFieldItem
    {        
        /// <summary>
        /// 
        /// </summary>
        public PKLabelField()
        {
            this.Name = "Label Field";            
            this.ExtTypeName = "Ext.form.Label";
            this.TextFont = new PKFont("Tahoma", 10, true);
        }

        public override void Accept(PKFieldVisitor visitor)
        {
            visitor.Visit(this);
        }        
    }
}
