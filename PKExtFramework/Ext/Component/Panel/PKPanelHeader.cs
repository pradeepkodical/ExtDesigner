using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Visitors;
using PKExtFramework.Ext.Layout;
using PKExtFramework.Ext.Drawing;
using System.ComponentModel;
using PKExtFramework.Ext.Component.Fields;
using PKExtFramework.Ext.GDI;

namespace PKExtFramework.Ext.Component.Panel
{
    public class PKPanelHeader:PKText
    {
        public PKPanelHeader()            
        {
            this.Name = "Header";
            this.Height = 25;
            this.BackgroundBrush = PKBrushes.BackgroundThemeBrush;
            this.BorderPen = PKPens.BorderPen;
            this.TextFont = PKFonts.HeaderFont;
            this.TextBrush = PKBrushes.HeaderBrush;
            this.Corners = PKRectangleCorners.TopLeft | PKRectangleCorners.TopRight;
        }
        public void ResetHeight()
        {
            this.Height = 25;
        }
        [Browsable(false)]
        public override PKLayout Layout
        {
            get
            {
                return base.Layout;
            }
            set
            {
                base.Layout = value;
            }
        }
        public override void Accept(PKVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
