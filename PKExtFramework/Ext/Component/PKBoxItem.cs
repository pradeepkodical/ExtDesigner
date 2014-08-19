using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Ext.Drawing;
using PKExtFramework.Ext.Layout;
using PKExtFramework.Utility;
using System.ComponentModel;
using PKExtFramework.TypeConverters;
using PKExtFramework.Visitors;
using PKExtFramework.Ext.GDI;

namespace PKExtFramework.Ext.Component
{
    public class PKBoxItem
    {
        private PKPoint position;
        protected PKRectangleCorners Corners { get; set; }

        [ReadOnly(true)]
        [Category("Document Settings")]
        public string ID { get; set; }
                
        public PKBoxItem()
        {
            this.Name = "Box";
            this.Layout = new PKLayout();
            this.ID = PKSequenceGenerator.GetNextId();
            this.position = new PKPoint();
            this.Corners = PKRectangleCorners.None;
        }        

        /// <summary>
        /// 
        /// </summary>
        [TypeConverter(typeof(LayoutConverter)), CategoryAttribute("Document Settings")]        
        public virtual PKLayout Layout { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]        
        public PKPen BorderPen { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //[Category("Graphics")]
        [Browsable(false)]
        public PKBrush BackgroundBrush { get; set; }
                
        /// <summary>
        /// Location
        /// </summary>
        internal PKPoint Position { get { return position; } }
        /// <summary>
        /// Relative Left Position from Parent
        /// </summary>        
        [Browsable(false)]
        public int X { get { return position.X; } set { position.X = value; } }

        /// <summary>
        /// Relative Top position from Parent
        /// </summary>        
        [Browsable(false)]
        public int Y { get { return position.Y; } set { position.Y = value; } }

        /// <summary>
        /// Width of the item
        /// </summary>
        [Category("Size")]
        public int Width { get; set; }

        /// <summary>
        /// Height of the Item
        /// </summary>
        [Category("Size")]
        public int Height { get; set; }

        /// <summary>
        /// Padding for Contents
        /// </summary>
        [Category("Size")]
        public virtual int Padding { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        public PKBoxItem Parent { get; internal set; }

        public virtual void Activate(PKBoxItem item)
        {
            if (this.Parent != null)
            {
                this.Parent.Activate(this);
            }
        }

        public PKPoint GetAbsPosition()
        {
            if (Parent != null)
            {
                return Parent.GetAbsPosition().Add(this.Position);
            }
            return this.Position.Clone();
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void DoLayout()
        { 
        }

        /// <summary>
        /// 
        /// </summary>
        public void Remove()
        {
            if (this.Parent != null)
            {
                this.Parent.Remove(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void MoveUp()
        {
            if (this.Parent != null)
            {
                this.Parent.Move(this, -1);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void MoveDown()
        {
            if (this.Parent != null)
            {
                this.Parent.Move(this, 1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="child"></param>
        /// <param name="direction"></param>
        public virtual void Move(PKBoxItem child, int direction)
        { 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public virtual void Remove(PKBoxItem item)
        { 
        }

        /// <summary>
        /// Flex Size
        /// </summary>
        [Category("Size")]
        [Description("Flexible size with respect to its siblings")]
        public int Flex { get; set; }

        /// <summary>
        /// Render the Item
        /// </summary>
        public virtual void Render(PKGraphics graphics)
        {
            if (BorderPen != null && BorderPen.Size > 0)
            {
                PKPoint absPosition = GetAbsPosition();

                graphics.DrawRectangle(
                    BorderPen,
                    absPosition.X, absPosition.Y,
                    this.Width, this.Height, 
                    this.Corners);
            }

            if (IsSelected)
            {
                PKPoint absPosition = GetAbsPosition();

                graphics.DrawRectangle(
                    PKPens.SelectedItem,
                    absPosition.X, absPosition.Y,
                    this.Width, this.Height, this.Corners);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphics"></param>
        public virtual void RenderBackground(PKGraphics graphics)
        {
            if (this.BackgroundBrush != null)
            {
                PKPoint absPosition = GetAbsPosition();

                graphics.FillRectangle(this.BackgroundBrush,
                    absPosition.X, absPosition.Y,
                    this.Width, this.Height, this.Corners);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [ReadOnly(true)]
        [Browsable(false)]
        public bool IsSelected { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public virtual PKBoxItem HitTest(int x, int y)
        {
            PKPoint absPosition = GetAbsPosition();
            if(absPosition.X<=x && x <= absPosition.X + this.Width &&
                absPosition.Y<=y && y <= absPosition.Y + this.Height){
                    return this;
                }
            return null;
        }

        private string name;
        /// <summary>
        /// 
        /// </summary>
        [Category("Document Settings")]
        public string Name 
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                if (name != null)
                {
                    name = name.Replace(" ", "");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Document Settings")]
        [DisplayName("Type")]
        public string TypeName 
        {
            get
            {
                return this.GetType().Name;
            }
        }

        [Category("Ext")]
        [ReadOnly(true)]
        public string ExtTypeName { get; set; }

        public virtual void Accept(PKVisitor visitor)
        { 
        }        
    }
}
