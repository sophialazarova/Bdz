namespace Bdz.Models
{
    public class TransitionItem
    {
        public TransitionItem(string from, string to, string arrivalTime, string departureTime, string trainNumber)
        {
            this.ArrivalTime = arrivalTime;
            this.DepartureTime = departureTime;
            this.From = from;
            this.To = to;
            this.TrainNumber = trainNumber;
        }

        public string From { get; private set; }

        public string To { get; private set; }

        public string TrainNumber { get; private set; }

        public string DepartureTime { get; private set; }

        public string ArrivalTime { get; private set; }
    }
}
