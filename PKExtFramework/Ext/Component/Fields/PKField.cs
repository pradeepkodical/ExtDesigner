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
    public class PKField: PKBoxItem
    {
        protected List<PKFieldItem> cells;

        /// <summary>
        /// 
        /// </summary>
        public PKField()
        {
            this.Name = "Field Container";
            this.cells = new List<PKFieldItem>();
            this.Layout = new PKHBox();
            this.ExtTypeName = "Ext.form.Field";
            this.Height = 25;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public PKFieldItem Add(PKFieldItem column)
        {
            column.Parent = this;
            this.cells.Add(column);
            return column;
        }

        [Category("Elements")]
        [DisplayName("Label")]
        public PKFieldItem LabelField
        {
            get
            {
                if (this.cells.Count > 0)
                {
                    return this.cells[0] as PKFieldItem;
                }
                return null;
            }
        }

        [Category("Elements")]
        [DisplayName("Text")]
        public PKFieldItem ValueField
        {
            get
            {
                if (this.cells.Count > 1)
                {
                    return this.cells[1] as PKFieldItem;
                }
                return null;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public override void DoLayout()
        {
            new PKHBox().Update(this, this.cells.ToArray());            
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
        
        public override void Remove(PKBoxItem item)
        {
            this.Parent.Remove(this);
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

        public override void Accept(PKVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
