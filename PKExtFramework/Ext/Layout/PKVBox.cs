using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Ext.Component;

namespace PKExtFramework.Ext.Layout
{
    public class PKVBox : PKLayout
    {
        public override string Name()
        {
            return "vbox";
        }

        public override void Update(PKBoxItem parent, PKBoxItem[] children)
        {
            var items = children.ToList();
            items.ForEach(x =>
            {
                x.X = parent.Padding;
                x.Width = parent.Width - parent.Padding * 2;
            });

            int total = items
                .FindAll(x => x.Flex == 0)
                .Sum(x => x.Height);
            float rem = parent.Height - total - parent.Padding * 2;
            int flex = items.Sum(x => x.Flex);
            if (flex > 0)
            {

                items
                    .FindAll(x => x.Flex > 0)
                    .ForEach(x =>
                    {
                        x.Height = (int)Math.Round((x.Flex * rem) / flex);                        
                    });                
            }

            int soFar = parent.Padding;
            items.ForEach(x =>
                {
                    x.Y = soFar;
                    soFar += x.Height;
                });
        }
    }    
}
