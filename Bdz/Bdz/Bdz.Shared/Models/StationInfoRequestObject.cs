namespace Bdz.Models
{
    public class StationInfoRequestObject
    {
        public TrainItem[] Departure { get; set; }

        public TrainItem[] Arrival { get; set; }
    }
}
