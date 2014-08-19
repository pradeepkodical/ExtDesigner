using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using PKExtFramework.Visitors;
using PKExtFramework.Ext.Layout;
using PKExtFramework.Ext.Drawing;
using PKExtFramework.Ext.GDI;

namespace PKExtFramework.Ext.Component.Panel
{
    public class PKPanel: PKBoxItem
    {
        [Browsable(false)]
        public PKPanelHeader Header { get; internal set; }
        [Browsable(false)]
        public PKToolbar Toolbar { get; internal set; }
        [Browsable(false)]
        public PKPanelBody Body { get; internal set; }
        [Browsable(false)]
        public PKPanelFooter Footer { get; internal set; }

        public PKPanel()
        {
            this.Name = "Panel Container";
            this.ExtTypeName = "Ext.Panel";
            this.Layout = new PKVBox();
            this.BorderPen = PKPens.BorderPen;
            this.Corners = PKRectangleCorners.All;
            this.Header = new PKPanelHeader
            {
                Parent = this,
                Height = 25,                
                Value = "Panel Header"                
            };
            this.Body = new PKPanelBody
            {                
                Parent = this,
                Flex = 1
            };
            this.Footer = new PKPanelFooter
            {
                Parent = this,
                Height = 25
            };
            this.Toolbar = new PKToolbar
            {
                Parent = this,
                Height = 25
            };            
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
            this.Header.ResetHeight();
            this.Toolbar.ResetHeight();
            this.Footer.ResetHeight();

            var items = new List<PKBoxItem> { 
                this.Header,
                this.Toolbar,
                this.Body,
                this.Footer
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
            this.Header.Render(graphics);
            this.Toolbar.Render(graphics);
            this.Body.Render(graphics);
            this.Footer.Render(graphics);
            
            base.Render(graphics);
        }

        public override PKBoxItem HitTest(int x, int y)
        {
            var hitItem = this.Header.HitTest(x, y);
            if (hitItem != null) return hitItem;

            hitItem = this.Toolbar.HitTest(x, y);
            if (hitItem != null) return hitItem;

            hitItem = this.Body.HitTest(x, y);
            if (hitItem != null) return hitItem;

            hitItem = this.Footer.HitTest(x, y);
            if (hitItem != null) return hitItem;

            return base.HitTest(x, y);
        }

        public override void Accept(PKVisitor visitor)
        {
            visitor.Visit(this);
        }        
    }
}
