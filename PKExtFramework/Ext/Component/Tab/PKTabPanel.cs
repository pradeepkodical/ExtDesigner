using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using PKExtFramework.Visitors;
using PKExtFramework.Ext.Layout;
using PKExtFramework.Ext.Drawing;

namespace PKExtFramework.Ext.Component.Tab
{
    public class PKTabPanel: PKBoxItem
    {
        private PKTabBar TabBar { get; set; }
        [Browsable(false)]
        public PKTabPanelBody Body { get; internal set; }
        
        public PKTabPanel()
        {
            this.Name = "Panel Container";
            this.ExtTypeName = "Ext.Panel";
            this.Layout = new PKVBox();
            this.BorderPen = PKPens.BorderPen;

            this.TabBar = new PKTabBar
            {                
                Parent = this,
                Height = 30
            };

            this.Body = new PKTabPanelBody
            {
                Parent = this,
                Flex = 1
            };
        }

        public override void Activate(PKBoxItem item)
        {
            if (item == Body)
            {
                TabBar.Activate(TabBar);
            }
            base.Activate(item);
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
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public override void Remove(PKBoxItem item)
        {
            this.Parent.Remove(this);
        }
        
        public override void DoLayout()
        {
            this.TabBar.ResetHeight();
            this.TabBar.SetTabItems(this.Body.Items);
            
            var items = new List<PKBoxItem> { 
                this.TabBar,
                this.Body
            };

            Layout.Update(this, items.ToArray());
            items.ForEach(x =>
            {
                x.DoLayout();
            });
        }

        public override void Render(PKGraphics graphics)
        {
            this.RenderBackground(graphics);

            this.TabBar.Render(graphics);
            this.Body.Render(graphics);
            
            
            base.Render(graphics);
        }

        public override PKBoxItem HitTest(int x, int y)
        {
            var hitItem = this.TabBar.HitTest(x, y);
            if (hitItem != null) return hitItem;

            hitItem = this.Body.HitTest(x, y);
            if (hitItem != null) return hitItem;

            return base.HitTest(x, y);
        }

        public override void Accept(PKVisitor visitor)
        {
            visitor.Visit(this);
        }        
    }
}
