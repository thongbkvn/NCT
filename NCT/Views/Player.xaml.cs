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
    public partial class Player : PhoneApplicationPage
    {
        public Player()
        {
            InitializeComponent();
            playlistView.DataContext = App.AlbumVM;
            playerPivot.DataContext = App.AlbumVM;
            musicVNView.DataContext = App.TrackVM;
        }

        private void Pivot_Loaded(object sender, RoutedEventArgs e)
        {

        }

        
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            //base.OnNavigatedTo(e);
            if (App.PlayerPlaylistArg != null)
            {
                App.AlbumVM.Coppy(App.PlayerPlaylistArg);
                if (App.PlayerPlaylistArg.TrackList == null || App.PlayerPlaylistArg.TrackList.Count == 0)
                    await App.AlbumVM.LoadTrackList();
                App.TrackVM.Coppy(App.AlbumVM.TrackList.First());
            }
        }

        private void playlistView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LongListSelector lls = sender as LongListSelector;
            if (lls.SelectedItem == null)
                return;
            var tmp = lls.SelectedItem as TrackViewModel;
            App.TrackVM.Coppy(tmp);
            if (audio.Source != null)
                audio.Pause();
            audio.Source = new Uri(App.TrackVM.Location);
            lls.SelectedItem = null;
        }


        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            audio.Stop();
        }

        private void ButtonPlay_Click(object sender, RoutedEventArgs e)
        {
            audio.Play();
        }

        private void ButtonPause_Click(object sender, RoutedEventArgs e)
        {
            audio.Pause();
        }

        private void audio_MediaOpened(object sender, RoutedEventArgs e)
        {
            audio.Play();
        }

        private void StackPanel_ManipulationStarted(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            (sender as StackPanel).Opacity = 0.5;
        }

        private void StackPanel_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            (sender as StackPanel).Opacity = 1.0;
        }
    }
}