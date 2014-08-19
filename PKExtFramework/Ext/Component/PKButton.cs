using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Ext.Drawing;
using System.ComponentModel;
using PKExtFramework.Visitors;
using System.Drawing.Design;
using System.ComponentModel.Design;
using PKExtFramework.Ext.Layout;
using PKExtFramework.Ext.Component.Fields;
using PKExtFramework.Ext.GDI;

namespace PKExtFramework.Ext.Component
{
    public class PKButton : PKText
    {    
        /// <summary>
        /// 
        /// </summary>
        public PKButton()
        {
            this.Name = "Button";
            this.ExtTypeName = "Ext.Button";

            Value = string.Empty;            
            Align = PKHAlign.Center;

            this.Padding = 5;
            this.Height = 22;
            this.BorderPen = PKPens.BorderPen;
            this.TextBrush = PKBrushes.SteelBlueBrush;
            this.TextFont = PKFonts.LabelFont;
            this.BackgroundBrush = PKBrushes.ButtonBackgroundBrush;
            this.Corners = PKRectangleCorners.All;
        }
        
        public override void Accept(PKVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
