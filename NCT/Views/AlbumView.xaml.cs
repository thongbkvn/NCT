using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NCT.ViewModels;

namespace NCT.Views
{
    public partial class AlbumView : PhoneApplicationPage
    {
        public AlbumView()
        {
            InitializeComponent();
            albumGenreView.DataContext = App.AlbumPlayView;
            albumListView.DataContext = App.AlbumListView;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            //base.OnNavigatedTo(e);
            App.AlbumPlayView.LoadAlbumView();
            if (!App.AlbumListView.IsDataLoaded)
                await App.AlbumListView.LoadData("http://www.nhaccuatui.com/playlist/playlist-moi.html", 0);
        }

        private async void albumGenreView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LongListSelector lls = sender as LongListSelector;
            if (lls.SelectedItem == null)
                return;
            ItemViewModel item = lls.SelectedItem as ItemViewModel;
            await App.AlbumListView.LoadData(item.NavigatePage, 0);
            albumViewPivot.SelectedIndex = 1;
        }

        private void StackPanel_ManipulationStarted(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            (sender as StackPanel).Opacity = 0.5;
        }

        private void albumListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LongListSelector lls = sender as LongListSelector;
            if (lls.SelectedItem == null)
                return;
            App.PlayerPlaylistArg = lls.SelectedItem as AlbumViewModel;
            NavigationService.Navigate(new Uri("/Views/Player.xaml", UriKind.Relative));
            lls.SelectedItem = null;
        }

        private void StackPanel_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            (sender as StackPanel).Opacity = 1;
        }

      
    }
}