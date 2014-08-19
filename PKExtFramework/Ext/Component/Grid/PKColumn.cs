using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Ext.Drawing;
using System.ComponentModel;
using PKExtFramework.Visitors;
using PKExtFramework.Ext.Layout;
using PKExtFramework.Ext.Component.Fields;

namespace PKExtFramework.Ext.Component.Grid
{
    public class PKColumn : PKText
    {
        public PKColumn()
        {
            this.Name = "Column";

            Value = string.Empty;            
            Align = PKHAlign.Left;

            this.BorderPen = PKPens.BorderPen;
            this.BackgroundBrush = PKBrushes.BackgroundThemeBrush;

            this.TextBrush = PKBrushes.HeaderBrush;
            this.TextFont = PKFonts.HeaderFont;
        }

        [Category("Data Binding")]
        [DisplayName("Row Property")]
        public string DataProperty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]        
        public PKBrush CellTextBrush { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        public PKFont CellTextFont { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        public PKPen CellBorderPen { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        public PKBrush CellBackgroundBrush { get; set; }

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
    }
}
