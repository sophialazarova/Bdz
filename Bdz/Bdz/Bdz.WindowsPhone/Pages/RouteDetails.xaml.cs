using Bdz.Common;
using Bdz.Utilities;
using Bdz.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556
namespace Bdz.Pages
{

    public sealed partial class RouteDetails : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private bool areTransitionDetailsShowed;

        public RouteDetails()
        {
            this.InitializeComponent();
            this.DataContext = new RouteDetailsViewModel();
            this.areTransitionDetailsShowed = true;

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TransitionsInfoLabelTapped(object sender, TappedRoutedEventArgs e)
        {
            this.areTransitionDetailsShowed = !this.areTransitionDetailsShowed;
            if (this.areTransitionDetailsShowed)
            {
                this.transitionDetails.Visibility = Visibility.Visible;
            }
            else
            {
                this.transitionDetails.Visibility = Visibility.Collapsed;
            }
        }

        private void OnHoldingDetailsGrid(object sender, HoldingRoutedEventArgs e)
        {
            this.toastCreatorMenu.Visibility = Visibility.Visible;
        }

        private void OnHoldingToastCreatorMenu(object sender, HoldingRoutedEventArgs e)
        {
            this.toastCreatorMenu.Visibility = Visibility.Collapsed;
        }

        private void OnClickCreateToast(object sender, RoutedEventArgs e)
        {
            DateTimeOffset date = this.triggerDate.Date;
            TimeSpan time = this.triggerHour.Time;
            DateTimeOffset toastOffset = new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, time.Seconds);
            ToastManager manager = new ToastManager();
            string toastHeader = "Влак <" + this.DepartureStation.Text + " - " + this.ArrivalStation.Text + "> ";
            string toastContent = "заминава в " + this.DepartureTime.Text + " на " + this.DepartureDate.Text;
            manager.SendScheduledToast(toastHeader, toastContent, toastOffset);
            this.toastCreatorMenu.Visibility = Visibility.Collapsed;
        }

        private void triggerDate_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            DateTimeOffset now = DateTime.Now;
            if (e.NewDate < now)
            {
                (sender as DatePicker).Date = now;
            }
        }

        private void triggerHour_TimeChanged(object sender, TimePickerValueChangedEventArgs e)
        {
            DateTimeOffset now = DateTime.Now;
            if (e.OldTime < now.TimeOfDay)
            {
                (sender as TimePicker).Time = now.AddMinutes(1).TimeOfDay;
            }
        }
    }
}
