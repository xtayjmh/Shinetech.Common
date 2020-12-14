using System;

namespace Shinetech.Common.Helper
{
    public static class CommonHelper
    {
        /// <summary>
        /// 将传入的字符串中间部分字符替换成特殊字符
        /// </summary>
        /// <param name="value">需要替换的字符串</param>
        /// <param name="startLen">前保留长度</param>
        /// <param name="endLen">尾保留长度</param>
        /// <param name="specialChar">特殊字符</param>
        /// <returns>被特殊字符替换的字符串</returns>
        public static string ReplaceWithSpecialChar(string value, int startLen = 4, int endLen = 4,
            char specialChar = '*')
        {
            int length = value.Length - startLen - endLen;

            string replaceStr = value.Substring(startLen, length);

            string specialStr = string.Empty;

            for (int i = 0; i < replaceStr.Length; i++)
            {
                specialStr += specialChar;
            }

            value = value.Replace(replaceStr, specialStr);

            return value;
        }
    }
}