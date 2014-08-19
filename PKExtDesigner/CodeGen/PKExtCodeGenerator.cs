using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Ext.Component;
using PKExtDesigner.Visitors;
using System.IO;
using System.Diagnostics;
using PKExtDesigner.Visitors.Code;

namespace PKExtDesigner.CodeGen
{
    internal class PKExtCodeGenerator
    {
        private static string GenerateCode(PKControl mainApp)
        {
            PKExt3CodeGenVisitor visitor = new PKExt3CodeGenVisitor();
            mainApp.Accept(visitor);
            return new JSBeautify(visitor.Code, new JSBeautifyOptions { 
                indent_char = ' ',
                indent_level = 1,
                indent_size = 4,
                preserve_newlines = false
            }).GetResult();            
        }

        internal static void Generate(PKControl app)
        {
            string AppName = app.Name;
            string AppScript = PKExtCodeGenerator.GenerateCode(app);

            var strCode =
            string.Format(@"<html>
            <head>
                <meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1'>
                <meta http-equiv='x-ua-compatible' content='IE=Edge' />
                <title>PK Ext Designer Test</title>
                <link rel='stylesheet' type='text/css' href='http://extjs-public.googlecode.com/svn/tags/extjs-3.4.0/release/resources/css/ext-all.css' />
                <script type='text/javascript' src='http://ajax.googleapis.com/ajax/libs/jquery/1.8.1/jquery.min.js'></script>

                <script type='text/javascript' src='http://extjs-public.googlecode.com/svn/tags/extjs-3.4.0/release/adapter/ext/ext-base.js'></script>
                <script type='text/javascript' src='http://extjs-public.googlecode.com/svn/tags/extjs-3.4.0/release/ext-all.js'></script>
                <script type='text/javascript' src='C:\\{0}.js'></script>
                <script type='text/javascript'>
                    Ext.onReady(function(){{                       
                                                
            	        var app = new {0}({{
                            style: 'padding: 5px;',
                            region: 'center'
                        }});                        

                        new Ext.Viewport({{
                            padding: 10,
                            layout: 'border',
                            items: [app]
                        }});
	                }});
                </script>
            </head>
            <body>
	            <div id='app'></div>
            </body>
            </html>", AppName);

            File.WriteAllText(string.Format("C:\\{0}.js", AppName), AppScript);
            File.WriteAllText("C:\\t.html", strCode);

            Process.Start("C:\\t.html");

        }
    }
}
