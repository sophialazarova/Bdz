namespace Bdz.ViewModels
{
    using Bdz.Models;
    using Bdz.Utilities;
    using GalaSoft.MvvmLight;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;

    public class ListStationInfoViewModel : ViewModelBase
    {
        private DateTimeOffset date;

        public ListStationInfoViewModel()
        {
            this.Station = ListStationInfoHelper.Station;
            this.date = ListStationInfoHelper.Date;
            this.Date = this.date.Day + "/" + this.date.Month + "/" + this.date.Year;
            this.Departure = ListStationInfoHelper.StationInfo.Departure;
            this.Arrival = ListStationInfoHelper.StationInfo.Arrival;
        }

        public string Station { get; set; }

        public string Date { get; private set; }

        public TrainItem[] Arrival { get; private set; }

        public TrainItem[] Departure { get; private set; }

    }
}
