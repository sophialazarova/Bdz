namespace Bdz.ViewModels
{
    using Bdz.Models;
    using Bdz.Pages;
    using Bdz.Utilities;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    public class SearchStationViewModel : ViewModelBase
    {
        private ICommand commandGetInformationForStation;
        private RemoteDataManager remoteManager;
        private DateTimeOffset date = DateTime.Today;
        private StationInfoRequestObject searchAnswer;

        public SearchStationViewModel()
        {
            this.remoteManager = new RemoteDataManager();
            this.searchAnswer = new StationInfoRequestObject();

        }

        public string Station { get; set; }

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

      
        public ICommand GetInformationForStation
        {
            get
            {
                if (this.commandGetInformationForStation == null)
                {
                    this.commandGetInformationForStation = new RelayCommand<FrameworkElement>(param => this.ExecuteSearchCommand(param));
                }

                return this.commandGetInformationForStation;
            }
        }

        private void ExecuteSearchCommand(FrameworkElement userControl)
        {
            this.ExecuteRequestAsync(userControl);
        }

        private async void ExecuteRequestAsync(FrameworkElement userControl)
        {
           var result = await this.remoteManager.GetStationInfo(this.Station, this.Date.Day +"/" + this.Date.Month + "/" + this.Date.Year);
           var response = await result.Content.ReadAsStringAsync();
           this.searchAnswer = JsonConvert.DeserializeObject<StationInfoRequestObject>(response);

            //setting data to the helper class so that the next viewmodel can be populated
           ViewDataTransferHelper.Station = this.Station;
           ViewDataTransferHelper.StationDetailsDate = this.Date;
           ViewDataTransferHelper.StationInfo = this.searchAnswer;

           var frame = (userControl as Page).Frame;
           frame.Navigate(typeof(ListStationInfo));
        }   

    }
}
