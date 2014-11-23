namespace Bdz.Models
{
    public class TrainTransition
    {
        public TrainTransition(string from, string to, string trainNumber, string departureTime, string arrivalTime)
        {
            this.DepartureTime = departureTime;
            this.ArrivalTime = arrivalTime;
            this.To = to;
            this.From = from;
            this.TrainNumber = trainNumber;
        }

        public string From { get; set; }

        public string To { get; set; }

        public string DepartureTime { get; set; }

        public string ArrivalTime { get; set; }

        public string TrainNumber { get; set; }
    }
}
