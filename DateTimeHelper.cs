using System;
using System.Collections.Generic;

namespace Shinetech.Common
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// Date to time stamp (time stamp in seconds)
        /// </summary>
        /// <param name="TimeStamp"></param>
        /// <returns></returns>
        public static long ConvertToTimeStamp(this DateTime time)
        {
            DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(time.AddHours(-8) - Jan1st1970).TotalSeconds;
        }

        /// <summary>
        /// Timestamps converted to dates (timestamps in seconds)
        /// </summary>
        /// <param name="TimeStamp"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(this long timeStamp)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return start.AddSeconds(timeStamp).AddHours(8);
        }

        /// <summary>
        ///Get current timestamp (seconds)
        /// </summary>
        public static long GetNowToTimeStamp { get { return DateTime.Now.ConvertToTimeStamp(); } }

        public static IEnumerable<string> GetDateList(DateTime date)
        {
            var day = date.Day;
            var list = new List<string>();
            for (int i = 1; i <= 10; i++)
            {
                if (i == 1)
                {
                    string item;
                    if (day >= 15)
                    {
                        item = date.ToString("yyyyMM15");
                        list.Add(LastDayOfMonth(date).ToString("yyyyMMdd"));
                        list.Add(item);
                    }
                    else
                    {
                        item = LastDayOfPreviousMonth(date).ToString("yyyyMMdd");
                        list.Add(date.ToString("yyyyMM15"));
                        list.Add(item);
                        list.Add(LastDayOfPreviousMonth(date).ToString("yyyyMM15"));
                    }
                }
                else
                {
                    if (day >= 15)
                    {
                        var newDate = date.AddMonths(-i + 1);
                        var item = newDate.ToString("yyyyMM15");
                        var item1 = LastDayOfMonth(newDate).ToString("yyyyMMdd");
                        list.Add(item1);
                        list.Add(item);
                    }
                    else
                    {
                        var newDate = date.AddMonths(-i);
                        list.Add(newDate.ToString($"yyyyMM{LastDayOfMonth(newDate).Day}"));
                        list.Add(newDate.ToString("yyyyMM15"));
                    }
                }
            }

            return list;
        }
        /// <summary>
        /// 取得某月的第一天
        /// </summary>
        /// <param name="datetime">要取得月份第一天的时间</param>
        /// <returns></returns>
        public static DateTime FirstDayOfMonth(this DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day);
        }

        /// <summary>
        /// 取得某月的最后一天
        /// <param name="datetime">要取得月份最后一天的时间</param>
        /// <returns></returns>
        /// </summary>
        public static DateTime LastDayOfMonth(this DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// 取得上个月第一天
        /// </summary>
        /// <param name="datetime">要取得上个月第一天的当前时间</param>
        /// <returns></returns>
        public static DateTime FirstDayOfPreviousMonth(this DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).AddMonths(-1);
        }

        /// <summary>
        /// 取得上个月的最后一天
        /// </summary>
        /// <param name="datetime">要取得上个月最后一天的当前时间</param>
        /// <returns></returns>
        public static DateTime LastDayOfPreviousMonth(this DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).AddDays(-1);
        }
    }
}
