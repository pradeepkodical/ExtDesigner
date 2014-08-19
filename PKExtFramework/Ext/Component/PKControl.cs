using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using PKExtFramework.Visitors;
using PKExtFramework.Ext.Drawing;

namespace PKExtFramework.Ext.Component
{
    public class PKControl: PKContainer
    {
        public PKControl()
        {
            this.Name = "Control";
            this.ExtTypeName = "Ext.Container";
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Position")]
        public int Left 
        {
            get
            {
                return this.X;
            }
            set
            {
                this.X = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Category("Position")]
        public int Top 
        {
            get
            {
                return this.Y;
            }
            set
            {
                this.Y = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Document Settings")]
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Category("Document Settings")]
        public string Author { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Category("Document Settings")]
        public string Application { get; set; }

        [Browsable(false)]
        public bool IsComponent { get; set; }

        [Browsable(false)]
        public string ComponentFileName { get; set; }

        public override PKBoxItem HitTest(int x, int y)
        {
            if (this.IsComponent)
            {
                PKPoint absPosition = GetAbsPosition();
                if (absPosition.X <= x && x <= absPosition.X + this.Width &&
                    absPosition.Y <= y && y <= absPosition.Y + this.Height)
                {
                    return this;
                }
                return null;
            }
            else
            {
                return base.HitTest(x, y);
            }
        }

        public override void Accept(PKVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
