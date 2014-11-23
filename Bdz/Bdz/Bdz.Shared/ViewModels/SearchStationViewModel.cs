namespace Bdz.ViewModels
{
    using Bdz.Models;
    using Bdz.Pages;
    using Bdz.Utilities;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using Newtonsoft.Json;
    using System;
    using System.Windows.Input;
    using Windows.UI.Popups;
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
            this.HandleRequestAsync(userControl);
        }

        private async void HandleRequestAsync(FrameworkElement userControl)
        {
            if (String.IsNullOrEmpty(this.Station))
            {
                MessageDialog message = new MessageDialog("Моля, задайте гара.");
                await message.ShowAsync();
            }
            else
            {
                this.searchAnswer = await this.remoteManager.GetStationInfo(this.Station, this.Date.Day + "/" + this.Date.Month + "/" + this.Date.Year);
                if (this.searchAnswer == null)
                {
                    MessageDialog message = new MessageDialog("Няма намерени резултати!");
                    await message.ShowAsync();
                }
                else
                {
                    ViewDataTransferHelper.Station = this.Station;
                    ViewDataTransferHelper.StationDetailsDate = this.Date;
                    ViewDataTransferHelper.StationInfo = this.searchAnswer;

                    var frame = (userControl as Page).Frame;
                    frame.Navigate(typeof(ListStationInfo));
                }
            }
        }   
    }
}
