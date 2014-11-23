namespace Bdz.ViewModels
{
    using Bdz.Models;
    using Bdz.Utilities;
    using System;
    using System.Collections.Generic;

    public class RouteDetailsViewModel
    {
        private DateTimeOffset date;
        private RouteItem routeItem;
        private IList<TrainTransition> route;
        public RouteDetailsViewModel()
        {
            this.DepartureStation = ViewDataTransferHelper.RouteDepartureStation;
            this.ArrivalStation = ViewDataTransferHelper.RouteArrivalStation;
            this.date = ViewDataTransferHelper.RouteDate;
            this.Date = date.Day + "/" + date.Month + "/" + date.Year;
            this.routeItem = ViewDataTransferHelper.SelectedItem;
            this.Route = this.ParseRouteItemDetails(this.routeItem.Details);
            this.ArrivalTime = this.routeItem.ArrivalTime;
            this.DepartureTime = this.routeItem.DepartureTime;
            this.Duration = this.routeItem.TripDuration;
            this.Transitions = this.routeItem.Transitions;

        }

        public string DepartureStation { get; private set; }

        public string ArrivalStation { get; private set; }

        public string Date { get; private set; }


        public string ArrivalTime { get; private set; }

        public string DepartureTime { get; private set; }

        public string Transitions { get; private set; }

        public string Duration { get; private set; }

        public IList<TrainTransition> Route { get; private set; }

        private IList<TrainTransition> ParseRouteItemDetails(string[] details){
            IList<TrainTransition> transitions = new List<TrainTransition>();
            for (int i = 0; i < details.Length-4; i += 5)
            {
                transitions.Add(new TrainTransition(details[i],details[i+1],details[i+2], details[i+3],details[i+4]));
            }

            return transitions;
        }
    }
}
