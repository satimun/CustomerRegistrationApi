using KKFCoreEngine.Util;
using CustomerRegistrationModel.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CustomerRegistrationModel.Enum;

namespace CustomerRegistrationAPI.Constant
{
    public static class ServiceFunction
    {
        public static string GetErrorMessage(string msg)
        {
            //map error code, message
            ErrorCode error = EnumUtil.GetEnum<ErrorCode>(msg);
            string coderr = error.ToString();
            if (coderr == ErrorCode.U000.ToString())
            {
                if(new Regex(@"^The DELETE statement conflicted", RegexOptions.IgnoreCase).Match(msg).Success)
                {
                    return GenErrorMsg(ErrorCode.D000);
                } 
                else if (new Regex(@"^Violation of UNIQUE KEY constraint", RegexOptions.IgnoreCase).Match(msg).Success)
                {
                    return GenErrorMsg(ErrorCode.V001);
                }
                return string.Concat(coderr, ":", msg);
            }
            return GenErrorMsg(error);
        }

        public static string GenErrorMsg(ErrorCode error, string txt1 = null)
        {
            return string.Concat(error.ToString(), ":", string.Format(EnumUtil.GetDescription(error), txt1));
        }   
       

        public static string GetRangeSeq(List<string> arr)
        {
            string lastSeq = arr.First();
            for (int i = 1; i < arr.Count; i++)
            {
                if ((int.Parse(lastSeq) + 1).ToString() == arr[i])
                {
                    lastSeq = arr[i];
                    if (i + 1 < arr.Count)
                    {
                        arr[i] = arr[i + 1] == (int.Parse(lastSeq) + 1).ToString() ? "" : arr[i];
                    }
                }
                else
                {
                    lastSeq = arr[i];
                }
            }
            string res = string.Join(",", arr);
            return Regex.Replace(res, @"[,]{2,}", " - ");
        }

        public static string GenMsgDesNotify(NotifyMessage message, string refkey, string txt1 = null, string txt2 = null, string txt3 = null, string txt4 = null)
        {
            return string.Format(EnumUtil.GetDescription(message), refkey, txt1, txt2, txt3, txt4);
        }
    }
}
