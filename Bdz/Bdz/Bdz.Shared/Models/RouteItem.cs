namespace Bdz.Models
{
    using System.Collections.Generic;

    public class RouteItem
    {
        public string DepartureTime { get; set; }

        public string ArrivalTime { get; set; }

        public string TripDuration { get; set; }

        public string Transitions { get; set; }

        public string[] Details { get; set; }
    }
}
