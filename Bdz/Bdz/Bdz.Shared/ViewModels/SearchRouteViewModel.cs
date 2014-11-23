namespace Bdz.ViewModels
{
    using Bdz.LocalDB;
    using Bdz.Models;
    using Bdz.Pages;
    using Bdz.Utilities;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public class SearchRouteViewModel : ViewModelBase
    {
        private ICommand onSearchButtonClick;
        private RemoteDataManager remoteManager;
        private RouteInfoRequestObject currentRouteInfo;
        private DateTimeOffset date = DateTime.Today;

        public SearchRouteViewModel()
        {
            this.remoteManager = new RemoteDataManager();
        }

        public string From { get; set; }

        public string To { get; set; }

        public DateTimeOffset Date
        {
            get
            {
                return this.date;
            }
            set
            {
                this.date = value;
            }
        }

        public ICommand OnSearchButtonClick
        {
            get
            {
                if (this.onSearchButtonClick == null)
                {
                    this.onSearchButtonClick = new RelayCommand<FrameworkElement>( param => this.SearchForRoute(param));
                }

                return this.onSearchButtonClick;
            }
        }

        private void SearchForRoute(FrameworkElement useControl)
        {
            this.HandleSearchAsync(useControl);
        }

        private async void HandleSearchAsync(FrameworkElement userControl)
        {
            this.currentRouteInfo = await remoteManager.GetRouteInfo(this.From, this.To, this.Date.Day + "/" + this.Date.Month + "/" + this.Date.Year);
           
            ViewDataTransferHelper.RouteArrivalStation = this.To;
            ViewDataTransferHelper.RouteDepartureStation = this.From;
            ViewDataTransferHelper.RouteDate = this.Date;
            ViewDataTransferHelper.RouteInfo = this.currentRouteInfo;

            var frame = (userControl as Page).Frame;
            frame.Navigate(typeof(ListRoutes));
            
        }
    }
}
