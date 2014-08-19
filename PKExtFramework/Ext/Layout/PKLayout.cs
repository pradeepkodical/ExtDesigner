using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Ext.Component;

namespace PKExtFramework.Ext.Layout
{
    public class PKLayout
    {
        public virtual string Name()
        {
            return "auto";
        }

        public virtual void Update(PKBoxItem parent, PKBoxItem[] children)
        {           
        }        
    }
}
