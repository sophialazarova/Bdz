﻿using Bdz.Common;
using Bdz.LocalDB;
using Bdz.Utilities;
using Bdz.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Bdz.Pages
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class SearchRoute : Page
    {

        private NavigationHelper navigationHelper;
        private TownSuggestionHelper suggestionHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public SearchRoute()
        {
            this.InitializeComponent();
            this.DataContext = new SearchRouteViewModel();

            this.suggestionHelper = new TownSuggestionHelper();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
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
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
        private void StartStationListboxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.SuggestionListBoxSelectionChanged(this.suggestionsToDeparture, this.departureTown);
        }

        private void StartStationTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            this.SuggestionTextBoxTextChanged(sender, this.suggestionsToDeparture);
        }

        private void EndStationListboxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.SuggestionListBoxSelectionChanged(this.suggestionsToArrival, this.arrivalTown);
        }

        private void EndStationTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            this.SuggestionTextBoxTextChanged(sender, this.suggestionsToArrival);
        }

        private void SuggestionListBoxSelectionChanged(ListBox suggestions, TextBox choiceHolder)
        {
            if (suggestions.ItemsSource != null)
            {
                suggestions.Visibility = Visibility.Collapsed;
                if (choiceHolder == this.arrivalTown)
                {
                    choiceHolder.TextChanged -= new TextChangedEventHandler(EndStationTextBoxTextChanged);
                }
                else
                {
                    choiceHolder.TextChanged -= new TextChangedEventHandler(StartStationTextBoxTextChanged);
                }

                if (suggestions.SelectedIndex != -1)
                {
                    choiceHolder.Text = suggestions.SelectedItem.ToString();

                }

                if (choiceHolder == this.arrivalTown)
                {
                    choiceHolder.TextChanged += new TextChangedEventHandler(EndStationTextBoxTextChanged);
                }
                else
                {
                    choiceHolder.TextChanged += new TextChangedEventHandler(StartStationTextBoxTextChanged);
                }
            }
        }

        private void SuggestionTextBoxTextChanged(object sender, ListBox suggestions)
        {
            string typed = (sender as TextBox).Text.ToUpper();
            IList<Town> matched = new List<Town>();


            if (String.IsNullOrEmpty(typed))
            {
                suggestions.Visibility = Visibility.Collapsed;
                return;
            }

            foreach (var item in this.suggestionHelper.Towns)
            {
                if (item.Name.StartsWith(typed))
                {
                    matched.Add(item);
                }
            }

            if (matched.Count > 0)
            {
                suggestions.ItemsSource = matched;
                suggestions.Visibility = Visibility.Visible;
            }
            else if (matched.Count == 0)
            {
                suggestions.Visibility = Visibility.Collapsed;
            }
        }
    }
}
