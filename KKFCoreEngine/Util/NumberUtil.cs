using System;
using System.Collections.Generic;
using System.Text;

namespace KKFCoreEngine.Util
{
    public static class NumberUtil
    {
        public static int? GetID(this int val)
        {
            if (val == 0) return null;
            return val;
        }

        public static int? GetID(this int? val)
        {
            if (val == 0) return null;
            return val;
        }

        public static int? ParseInt(this string value)
        {
            if (value == null || value.Trim() == string.Empty)
            {
                return null;
            }
            else
            {
                try
                {
                    return int.Parse(value);
                }
                catch
                {
                    return null;
                }
            }
        }

        public static int? ParseInt(this object value)
        {
            if (value == null || value.ToString().Trim() == string.Empty)
            {
                return null;
            }
            else
            {
                try
                {
                    return int.Parse(value.ToString());
                }
                catch
                {
                    return null;
                }
            }
        }
               
    }
}
