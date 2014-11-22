namespace Bdz.Models
{
    using System.Collections.Generic;

    public class RouteItem
    {
        public RouteItem(string departureTime, string arrivalTime, string duration, int transitions, List<TransitionItem> details)
        {
            this.DepartureTime = departureTime;
            this.ArrivalTime = arrivalTime;
            this.Duration = duration;
            this.Details = details;
            this.Transitions = transitions;

        }

        public string DepartureTime { get; set; }

        public string ArrivalTime { get; set; }

        public string Duration { get; set; }

        public int Transitions { get; set; }

        public IList<TransitionItem> Details { get; set; }
    }
}
