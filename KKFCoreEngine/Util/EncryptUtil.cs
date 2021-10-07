using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace KKFCoreEngine.Util
{
    public static class EncryptUtil
    {
        public static string GenerateSHA256String(string inputString)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        public static string GenerateSHA512String(string inputString)
        {
            SHA512 sha512 = SHA512Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha512.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }

        public static string NewID(this string secret)
        {
            string hash = "";
            using (var sha256 = SHA256.Create())
            {
                var key = $"{Guid.NewGuid()}+{secret}";
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
                hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
            return hash;
        }

        public static string Hash(this string key)
        {
            string hash = "";
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
                hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
            return hash;
        }

        public static string MD5(this string s)
        {
            using (var provider = System.Security.Cryptography.MD5.Create())
            {
                StringBuilder builder = new StringBuilder();

                foreach (byte b in provider.ComputeHash(Encoding.UTF8.GetBytes(s)))
                    builder.Append(b.ToString("x2").ToLower());

                return builder.ToString();
            }
        }

        public static string GetRunningCode(this int num, int digi)
        {
            string codeTmp = "";
            int max = 10 + 26;
            int val = num;
            do
            {
                num = val % max;
                val = (int)(val / max);
                codeTmp = string.Concat(num < 10 ? num.ToString() : Convert.ToChar(num + 55).ToString(), codeTmp);
            } while (val > 0);

            for(int i = codeTmp.Length; i < digi; i++ )
            {
                codeTmp = string.Concat("0", codeTmp);
            }
            return codeTmp;
        }

        public static string GetCodeRunning(this int num, int digi)
        {
            string codeTmp = num.ToString() + "";
            for (int i = codeTmp.Length; i < digi; i++)
            {
                codeTmp = "0" + codeTmp;
            }
            return codeTmp;
        }

        public static string GetRunningCode2Digi(this int num)
        {
            string codeTmp = "";
            int max = 26;
            int val = num;
            int round = 0;
            if(num > 99)
            {
                val = num = num - 100;
                do
                {
                    round++;
                    num = val % max;
                    val = val / max;
                    if (round > 1) num--;
                    codeTmp = Convert.ToChar(num + 65) + codeTmp;
                } while (val > 0);
            } 
            else
            {
                codeTmp = num.ToString();                
            }

            return codeTmp.Length == 1 ? '0' + codeTmp : codeTmp;


        }
        public static string ENDCodeNEW(string d)
        {
            string strInput = d.ToUpper();
            string strOutput = "";

            char[] charInput = strInput.ToCharArray();
            char[] charOutput = new char[strInput.Length];

            for (int i = 0; i < charInput.Length; i++)
            {

                charOutput[i] = Convert.ToChar((int)charInput[i] + 5);
            }


            strOutput = new string(charOutput);

            return strOutput;
        }
    }
}
