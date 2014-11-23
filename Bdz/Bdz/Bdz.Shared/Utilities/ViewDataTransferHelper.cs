using Bdz.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bdz.Utilities
{
   public static class  ViewDataTransferHelper
    {
       public static string Station { get; set; }

       public static DateTimeOffset StationDetailsDate { get; set; }

       public static StationInfoRequestObject StationInfo { get; set; }

       public static string RouteDepartureTime { get; set; }

       public static string RouteArrivalTime { get; set; }

       public static string RouteDuration { get; set; }

       public static int RouteTransitions { get; set; }

       public static List<TransitionItem> RouteDetails { get; set; }
    }
}
