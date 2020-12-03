using System;

namespace Shinetech.Common.Helper
{
    public class RandomHelper
    {
        public static string RandomString(int intlen, bool specificChar = false)
        {
            string strPwChar = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (true)
            {
                // comment this because this may error
                //strPwChar += "@#$%^&*";
            }
            string strRe = "_";
            int iRandNum;
            Random rnd = new Random();
            for (int i = 0; i < intlen; i++)
            {
                iRandNum = rnd.Next(strPwChar.Length);
                strRe += strPwChar[iRandNum];
            }
            return strRe;
        }
    }
}
