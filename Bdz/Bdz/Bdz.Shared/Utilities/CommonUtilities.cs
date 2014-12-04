namespace Bdz.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Text;

   public class CommonUtilities
    {
       public static string FormatDate(DateTimeOffset date)
        {
            string day = date.Day.ToString();
            string month = date.Month.ToString();
            if (day.Length == 1)
            {
                day = "0" + day;
            }

            if (month.Length == 1)
            {
                month = "0" + month;
            }

            string formatedDate = day + "/" + month + "/" + date.Year;
            return formatedDate;
        }
    }
}
