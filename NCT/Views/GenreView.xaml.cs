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
            songsView.DataContext = App.SongsView;
        }

        class Request
        {
            public static int Count;
            public int Id;
            public Request(int Id)
            {
                this.Id = Id;
                Count++;
            }
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            string id = "";
            if (NavigationContext.QueryString.TryGetValue("id", out id))
                try
                {
                    albumViewPivot.SelectedIndex = int.Parse(id);
                }
                catch (Exception) { }

            if (!App.AlbumListView.IsDataLoaded)
                App.AlbumPlayView.LoadAlbumView();
            if (!App.AlbumListView.IsDataLoaded)
                await App.AlbumListView.LoadDataInit("http://www.nhaccuatui.com/playlist/playlist-moi.html");
            if (!App.SongsView.IsDataLoaded)
                await App.SongsView.LoadInit("http://www.nhaccuatui.com/bai-hat/bai-hat-moi.html");
        }

        private async void albumGenreView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LongListSelector lls = sender as LongListSelector;
            if (lls.SelectedItem == null)
                return;
            ItemViewModel item = lls.SelectedItem as ItemViewModel;
            var rq = new Request(Request.Count);
            await App.AlbumListView.LoadDataInit(item.LineTwo);
            await App.SongsView.LoadInit(item.LineThree);
            if (rq.Id == Request.Count - 1)
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

        private async void albumListView_ItemRealized(object sender, ItemRealizationEventArgs e)
        {
            if (e.ItemKind == LongListSelectorItemKind.ListFooter)
                await App.AlbumListView.LoadMore();
        }

        private async void songsView_ItemRealized(object sender, ItemRealizationEventArgs e)
        {
            if (e.ItemKind == LongListSelectorItemKind.ListFooter)
                await App.SongsView.LoadMore();
        }
    }
}