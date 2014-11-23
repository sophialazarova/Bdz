namespace Bdz.Models
{
    using System.Collections.Generic;

    public class RouteItem
    {
        public RouteItem(string departureTime, string arrivalTime,
            string tripDuration, string transitions, string[] details)
        {
            this.DepartureTime = departureTime;
            this.ArrivalTime = arrivalTime;
            this.Transitions = transitions;
            this.TripDuration = tripDuration;
            this.Details = details;
        }

        public string DepartureTime { get; set; }

        public string ArrivalTime { get; set; }

        public string TripDuration { get; set; }

        public string Transitions { get; set; }

        public string[] Details { get; set; }
    }
}
