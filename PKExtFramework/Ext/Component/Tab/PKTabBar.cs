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
    public class PKTabBar:PKContainer
    {
        private int ActiveItemIndex;
        public PKTabBar()
        {
            this.Name = "TabBar";
            this.Layout = new PKLayout();
            this.Padding = 1;
            this.BorderPen = PKPens.BorderPen;
            this.BackgroundBrush = PKBrushes.BackgroundThemeBrush;            
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

        public void ResetHeight()
        {
            this.Height = 25;
        }

        internal void SetTabItems(PKBoxItem[] tabItems)
        {
            this.items.Clear();
            tabItems.ToList().ForEach(x=>{
                if ((x as PKTab).Text == null)
                    (x as PKTab).Text = "Header";
                this.items.Add(new PKTabButton
                {                     
                    Height = 22,
                    Width = 20 + (x as PKTab).Text.Length * 8,
                    Value = (x as PKTab).Text,
                    Parent = this,
                    TabPage = x
                });
            });
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
            new PKHBox().Update(this, tempItems.ToArray());
        }

        public override void Activate(PKBoxItem item)
        {
            this.items.ForEach(x =>
            {
                x.IsSelected = false;                
            });
            ActiveItemIndex = (this.Parent as PKTabPanel).Body.ActiveItemIndex;
        }

        public override PKBoxItem HitTest(int x, int y)
        {
            for (int i = 0; i < this.items.Count; i++ )
            {
                var hitItem = this.items[i].HitTest(x, y);
                if (hitItem != null)
                {
                    ActiveItemIndex = i;
                    Activate((this.Parent as PKTabPanel).Body);                    
                    return (this.Parent as PKTabPanel).Body.Items[i];
                }
            }
            return base.HitTest(x, y);
        }

        public override void Render(PKGraphics graphics)
        {
            base.Render(graphics);
            if (ActiveItemIndex >= 0 && ActiveItemIndex < this.items.Count)
            {
                this.items[ActiveItemIndex].IsSelected = true;
                this.items[ActiveItemIndex].Render(graphics);
            }
        }

        public override void Accept(PKVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
