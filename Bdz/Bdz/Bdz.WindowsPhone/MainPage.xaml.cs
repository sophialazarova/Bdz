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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Bdz
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private IList<string> testItems;
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            testItems = new List<string>()
            {
                "СОФИЯ","БУРГАС","ПЛОВДИВ","СМОЛЯН","СВОГЕ"
            };

        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.suggestions.ItemsSource != null)
            {
                this.suggestions.Visibility = Visibility.Collapsed;
                this.departureTown.TextChanged -= new TextChangedEventHandler(departureTown_TextChanged);
                if (this.suggestions.SelectedIndex != -1)
                {
                    this.departureTown.Text = this.suggestions.SelectedItem.ToString();
                    
                }

                this.departureTown.TextChanged += new TextChangedEventHandler(departureTown_TextChanged);
            }
        }

        private void departureTown_TextChanged(object sender, TextChangedEventArgs e)
        {
            string typed = (sender as TextBox).Text;
            IList<string> matched = new List<string>();

            if(String.IsNullOrEmpty(typed)){
                return;
            }

            foreach (var item in this.testItems)
            {
                if (item.StartsWith(typed))
                {
                    matched.Add(item);
                }
            }

            if (matched.Count > 0)
            {
                this.suggestions.ItemsSource = matched;
                this.suggestions.Visibility = Visibility.Visible;
            }
        }
    }
}
