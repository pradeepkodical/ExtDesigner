using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PKExtFramework.Ext.Drawing
{
    public class PKBrushes
    {
        public static readonly PKBrush WhiteBrush = 
            new PKSolidBrush(new PKColor(255, 255, 255, 255), 1);
        public static readonly PKBrush BlackBrush = 
            new PKSolidBrush(new PKColor(0, 0, 0, 255), 1);

        public static readonly PKBrush GrayBlackBrush =
            new PKLinearGradientBrush(
                new PKColor(102, 102, 102, 255), new PKColor(51, 51, 51, 255), 1);

        public static readonly PKBrush GrayWhiteBrush = 
            new PKLinearGradientBrush(
                new PKColor(238, 238, 238, 255), new PKColor(255, 255, 255, 255), 1);
        public static readonly PKBrush WhiteGrayBrush = 
            new PKLinearGradientBrush(
                new PKColor(255, 255, 255, 255), new PKColor(238, 238, 238, 255), 1);
        
        public static readonly PKBrush SteelBlueBrush = 
            new PKLinearGradientBrush( 
                new PKColor(21, 137, 255, 255),new PKColor(21, 137, 255, 150), 1);

        public static readonly PKBrush ButtonBackgroundBrush =
            new PKLinearGradientBrush(
                new PKColor(238, 238, 238, 255), new PKColor(255, 255, 255, 255), 1);
        public static readonly PKBrush BackgroundThemeBrush =
            new PKLinearGradientBrush(
                new PKColor(21, 137, 255, 255), new PKColor(21, 137, 255, 150), 1);
        public static readonly PKBrush HeaderBrush =
            new PKLinearGradientBrush(
                new PKColor(255, 255, 255, 255), new PKColor(238, 238, 238, 255), 1);
    }
}
