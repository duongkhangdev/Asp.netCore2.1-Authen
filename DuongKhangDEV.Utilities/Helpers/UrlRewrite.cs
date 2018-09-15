using System;
using System.Collections.Generic;
using System.Text;

namespace DuongKhangDEV.Utilities.Helpers
{
    public class UrlRewrite
    {
        private static readonly string[] a = new[]
                                             {
                                                 "à", "á", "ạ", "ả", "ã", "â", "ầ", "ấ", "ậ", "ẩ", "ẫ", "ă", "ắ", "ằ", "ắ",
                                                 "ặ", "ẳ", "ẵ", "a"
                                             };

        private static readonly string[] d = new[] { "đ", "d" };
        private static readonly string[] e = new[] { "è", "é", "ẹ", "ẻ", "ẽ", "ê", "ề", "ế", "ệ", "ể", "ễ", "e" };
        private static readonly string[] ii = new[] { "ì", "í", "ị", "ỉ", "ĩ", "i" };
        private static readonly string[] y = new[] { "ỳ", "ý", "ỵ", "ỷ", "ỹ", "y" };

        private static readonly string[] o = new[]
                                                 {
                                                 "ò", "ó", "ọ", "ỏ", "õ", "ô", "ồ", "ố", "ộ", "ổ", "ỗ", "ơ", "ờ", "ớ", "ợ",
                                                 "ở", "ỡ", "o"
                                             };

        private static readonly string[] u = new[] { "ù", "ú", "ụ", "ủ", "ũ", "ừ", "ứ", "ự", "ử", "ữ", "u", "ư" };

        public static string GenKhongdau(string LongName)
        {
            string ret = "";
            string currentchar = "";
            int len = LongName.Length;
            if (LongName.Length > 0)
            {
                int i;
                for (i = 0; i < len; i++)
                {
                    currentchar = LongName.Substring(i, 1);
                    ret = ret + ChangeChar(currentchar);
                }
            }
            else
            {
                ret = "";
            }
            return ret;
        }

        public static string getShortUrl(string strInput)
        {
            string ret = "";
            string currentchar = "";
            strInput = strInput.Replace(" ", "-");
            strInput = strInput.Replace(")", "");
            strInput = strInput.Replace("(", "");
            strInput = strInput.Replace("*", "");
            strInput = strInput.Replace("[", "");
            strInput = strInput.Replace("]", "");
            strInput = strInput.Replace("}", "");
            strInput = strInput.Replace("{", "");
            strInput = strInput.Replace(">", "");
            strInput = strInput.Replace("<", "");
            strInput = strInput.Replace("=", "");
            strInput = strInput.Replace(":", "");
            strInput = strInput.Replace(",", "");
            strInput = strInput.Replace("'", "");
            strInput = strInput.Replace("\"", "");
            strInput = strInput.Replace("/", "");
            strInput = strInput.Replace("\\", "");
            strInput = strInput.Replace("#", "");
            strInput = strInput.Replace("&", "");
            strInput = strInput.Replace("?", "");
            strInput = strInput.Replace(";", "");
            strInput = strInput.ToLower();
            int len = strInput.Length;
            if (strInput.Length > 0)
            {
                int i;
                for (i = 0; i < len; i++)
                {
                    currentchar = strInput.Substring(i, 1);
                    ret = ret + ChangeChar(currentchar);
                }
            }
            else
            {
                ret = "";
            }
            return ret;
        }

        public static string GenShortName(string LongName, string aspx)
        {
            string ret = "";
            string currentchar = "";
            LongName = LongName.Replace(" ", "-");
            LongName = LongName.Replace(")", "");
            LongName = LongName.Replace("(", "");
            LongName = LongName.Replace("*", "");
            LongName = LongName.Replace("[", "");
            LongName = LongName.Replace("]", "");
            LongName = LongName.Replace("}", "");
            LongName = LongName.Replace("{", "");
            LongName = LongName.Replace(">", "");
            LongName = LongName.Replace("<", "");
            LongName = LongName.Replace("=", "");
            LongName = LongName.Replace(":", "");
            LongName = LongName.Replace(",", "");
            LongName = LongName.Replace("'", "");
            LongName = LongName.Replace("\"", "");
            LongName = LongName.Replace("/", "");
            LongName = LongName.Replace("\\", "");
            LongName = LongName.Replace("#", "");
            LongName = LongName.Replace("&", "");
            LongName = LongName.Replace("?", "");
            LongName = LongName.Replace(";", "");
            LongName = LongName.ToLower();
            int len = LongName.Length;
            if (LongName.Length > 0)
            {
                int i;
                for (i = 0; i < len; i++)
                {
                    currentchar = LongName.Substring(i, 1);
                    ret = ret + ChangeChar(currentchar);
                }
            }
            else
            {
                ret = "";
            }
            return ret + aspx.ToLower();
        }

        private static string ChangeChar(string charinput)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i].Equals(charinput))
                {
                    return "a";
                }
            }
            for (int i = 0; i < d.Length; i++)
            {
                if (d[i].Equals(charinput))
                {
                    return "d";
                }
            }
            for (int i = 0; i < e.Length; i++)
            {
                if (e[i].Equals(charinput))
                {
                    return "e";
                }
            }
            for (int i = 0; i < ii.Length; i++)
            {
                if (ii[i].Equals(charinput))
                {
                    return "i";
                }
            }
            for (int i = 0; i < y.Length; i++)
            {
                if (y[i].Equals(charinput))
                {
                    return "y";
                }
            }
            for (int i = 0; i < o.Length; i++)
            {
                if (o[i].Equals(charinput))
                {
                    return "o";
                }
            }
            for (int i = 0; i < u.Length; i++)
            {
                if (u[i].Equals(charinput))
                {
                    return "u";
                }
            }
            return charinput;
        }
    }
}
