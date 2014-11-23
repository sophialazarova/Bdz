using Bdz.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bdz.Utilities
{
   public static class  ListStationInfoHelper
    {
       public static string Station { get; set; }

       public static DateTimeOffset Date { get; set; }

       public static StationInfoRequestObject StationInfo { get; set; }
    }
}
