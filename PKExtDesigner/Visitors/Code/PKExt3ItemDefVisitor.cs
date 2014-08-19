using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Visitors;
using PKExtFramework.Ext.Component;
using PKExtFramework.Ext.Component.Fields;
using PKExtFramework.Ext.Component.Panel;
using PKExtFramework.Ext.Component.Tab;
using PKExtFramework.Ext.Component.Grid;

namespace PKExtDesigner.Visitors.Code
{
    internal class PKExt3ItemDefVisitor: PKVisitor
    {
        public string Code { get; set; }

        public PKExt3ItemDefVisitor()
        {
            this.Code = string.Empty;
        }

        public override void Visit(PKControl item)
        {
        }

        public override void Visit(PKContainer item)
        {
            string strItems = string.Empty;
            string strDimension = string.Empty;
            string strLayoutConfig = string.Empty;

            item.Items.ToList().ForEach(x =>
            {
                var visitor = new PKExt3ItemDefVisitor();
                x.Accept(visitor);
                this.Code += visitor.Code;
                strItems += string.Format("this.{0},", x.Name);
            });

            if (strItems.Length > 0)
            {
                strItems = strItems.Remove(strItems.Length - 1, 1);
            }

            if (item.Flex > 0)
            {
                strDimension = string.Format("flex: {0},", item.Flex);
            }
            else
            {
                if (item.Parent.Layout.Name() == "vbox")
                {
                    strDimension = string.Format("height: {0},", item.Height);
                }
                if (item.Parent.Layout.Name() == "hbox")
                {
                    strDimension = string.Format("width: {0},", item.Width);
                }
            }

            if(item.Layout.Name() == "vbox" || item.Layout.Name() == "hbox" )
            {
                strLayoutConfig = "layoutConfig: {align: 'stretch'},";
            }

            Code += string.Format(@"this.{0} = new Ext.Container({{
                xtype: 'container',
                style: 'padding: {5}px;',
                layout : '{1}',
                {2}
                {3}
                items: [{4}]                
                }});", 
                     item.Name, 
                     item.Layout.Name(), 
                     strLayoutConfig, 
                     strDimension, 
                     strItems, 
                     item.Padding);            
        }

        public override void Visit(PKBottombar item)
        {
            item.Items.ToList().ForEach(x =>
            {
                var visitor = new PKExt3CodeGenVisitor();
                x.Accept(visitor);
                Code += visitor.Code + ",";
            });
            if (Code.Length > 0)
            {
                Code = Code.Remove(Code.Length - 1, 1);
            }
        }

        public override void Visit(PKToolbar item)
        {
            item.Items.ToList().ForEach(x =>
            {
                var visitor = new PKExt3CodeGenVisitor();
                x.Accept(visitor);
                Code += visitor.Code + ",";
            });
            if (Code.Length > 0)
            {
                Code = Code.Remove(Code.Length - 1, 1);
            }
        }

        public override void Visit(PKPanelFooter item)
        {
            item.Items.ToList().ForEach(x =>
            {
                var visitor = new PKExt3CodeGenVisitor();
                x.Accept(visitor);
                Code += visitor.Code + ",";
            });
            if (Code.Length > 0)
            {
                Code = Code.Remove(Code.Length - 1, 1);
            }
        }
                
        public override void Visit(PKTabPanel item)
        {
            string strDimension = "";

            var bvisitor = new PKExt3CodeGenVisitor();
            
            item.Body.Accept(bvisitor);
            

            if (item.Flex > 0)
            {
                strDimension = string.Format("flex: {0},", item.Flex);
            }
            else
            {
                if (item.Parent.Layout.Name() == "vbox")
                {
                    strDimension = string.Format("height: {0},", item.Height);
                }
                if (item.Parent.Layout.Name() == "hbox")
                {
                    strDimension = string.Format("width: {0},", item.Width);
                }
            }

            Code = string.Format(@"new Ext.TabPanel({{                
                style: 'padding: {2}px;',
                {0}                                
                items: [{1}]
            }})",
                strDimension,
                bvisitor.Code,                
                item.Padding);
        }

        public override void Visit(PKTabBar item)
        {
            
        }

        public string VisitPKTab(PKTab item)
        {
            string strCode = string.Empty;
            var childItem = item.Items.FirstOrDefault();

            if (childItem != null)
            {
                var visitor = new PKExt3CodeGenVisitor();                
                childItem.Accept(visitor);
                strCode = visitor.Code;                
            }
            return string.Format(@"new Ext.Container({{ 
                    title: '{0}',
                    layout: 'fit',
                    items: [{1}]
                }})", item.Text, strCode);
        }

        public override void Visit(PKTabPanelBody item)
        {
            item.Items.ToList().ForEach(x => {
                Code += VisitPKTab(x as PKTab) +",";
            });
            if (Code.Length > 0)
            {
                Code = Code.Remove(Code.Length - 1, 1);
            }
        }

        public override void Visit(PKPanel item)
        {
            string strDimension = "";

            var hvisitor = new PKExt3CodeGenVisitor();
            var tvisitor = new PKExt3CodeGenVisitor();
            var bvisitor = new PKExt3CodeGenVisitor();
            var fvisitor = new PKExt3CodeGenVisitor();
            item.Header.Accept(hvisitor);
            item.Toolbar.Accept(tvisitor);
            item.Body.Accept(bvisitor);
            item.Footer.Accept(fvisitor);

            if (item.Flex > 0)
            {
                strDimension = string.Format("flex: {0},", item.Flex);
            }
            else
            {
                if (item.Parent.Layout.Name() == "vbox")
                {
                    strDimension = string.Format("height: {0},", item.Height);
                }
                if (item.Parent.Layout.Name() == "hbox")
                {
                    strDimension = string.Format("width: {0},", item.Width);
                }
            }

            Code = string.Format(@"new Ext.Panel({{                
                layout : 'fit',
                style: 'padding: {5}px;',
                {0}
                title: '{1}',
                tbar:[{2}],
                items: [{3}],
                buttons : [{4}]                
            }})", 
                strDimension, 
                hvisitor.Code, 
                tvisitor.Code,
                bvisitor.Code, 
                fvisitor.Code, 
                item.Padding);
        }

        public override void Visit(PKPanelHeader item)
        {
            Code = item.Value;
        }

        public override void Visit(PKField item)
        {
            var codeVisitor = new PKFieldCodeGenVisitor();
            item.ValueField.Accept(codeVisitor);
            Code = string.Format(@"new {0}({{
                width: {1},
                fieldLabel: '{2}',
                {3}
            }})", item.ValueField.ExtTypeName, 
                item.ValueField.Width,
                item.LabelField.Value.Replace(":", ""),                 
                codeVisitor.StrCode);
        }

        public override void Visit(PKGrid item)
        {
            string strDimension = "";
            var cvisitor = new PKExt3CodeGenVisitor();
            var tvisitor = new PKExt3CodeGenVisitor();
            var bvisitor = new PKExt3CodeGenVisitor();

            item.Toolbar.Accept(tvisitor);
            item.ColumnRow.Accept(cvisitor);
            item.Bottombar.Accept(bvisitor);

            if (item.Flex > 0)
            {
                strDimension = string.Format("flex: {0},", item.Flex);
            }
            else
            {
                if (item.Parent.Layout.Name() == "vbox")
                {
                    strDimension = string.Format("height: {0},", item.Height);
                }
                if (item.Parent.Layout.Name() == "hbox")
                {
                    strDimension = string.Format("width: {0},", item.Width);
                }
            }

            Code = string.Format(@"new Ext.grid.GridPanel({{                
                viewConfig: {{
                    forceFit: true,
                    sm: new Ext.grid.RowSelectionModel({{
                        singleSelect: true
                    }})
                }},
                store: new Ext.data.JsonStore({{}}),
                {0}
                title: '{1}',
                columns: [{2}],
                tbar:[{3}],
                bbar:[{4}]
            }})", 
                strDimension, 
                item.Header.Value, 
                cvisitor.Code,
                tvisitor.Code,
                bvisitor.Code);
        }

        public override void Visit(PKGridColumnRow item)
        {
            item.Cells.ToList().ForEach(x => {
                var visitor = new PKExt3CodeGenVisitor();
                x.Accept(visitor);
                Code += visitor.Code + ",";
            });
            if (Code.Length > 0)
            {
                Code = Code.Remove(Code.Length - 1, 1);
            }
        }

        public override void Visit(PKText item)
        {
            Code = string.Format(@"{{xtype:'label', text: '{0}'}}", item.Value);
        }

        public override void Visit(PKButton item)
        {
            Code = string.Format(@"{{xtype:'button', text: '{0}'}}", item.Value);
        }

        public override void Visit(PKColumn item)
        {
            Code = string.Format(@"{{
                dataIndex: '{0}',
                header: '{1}',
                width: {2}
            }}", item.DataProperty, item.Value, item.Width);
        }

        public override void Visit(PKFieldItem item)
        {
            
        }
    }
}
