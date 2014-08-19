using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Ext.Component.Fields;
using PKExtFramework.Persistance;

namespace PKExtFramework.Visitors
{
    internal class PKFlatElementFieldCreateVisitor:PKFieldVisitor
    {
        private PKFlatItem flatItem;

        public PKFlatElementFieldCreateVisitor(PKFlatItem flatItem)
        {
            this.flatItem = flatItem;
        }

        public override void Visit(PKLabelField item)
        {
        }

        public override void Visit(PKCheckField item)
        {
        }

        public override void Visit(PKComboField item)
        {
            flatItem.DisplayMember = item.DisplayMember;
            flatItem.ValueMember = item.ValueMember;
        }

        public override void Visit(PKDateField item)
        {            
        }

        public override void Visit(PKTextField item)
        {            
        }

        public override void Visit(PKRadioField item)
        {            
        }

        public override void Visit(PKNumberField item)
        {            
        }

        public override void Visit(PKTextAreaField item)
        {            
        }
    }
}
