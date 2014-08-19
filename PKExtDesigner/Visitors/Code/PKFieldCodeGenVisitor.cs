using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Visitors;
using System.Windows.Forms;
using PKExtFramework.Ext.Component;
using PKExtFramework.Ext.Component.Fields;

namespace PKExtDesigner.Visitors.Code
{
    internal class PKFieldCodeGenVisitor: PKFieldVisitor
    {
        public string StrCode { get; private set; }
        public PKFieldCodeGenVisitor()
        {            
        }
        
        public override void Visit(PKLabelField item)
        {
            StrCode = "allowBlank: true";
        }

        public override void Visit(PKComboField item)
        {
            StrCode = string.Format(@"
                allowBlank: true,
                msgTarget : 'qtip',
                store: new Ext.data.JsonStore({{
                    fields: ['{0}', '{1}']
                }}),  
                mode: 'local',
                triggerAction: 'all',
                displayField: '{0}',
                valueField: '{1}',                
                forceSelection: true
            ", item.DisplayMember, item.ValueMember);            
        }

        public override void Visit(PKDateField item)
        {
            StrCode = "allowBlank: true";
        }

        public override void Visit(PKTextField item)
        {
            StrCode = "allowBlank: true";
        }

        public override void Visit(PKNumberField item)
        {
            StrCode = "allowBlank: true";
        }

        public override void Visit(PKTextAreaField item)
        {
            StrCode = "allowBlank: true, maxLength: 250";
        }

        public override void Visit(PKCheckField item)
        {
            StrCode = "allowBlank: true";
        }

        public override void Visit(PKRadioField item)
        {
            StrCode = "allowBlank: true";
        }
    }
}
