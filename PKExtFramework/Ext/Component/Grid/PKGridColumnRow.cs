using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Visitors;
using PKExtFramework.Ext.Layout;
using PKExtFramework.Ext.Drawing;
using System.ComponentModel;

namespace PKExtFramework.Ext.Component.Grid
{
    public class PKGridColumnRow:PKBoxItem
    {
        public PKGridColumnRow()            
        {
            this.Name = "Table Column Row";
            cells = new List<PKColumn>();
            this.Height = 25;
            this.Layout = new PKHBox();
        }

        public PKColumn Add(PKColumn column)
        {
            column.Parent = this;
            this.cells.Add(column);
            return column;
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

        protected List<PKColumn> cells;

        /// <summary>
        /// 
        /// </summary>
        public PKColumn[] Cells
        { 
            get 
            {
                return cells.ToArray();
            }
        }

        public void ResetHeight()
        {
            this.Height = 25;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public override void DoLayout()
        {
            this.ResetHeight();
            this.Layout.Update(this, this.Cells);            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphics"></param>
        public override void Render(PKGraphics graphics)
        {
            this.RenderBackground(graphics);
            this.cells.ForEach(item =>
            {
                item.Render(graphics);
            });
            base.Render(graphics);            
        }
        
        public override void Remove(PKBoxItem item)
        {
            this.cells.Remove(item as PKColumn);
        }

        public override PKBoxItem HitTest(int x, int y)
        {
            foreach (var item in this.cells)
            {
                var hitItem = item.HitTest(x, y);
                if (hitItem != null)
                {
                    return hitItem;
                }
            }
            return base.HitTest(x, y);
        }

    }
}
