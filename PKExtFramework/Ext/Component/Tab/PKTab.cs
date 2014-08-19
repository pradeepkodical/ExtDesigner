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
    public class PKTab:PKContainer
    {
        public PKTab()            
        {
            this.Name = "Tab";
            this.Layout = new PKFit();            
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
    }
}
