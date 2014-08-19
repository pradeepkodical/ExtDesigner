using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Ext.Layout;
using PKExtFramework.Ext.Drawing;
using PKExtFramework.Visitors;
using System.ComponentModel;
using PKExtFramework.Ext.Component.Panel;

namespace PKExtFramework.Ext.Component.Grid
{
    public class PKGrid : PKBoxItem
    {
        [Browsable(false)]
        public PKPanelHeader Header { get; internal set; }
        [Browsable(false)]
        public PKToolbar Toolbar { get; internal set; }
        [Browsable(false)]
        public PKBottombar Bottombar { get; internal set; }
        [Browsable(false)]
        public PKGridColumnRow ColumnRow { get; internal set; }        
        
        private int rowHeight;

        public PKGrid():this(22,22)
        {
            this.BorderPen = PKPens.BorderPen;
        }
        public PKGrid(int columnHeight, int rowHeight)
        {
            this.Name = "Table";
            this.ExtTypeName = "Ext.grid.GridPanel";
            this.BorderPen = PKPens.BorderPen;
            this.rowHeight = rowHeight;
            this.Layout = new PKVBox();
            this.ColumnRow = new PKGridColumnRow 
            { 
                Parent = this,                
                Height = columnHeight,
                Name = "Column Header"
            };

            this.Header = new PKPanelHeader
            {
                Parent = this,                
                Value = "Grid Header"
            };

            this.Toolbar = new PKToolbar
            {
                Parent = this
            };
            this.Bottombar = new PKBottombar
            {
                Parent = this
            };
            
            this.Layout = new PKVBox();
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

        public void AddColumn(PKColumn column)
        {
            ColumnRow.Add(column);            
        }
        
        public override void DoLayout()
        {
            this.Header.ResetHeight();
            this.Toolbar.ResetHeight();
            this.Bottombar.ResetHeight();

            List<PKBoxItem> items = new List<PKBoxItem> { 
                this.Header,
                this.Toolbar,
                this.ColumnRow,
                new PKContainer{
                    Flex = 1,
                    Parent= this
                },
                this.Bottombar
            };            

            new PKVBox().Update(this, items.ToArray());
            this.Header.DoLayout();
            this.Toolbar.DoLayout();
            this.ColumnRow.DoLayout();
            this.Bottombar.DoLayout();            
        }

        public override void Render(PKGraphics graphics)
        {
            this.RenderBackground(graphics);            
            this.Header.Render(graphics);
            this.Toolbar.Render(graphics);
            this.ColumnRow.Render(graphics);
            this.Bottombar.Render(graphics);
            base.Render(graphics);
        }

        public override PKBoxItem HitTest(int x, int y)
        {
            var hitItem = this.Header.HitTest(x, y);
            if (hitItem != null)return hitItem;

            hitItem = this.Toolbar.HitTest(x, y);
            if (hitItem != null)return hitItem;

            hitItem = this.ColumnRow.HitTest(x, y);
            if (hitItem != null) return hitItem;

            hitItem = this.Bottombar.HitTest(x, y);
            if (hitItem != null) return hitItem;

            return base.HitTest(x, y);
        }

        public override void Accept(PKVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
