using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PKExtFramework.Ext.Drawing
{
    public class PKPens
    {
        public static readonly PKPen GrayPen = new PKPen(new PKColor(238, 238, 238, 255), 1);
        public static readonly PKPen DarkGrayPen = new PKPen(new PKColor(102, 102, 102, 255), 1);
        public static readonly PKPen WhitePen = new PKPen(new PKColor(255, 255, 255, 255), 1);
        public static readonly PKPen BlackPen = new PKPen(new PKColor(0, 0, 0, 255), 1);
        public static readonly PKPen BlackPen2 = new PKPen(new PKColor(0, 0, 0, 255), 2);
        
        public static readonly PKPen BluePen = new PKPen(new PKColor(0, 0, 255, 255), 1);
        public static readonly PKPen BluePen2 = new PKPen(new PKColor(0, 0, 255, 255), 2);
        public static readonly PKPen BluePen3 = new PKPen(new PKColor(21, 137, 255, 255), 3);
        public static readonly PKPen SteelBluePen = new PKPen(new PKColor(21, 137, 255, 255), 1);
        public static readonly PKPen SelectedItem = new PKPen(new PKColor(255, 0, 0, 255), 2)
        { 
            DashStyle = 4
        };
        public static readonly PKPen ActiveItem = new PKPen(new PKColor(255, 128, 0, 255), 2)
        {
            DashStyle = 4
        };

        public static readonly PKPen BorderPen = new PKPen(new PKColor(21, 137, 255, 255), 1);
    }
}
