using System;
using System.Linq;


public static class StringExt
{

    /// <summary>
    /// First Char To Lower
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string FirstCharToLower(this string input)
    {
        if (String.IsNullOrEmpty(input))
            return input;
        string str = input.First().ToString().ToLower() + input.Substring(1);
        return str;
    }

    /// <summary>
    /// First Char To Upper
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string FirstCharToUpper(this string input)
    {
        if (String.IsNullOrEmpty(input))
            return input;
        string str = input.First().ToString().ToUpper() + input.Substring(1);
        return str;
    }
    public static string ToAzureSafeString(this string input)
    {
        if (String.IsNullOrEmpty(input))
            return input;
        string str = input.Replace(" ", "").Replace("_", "").Replace("-", "").ToLower();
        return str;
    }
}

