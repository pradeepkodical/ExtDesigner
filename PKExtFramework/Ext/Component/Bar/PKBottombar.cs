using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Visitors;
using PKExtFramework.Ext.Layout;
using PKExtFramework.Ext.Drawing;
using System.ComponentModel;
using PKExtFramework.Ext.GDI;

namespace PKExtFramework.Ext.Component
{
    public class PKBottombar : PKContainer
    {
        public PKBottombar()            
        {
            this.Name = "Bottombar";
            this.Layout = new PKHBox();
            this.Padding = 2;
            this.BorderPen = PKPens.BorderPen;
            this.BackgroundBrush = PKBrushes.BackgroundThemeBrush;
            this.Corners = PKRectangleCorners.BottomLeft | PKRectangleCorners.BottomRight;
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

        public override void DoLayout()
        {
            var tempItems = new List<PKBoxItem>();
            this.Items.ToList().ForEach(x => {
                tempItems.Add(x);
                tempItems.Add(new PKContainer
                {
                    Width = 2
                });
            });
            this.Layout.Update(this, tempItems.ToArray());
        }

        public override void Accept(PKVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
