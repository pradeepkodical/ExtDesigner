using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Ext.Component;

namespace PKExtFramework.Ext.Layout
{
    public class PKCard : PKLayout
    {
        public override string Name()
        {
            return "card";
        }

        public override void Update(PKBoxItem parent, PKBoxItem[] children)
        {
            var items = children.ToList();
            items.ForEach(x =>
            {
                x.X = parent.Padding;
                x.Y = parent.Padding;
                x.Width = parent.Width - parent.Padding * 2;
                x.Height = parent.Height - parent.Padding * 2;
            });
        }
    }    
}
