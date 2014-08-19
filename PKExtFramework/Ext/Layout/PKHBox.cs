using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Ext.Component;

namespace PKExtFramework.Ext.Layout
{
    public class PKHBox : PKLayout
    {
        public override string Name()
        {
            return "hbox";
        }

        public override void Update(PKBoxItem parent, PKBoxItem[] children)
        {
            var items = children.ToList();
            items.ForEach(x => 
            {
                x.Y = parent.Padding;
                x.Height = parent.Height - parent.Padding * 2;
            });

            int total = items
                .FindAll(x => x.Flex == 0)
                .Sum(x => x.Width);
            float rem = parent.Width - total - parent.Padding * 2;
            int flex = items.Sum(x => x.Flex);
            double delta = 0, r=0;
            if (flex > 0)
            {
                items
                    .FindAll(x => x.Flex > 0)
                    .ForEach(x => 
                    {
                        delta += Math.Round(r) - r;
                        r = (x.Flex * rem) / flex;                        
                        x.Width = (int)Math.Round(r);                        
                    });                
            }

            if (items.Count > 0)
            {
                items.LastOrDefault().Width -= (int)Math.Round(delta);
            }

            int soFar = parent.Padding;
            items.ForEach(x =>
            {
                x.X = soFar;
                soFar += x.Width;
            });
        }        
    }    
}
