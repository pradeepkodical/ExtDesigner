using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Visitors;
using PKExtFramework.Ext.Layout;
using PKExtFramework.Ext.Drawing;
using System.ComponentModel;

namespace PKExtFramework.Ext.Component.Tab
{
    public class PKTabPanelBody:PKContainer
    {
        public int ActiveItemIndex { get; set; }
        public PKTabPanelBody()            
        {
            this.Name = "Body";
            this.Layout = new PKCard();
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

        public override void Activate(PKBoxItem item)
        {
            for(int i=0;i<this.items.Count;i++){
                if(items[i]==item)
                {
                    ActiveItemIndex=i;
                    break;
                }
            }
            base.Activate(item);
        }

        public override PKBoxItem HitTest(int x, int y)
        {
            if (ActiveItemIndex >= 0 && ActiveItemIndex < this.items.Count)
            {
                var hitItem = this.items[ActiveItemIndex].HitTest(x, y);
                if (hitItem != null)
                {
                    return hitItem;
                }
            }
            return base.HitTest(x, y);
        }

        public override void Render(PKGraphics graphics)
        {
            if (ActiveItemIndex >= 0 && ActiveItemIndex < this.items.Count)
            {
                this.items[ActiveItemIndex].Render(graphics);
            }
        }

        public override void Accept(PKVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
