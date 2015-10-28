using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NCT.Models;
using NCT.ViewModels;

namespace NCT.Views
{
    public partial class ArtistView : PhoneApplicationPage
    {
        public ArtistView()
        {
            InitializeComponent();
            artistView.DataContext = App.ArtistVM;
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            string name = NavigationContext.QueryString["name"];
            string info = NavigationContext.QueryString["info"];
            info = "http://nhaccuatui.com/" + info;
            ArtistViewModel Artist = new ArtistViewModel(new Artist() { Name = name, Info = info });
            await Artist.LoadDataInitAsync();
            App.ArtistVM.Coppy(Artist);
            albumslls.ScrollTo(App.ArtistVM.AlbumList.First());
            songsView.ScrollTo(App.ArtistVM.TrackList.First());
        }

        private async void albumsView_ItemRealized(object sender, ItemRealizationEventArgs e)
        {
            if (e.ItemKind == LongListSelectorItemKind.ListFooter)
                await App.ArtistVM.LoadMoreAlbumAsync();
        }


        private void StackPanel_ManipulationStarted(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            (sender as StackPanel).Opacity = 0.5;
        }

        private void StackPanel_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            (sender as StackPanel).Opacity = 1.0;
        }

        private void albumsView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LongListSelector lls = sender as LongListSelector;
            if (lls.SelectedItem == null)
                return;
            App.PlayerPlaylistArg = lls.SelectedItem as AlbumViewModel;
            NavigationService.Navigate(new Uri("/Views/Player.xaml", UriKind.Relative));
            lls.SelectedItem = null;
        }

        private async void songsView_ItemRealized(object sender, ItemRealizationEventArgs e)
        {
            if (e.ItemKind == LongListSelectorItemKind.ListFooter)
                await App.ArtistVM.LoadMoreTrackAsync();
        }

        private bool isProcessing = false;
        private async void songsView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LongListSelector lls = sender as LongListSelector;
            if (lls.SelectedItem == null)
                return;
            if (isProcessing == true)
                return;
            isProcessing = true;
            MessageBox.Show("Please wait for a few seconds");
            AlbumViewModel tmp = new AlbumViewModel() { Title = App.ArtistVM.Name };
            foreach (var track in App.ArtistVM.TrackList)
            {
                await track.GetDetailAsync();
                tmp.TrackList.Add(track);
            }
            App.PlayerPlaylistArg = tmp;
            NavigationService.Navigate(new Uri("/Views/Player.xaml", UriKind.Relative));
            isProcessing = false;
            lls.SelectedItem = null;
        }
    }
}