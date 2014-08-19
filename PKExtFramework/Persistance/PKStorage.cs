using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Ext.Component;
using PKExtFramework.Visitors;
using System.Web.Script.Serialization;
using PKExtFramework.Utility;
using System.IO;

namespace PKExtFramework.Persistance
{
    public class PKStorage
    {
        private static List<PKFlatItem> ConvertToFlatItems(PKBoxItem item)
        {
            PKFlatElementCreateVisitor visitor = new PKFlatElementCreateVisitor();
            item.Accept(visitor);
            visitor.FlatItems.Find(x => x.ID == item.ID).ParentID = null;
            return visitor.FlatItems;
        }

        private static PKBoxItem ConvertToBoxItem(List<PKFlatItem> items, bool newId)
        {
            if (newId)
            {
                items.ForEach(x => {
                    string oldId = x.ID;
                    x.ID = PKSequenceGenerator.GetNextId();
                    x.Name = "cmp" + PKSequenceGenerator.GetNextGUIId();
                    items.FindAll(y => y.ParentID == oldId).ForEach(y => y.ParentID = x.ID);
                });
            }

            items.ForEach(x =>
            {
                PKBoxItem item = null;
                if (x.IsComponent)
                {
                    string strCmp = File.ReadAllText(Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "Components"), x.ComponentFileName));
                    item = Deserialize(strCmp, true);
                }
                else
                {
                    item = (PKBoxItem)Activator.CreateInstance(Type.GetType(x.ClassName));
                }
                if (item != null)
                {
                    PKElementCreateVisitor visitor = new PKElementCreateVisitor(x);
                    item.Accept(visitor);
                    x.BoxItem = item;
                }
            });

            items.ForEach(x => {
                x.ParentItem = items.Find(y => y.ID == x.ParentID);
                if (x.ParentItem != null)
                {
                    PKElementParentVistor visitor = new PKElementParentVistor(items, x.BoxItem);
                    x.ParentItem.BoxItem.Accept(visitor);
                }
            });

            return items.Find(x=>x.ParentID == null).BoxItem;
        }

        public static string Serialize(PKBoxItem item)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(ConvertToFlatItems(item));
        }

        public static PKBoxItem Deserialize(string strString)
        {
            return Deserialize(strString, false);
        }

        public static PKBoxItem Deserialize(string strString, bool newId)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return ConvertToBoxItem(serializer.Deserialize<List<PKFlatItem>>(strString), newId);
        }

        public static PKControl DeserializeComponent(string strString)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var flatItems = serializer.Deserialize<List<PKFlatItem>>(strString);
            flatItems.Find(x => x.ParentID == null).IsComponent = false;
            return ConvertToBoxItem(flatItems, true) as PKControl;
        }
    }
}
