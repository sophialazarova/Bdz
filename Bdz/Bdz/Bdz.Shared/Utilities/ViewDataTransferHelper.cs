namespace Bdz.Utilities
{
    using Bdz.Models;
    using System;

   public static class  ViewDataTransferHelper
    {
       /// <summary>
       /// Used to pass to the next view the chosen station from <Station Info Search> page
       /// </summary>
       public static string Station { get; set; }

       public static DateTimeOffset StationDetailsDate { get; set; }

       public static StationInfoRequestObject StationInfo { get; set; }

       public static RouteInfoRequestObject RouteInfo { get; set; }

       public static string RouteDepartureStation { get; set; }

       public static string RouteArrivalStation { get; set; }

       public static DateTimeOffset RouteDate { get; set; }

       public static RouteItem SelectedItem { get; set; }
    }
}
