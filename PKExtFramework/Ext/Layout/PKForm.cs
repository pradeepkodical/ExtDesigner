using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Ext.Component;

namespace PKExtFramework.Ext.Layout
{
    public class PKForm : PKLayout
    {
        public override string Name()
        {
            return "form";
        }

        public override void Update(PKBoxItem parent, PKBoxItem[] children)
        {
            int y = parent.Padding;
            children.ToList().ForEach(x =>
            {
                x.X = parent.Padding;
                x.Y = y;
                x.Width = parent.Width - 2 * parent.Padding;
                //x.Height = 25;
                y += x.Height;
            });
        }
    }
}
