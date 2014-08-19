using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using PKExtFramework.Ext.Layout;

namespace PKExtFramework.TypeConverters
{
    internal class LayoutConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(PKLayout))
                return true;
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, 
            System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(System.String) && value is PKLayout)
            {
                PKLayout so = (PKLayout)value;

                return so.Name();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, 
            System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                try
                {
                    string s = (string)value;
                    if(s=="auto")
                    {
                        return new PKLayout();
                    }
                    if (s == "form")
                    {
                        return new PKForm();
                    }
                    if (s == "fit")
                    {
                        return new PKFit();
                    }
                    if (s == "hbox")
                    {
                        return new PKHBox();
                    }
                    if (s == "vbox")
                    {
                        return new PKVBox();
                    }
                    if (s == "card")
                    {
                        return new PKCard();
                    }
                }
                catch
                {
                    throw new ArgumentException(
                        "Can not convert '" + (string)value +
                                           "' to type SpellingOptions");
                }
            }
            return base.ConvertFrom(context, culture, value);
        }


        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(
                new PKLayout[]{
                    new PKLayout(),
                    new PKCard(),
                    new PKFit(),
                    new PKForm(),
                    new PKHBox(),
                    new PKVBox()
                });
        }
    }
}
