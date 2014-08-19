using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Ext.Drawing;
//using PKExtFramework.UITypeConverters;
using System.ComponentModel;
using PKExtFramework.Visitors;
using System.Drawing.Design;
using System.ComponentModel.Design;
using PKExtFramework.Ext.Layout;

namespace PKExtFramework.Ext.Component.Fields
{
    public class PKText : PKBoxItem
    {
        /// <summary>
        /// 
        /// </summary>
        [Category("Data")]
        [DisplayName("Text Value")]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Category("Data")]        
        [DisplayName("Text Align")]
        public PKHAlign Align { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]        
        public PKBrush TextBrush { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]        
        public PKFont TextFont { get; set; }

        /// <summary>
        /// 
        /// </summary>
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
        public PKText()
        {
            this.Name = "Text";

            Value = string.Empty;            
            Align = PKHAlign.Left;

            this.Padding = 5;

            this.TextBrush = new PKSolidBrush(new PKColor(0, 0, 0, 255), 1);
            this.TextFont = new PKFont("Tahoma", 10, false);            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphics"></param>
        public override void Render(PKGraphics graphics)
        {
            PKPoint absPosition = GetAbsPosition();
            this.RenderBackground(graphics);

            if (this.TextFont == null)
            {
                this.TextFont = new PKFont("Tahoma", 10, false);
            }

            graphics.WriteText(
                this.Value,
                this.TextBrush == null ? PKBrushes.BlackBrush : this.TextBrush,
                this.TextFont == null?new PKFont("Tahoma", 10, false): this.TextFont,                
                this.Align,
                absPosition.X + this.Padding,
                absPosition.Y,
                this.Width - this.Padding * 2, this.Height);
            base.Render(graphics);          
        }

        public override void Accept(PKVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
