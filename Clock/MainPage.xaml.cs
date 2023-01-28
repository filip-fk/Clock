using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Clock
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            //time handles
            DataContext = this;
            Timer.Tick += Timer_Tick;
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();

            //titilebar customs
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Windows.UI.Colors.Transparent;
        }

        DispatcherTimer Timer = new DispatcherTimer();
        string timeFormat = "h:mm:ss tt";

        private void dismiss_Click(object sender, RoutedEventArgs e)
        {
            topCmmds.Visibility = topCmmds.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
            btmCmmds.Visibility = btmCmmds.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
            dismiss.Visibility = dismiss.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
            show.Visibility = show.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;

            if(dismiss.Visibility == Visibility.Collapsed)
            {
                if(sett.Visibility == Visibility.Visible)
                {
                    sett.Visibility = Visibility.Collapsed;
                    settings.IsChecked = false;
                }
            }
        }

        private void big_Click(object sender, RoutedEventArgs e)
        {
            timeTxt.FontSize += 4;
        }

        private void small_Click(object sender, RoutedEventArgs e)
        {
            timeTxt.FontSize -= 4;
        }

        private void Timer_Tick(object sender, object e)
        {
            timeTxt.Text = DateTime.Now.ToString(timeFormat);
        }

        private void settings_Click(object sender, RoutedEventArgs e)
        {
            sett.Visibility = sett.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }

        private void hhmm_Click(object sender, RoutedEventArgs e)
        {
            timeFormat = ((Button)sender).Content.ToString();
        }

        private async void full_Click(object sender, RoutedEventArgs e)
        {
            var view = ApplicationView.GetForCurrentView();
            bool modeSwitched = await view.TryEnterViewModeAsync(ApplicationViewMode.CompactOverlay);

            if (modeSwitched)
            {
                //app on top
            }
            else
            {
                if (view.TryEnterFullScreenMode())
                {
                    modeSwitched = await view.TryEnterViewModeAsync(ApplicationViewMode.Default);
                }
            }

            /*await CoreApplication.CreateNewView().Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var frame = new Frame();
                compactViewId = ApplicationView.GetForCurrentView().Id;
                frame.Navigate(typeof(SecondaryCompactViewPage));
                Window.Current.Content = frame;
                Window.Current.Activate();
                ApplicationView.GetForCurrentView().Title = "CompactOverlay Window";
            });
            bool viewShown = await ApplicationViewSwitcher.TryShowAsViewModeAsync(compactViewId, ApplicationViewMode.CompactOverlay);*/
            
        }

        private void small_Click(object sender, RightTappedRoutedEventArgs e)
        {
            small_Click(sender, new RoutedEventArgs());
        }
    }
}
