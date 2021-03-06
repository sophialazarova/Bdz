﻿namespace Bdz.ViewModels
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
    using Windows.UI.Popups;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public class SearchRouteViewModel : ViewModelBase
    {
  
        private ICommand onSearchButtonClick;
        private RemoteDataManager remoteManager;
        private RouteInfoRequestObject currentRouteInfo;
        private DateTimeOffset date = DateTime.Today;
        private bool isActive;
        private bool isBackgroundAvailable;

        public SearchRouteViewModel()
        {
            this.remoteManager = new RemoteDataManager();
            this.MinimalSelectionDate = DateTime.Now;
            this.isActive = false;
            this.isBackgroundAvailable = true;
        }
        public DateTimeOffset MinimalSelectionDate { get; private set; }

        public bool IsBackgroundAvailable
        {
            get
            {
                return this.isBackgroundAvailable;
            }
            set
            {
                this.isBackgroundAvailable = value;
                RaisePropertyChanged<bool>("IsBackgroundAvailable", !this.isBackgroundAvailable, this.isBackgroundAvailable, true);
            }
        }

        public string From { get; set; }

        public string To { get; set; }

        public bool IsProgressRingActive
        {
            get
            {
                return this.isActive;
            }
            set
            {
                this.isActive = value;
                RaisePropertyChanged<bool>("IsProgressRingActive", !this.isActive, this.isActive, true);
            }
        }

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
                    this.onSearchButtonClick = new RelayCommand<FrameworkElement>(param => this.SearchForRoute(param));
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
            if (String.IsNullOrEmpty(this.From) || String.IsNullOrEmpty(this.To))
            {
                MessageDialog message = new MessageDialog("Моля, задайте начална и крайна гара.");
                await message.ShowAsync();
            }
            else
            {
                this.IsBackgroundAvailable = false;
                this.IsProgressRingActive = true;
                string pickedDate = CommonUtilities.FormatDate(this.date);
                this.currentRouteInfo = await remoteManager.GetRouteInfo(this.From, this.To, pickedDate);
                if (this.currentRouteInfo.Routes == null || this.currentRouteInfo == null)
                {
                    this.IsProgressRingActive = false;
                    MessageDialog message = new MessageDialog("Няма намерени резултати!");
                    await message.ShowAsync();
                    this.IsBackgroundAvailable = true;
                }
                else
                {
                    ViewDataTransferHelper.RouteArrivalStation = this.To;
                    ViewDataTransferHelper.RouteDepartureStation = this.From;
                    ViewDataTransferHelper.RouteDate = this.Date;
                    ViewDataTransferHelper.RouteInfo = this.currentRouteInfo;
                    this.IsProgressRingActive = false;

                    var frame = (userControl as Page).Frame;
                    frame.Navigate(typeof(ListRoutes));
                }
            }
        }
    }
}
