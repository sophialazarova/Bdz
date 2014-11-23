namespace Bdz.ViewModels
{
    using Bdz.Utilities;
    using GalaSoft.MvvmLight;
    using System;

    public class ListRoutesViewModel : ViewModelBase
    {
        private DateTimeOffset date;
        public ListRoutesViewModel()
        {
            this.DepartureStation = ViewDataTransferHelper.RouteDepartureStation;
            this.ArrivalStation = ViewDataTransferHelper.RouteArrivalStation;
            this.date = ViewDataTransferHelper.RouteDate;
            this.Date = date.Day + "/" + date.Month + "/" + date.Year;
        }
        public string DepartureStation { get; private set; }

        public string ArrivalStation { get; private set; }

        public string Date { get; set; }
    }
}
