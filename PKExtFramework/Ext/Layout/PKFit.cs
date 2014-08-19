using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Ext.Component;

namespace PKExtFramework.Ext.Layout
{
    public class PKFit: PKLayout
    {
        public override string Name()
        {
            return "fit";
        }
        public override void Update(PKBoxItem parent, PKBoxItem[] children)
        {
            PKBoxItem x = children.ToList().FirstOrDefault();
            if (x != null)
            {
                x.X = parent.Padding;
                x.Y = parent.Padding;
                x.Width = parent.Width - parent.Padding * 2;
                x.Height = parent.Height - parent.Padding * 2;
            }
        }        
    }
}
