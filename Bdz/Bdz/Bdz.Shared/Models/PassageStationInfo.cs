namespace Bdz.Models
{
    using System;

    public class PassageStationInfo
    {
        public PassageStationInfo(string station, DateTimeOffset date, StationInfoRequestObject info)
        {
            this.Station = station;
            this.Date = date;
            this.Info = info;
        }

        public string Station { get; private set; }

        public DateTimeOffset Date { get; private set; }

        public StationInfoRequestObject Info { get; private set; }
    }
}
