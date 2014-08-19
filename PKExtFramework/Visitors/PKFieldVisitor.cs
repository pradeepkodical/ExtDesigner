using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Ext.Component;
using PKExtFramework.Ext.Component.Fields;

namespace PKExtFramework.Visitors
{
    public abstract class PKFieldVisitor
    {
        public abstract void Visit(PKLabelField item);
        public abstract void Visit(PKCheckField item);
        public abstract void Visit(PKComboField item);
        public abstract void Visit(PKDateField item);
        public abstract void Visit(PKTextField item);
        public abstract void Visit(PKRadioField item);
        public abstract void Visit(PKNumberField item);
        public abstract void Visit(PKTextAreaField item);
    }
}
