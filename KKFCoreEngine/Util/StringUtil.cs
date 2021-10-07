using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace KKFCoreEngine.Util
{
    public static class StringUtil
    {
        public static string GetStringValue(this string val)
        {
            if (val != null && val != "")
            {
                val = val.Trim();
                if (val != "") { return val; }
            }
            return null;
        }

        public static string GetValue(this string val)
        {
            if (val != null && val != "")
            {
                val = val.Trim();
                if (val != "") { return val; }
            }
            return "";
        }

        public static string Join(string sp, List<string> val)
        {
            if (val != null) { return string.Join(sp, val); }
            return null;
        }

        public static string Join(this List<string> val, string sp)
        {
            if (val != null) { return string.Join(sp, val); }
            return null;
        }

        public static string Join(string sp, List<int> val)
        {
            if (val != null) { return string.Join(sp, val); }
            return null;
        }

        public static string Join(this List<int> val, string sp)
        {
            if (val != null) { return string.Join(sp, val); }
            return null;
        }
        public static string Join(this List<int?> val, string sp)
        {
            if (val != null && val.TrueForAll(x => !x.HasValue)) return null;
            if (val != null) { return string.Join(sp, val); }
            return null;
        }

        public static string Number(this decimal value, int? decimalx = null)
        {
            if (decimalx.HasValue)
            {
                value = Math.Round(value, decimalx.Value, MidpointRounding.AwayFromZero);
                return Regex.Replace(string.Format("{0:#,0.00000}", value), @"\b^([0-9,]*[.][0-9]{" + decimalx.Value + @"}).*$", "$1");
            }
            else
            {
                return string.Format("{0:#,0.#####}", value);
            }
        }
    }
}
