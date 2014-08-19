using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Ext.Component;
using PKExtFramework.Ext.Component.Fields;
using PKExtFramework.Ext.Component.Panel;
using PKExtFramework.Ext.Component.Grid;
using PKExtFramework.Ext.Component.Tab;

namespace PKExtFramework.Visitors
{
    public abstract class PKVisitor
    {
        public abstract void Visit(PKControl item);

        public abstract void Visit(PKContainer item);

        public abstract void Visit(PKPanel item);

        public abstract void Visit(PKField item);

        public abstract void Visit(PKGrid item);

        public abstract void Visit(PKGridColumnRow item);

        public abstract void Visit(PKText item);

        public abstract void Visit(PKButton item);

        public abstract void Visit(PKColumn item);

        public abstract void Visit(PKFieldItem item);

        public abstract void Visit(PKPanelHeader item);

        public abstract void Visit(PKPanelFooter item);

        public abstract void Visit(PKToolbar item);

        public abstract void Visit(PKBottombar item);

        public abstract void Visit(PKTabPanel item);

        public abstract void Visit(PKTabBar item);
        
        public abstract void Visit(PKTabPanelBody item);        
    }
}
