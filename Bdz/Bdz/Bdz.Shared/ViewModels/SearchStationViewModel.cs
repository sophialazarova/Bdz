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
        private ObservableCollection<TrainItem> trains;
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

        public IEnumerable<TrainItem> Trains
        {
            get
            {
                if (this.trains == null)
                {
                    this.trains = new ObservableCollection<TrainItem>();
                }

                return this.trains;
            }
            set
            {
                if (this.trains == null)
                {
                    this.trains = new ObservableCollection<TrainItem>();
                }
                this.trains.Clear();
                foreach (var train in value)
                {
                    this.trains.Add(train);
                }
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
           var frame = (userControl as Page).Frame;
           var parameter = new PassageStationInfo(this.Station, this.Date, this.searchAnswer);
           frame.Navigate(typeof(ListStationInfo), parameter);
           int a = 9;
        
        }   

    }
}
