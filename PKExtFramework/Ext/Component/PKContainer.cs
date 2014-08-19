using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Ext.Layout;
using PKExtFramework.Ext.Drawing;
using System.ComponentModel;
using PKExtFramework.Visitors;

namespace PKExtFramework.Ext.Component
{
    public class PKContainer : PKBoxItem
    {
        public PKContainer()
        {
            items = new List<PKBoxItem>();
            this.Name = "Container";
            this.ExtTypeName = "Ext.Container";
        }
                
        protected List<PKBoxItem> items;

        [Browsable(false)]
        public PKBoxItem[] Items
        { 
            get 
            {
                return items.ToArray();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Text { get; set; }

        public override void Move(PKBoxItem child, int direction)
        {
            int index = this.items.IndexOf(child);
            int nindex = index + direction;
            if (nindex >= 0 && nindex < this.items.Count)
            {
                this.Remove(child);
                this.items.Insert(nindex, child);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public override void Remove(PKBoxItem item)
        {
            this.items.Remove(item);
        }

        public PKBoxItem Add(PKBoxItem item)
        {
            this.items.Add(item);
            item.Parent = this;
            return item;
        }
                
        public override void DoLayout()
        { 
            Layout.Update(this, this.Items);
            items.ForEach(x => {
                x.DoLayout();
            });
        }

        public override void Render(PKGraphics graphics)
        {
            this.RenderBackground(graphics);
            this.items.ForEach(item => {
                item.Render(graphics);
            });
            base.Render(graphics);            
        }

        public override PKBoxItem HitTest(int x, int y)
        {
            foreach(var item in this.items)
            {
                var hitItem = item.HitTest(x, y);
                if (hitItem != null)
                {
                    return hitItem;
                }
            }
            return base.HitTest(x, y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitor"></param>
        public override void Accept(PKVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
