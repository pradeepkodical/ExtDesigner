using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PKExtFramework.Utility
{
    public class PKSequenceGenerator
    {
        private static int idx = 0;
        public static string GetNextGUIId()
        {
            return "_" + (++idx).ToString();
        }

        public static string GetNextId()
        {
            return (DateTime.Now.Ticks + ++idx).ToString();
        }
    }
}
