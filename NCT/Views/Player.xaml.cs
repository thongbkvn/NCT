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
using Microsoft.Phone.BackgroundAudio;
using System.IO.IsolatedStorage;
using Microsoft.Phone.BackgroundTransfer;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NCT.Views
{
    public partial class Player : PhoneApplicationPage
    {
        private bool isChecked = false;
        private int currentTrack;
        BitmapImage iconPlay = new BitmapImage();
        public Player()
        {
            InitializeComponent();
            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!isoStore.DirectoryExists("/shared/transfers"))
                {
                    isoStore.CreateDirectory("/shared/transfers");
                }
            }

            playlistView.DataContext = App.AlbumVM;
            playerPivot.DataContext = App.AlbumVM;
            musicVNView.DataContext = App.TrackVM;
            st.DataContext = App.TrackVM;                                      
        }

       

        private void Pivot_Loaded(object sender, RoutedEventArgs e)
        {

        }

        
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {                                        
            if (App.PlayerPlaylistArg != null)
            {
                App.AlbumVM.Coppy(App.PlayerPlaylistArg);
                if (App.PlayerPlaylistArg.TrackList == null || App.PlayerPlaylistArg.TrackList.Count == 0)
                    await App.AlbumVM.LoadTrackList();
                App.TrackVM.Copy(App.AlbumVM.TrackList.First());
            }
        }

        private void playlistView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isChecked == true)
            {
                isChecked = false;
                return;
            }
            LongListSelector lls = sender as LongListSelector;
            if (lls.SelectedItem == null)
                return;
            var tmp = lls.SelectedItem as TrackViewModel;
            foreach (var track in App.AlbumVM.TrackList)
                if (tmp.Location == track.Location)
                    currentTrack = App.AlbumVM.TrackList.IndexOf(track);
            App.TrackVM.Copy(tmp);
            lls.SelectedItem = null;
        }


        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                audio.Stop();
            }
            catch (Exception) { }                      
        }

        private void ButtonPlay_Click(object sender, RoutedEventArgs e)
        {   try
            {
                if (audio.CurrentState == MediaElementState.Playing)
                {
                    audio.Pause();
                    var sri2 = Application.GetResourceStream(new Uri("Assets/transport.play.png", UriKind.Relative));
                    iconPlay.SetSource(sri2.Stream);
                    playbg.ImageSource = iconPlay;
                }
                else
                {
                    audio.Play();

                    var sri2 = Application.GetResourceStream(new Uri("Assets/transport.pause.png", UriKind.Relative));
                    iconPlay.SetSource(sri2.Stream);
                    playbg.ImageSource = iconPlay;
                }
            }
            catch (Exception) { }                     
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ++currentTrack;
                if (currentTrack == App.AlbumVM.TrackList.Count)
                    currentTrack = 0;
                App.TrackVM.Copy(App.AlbumVM.TrackList.ElementAt(currentTrack));
            }
            catch (Exception) { }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                --currentTrack;
                if (currentTrack < 0)
                    currentTrack = App.AlbumVM.TrackList.Count - 1;
                App.TrackVM.Copy(App.AlbumVM.TrackList.ElementAt(currentTrack));
            }
            catch (Exception) { }
        }

       






        private void StackPanel_ManipulationStarted(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            (sender as StackPanel).Opacity = 0.5;
        }

        private void StackPanel_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            (sender as StackPanel).Opacity = 1.0;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            isChecked = true;
        }

        private void ApplicationBarIconButton_SaveClick(object sender, EventArgs e)
        {

        }

        private void ApplicationBarIconButton_SelectAllClick(object sender, EventArgs e)
        {
            bool tmp = false;
            foreach (var i in App.AlbumVM.TrackList)
                if (i.IsSelected == false)
                    tmp = true;


            foreach (var i in App.AlbumVM.TrackList)
                i.IsSelected = tmp;
        }


        /// <summary>
        /// Can chu y sua
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ApplicationBarIconButton_DownloadClick(object sender, EventArgs e)
        {               
            foreach (var track in App.AlbumVM.TrackList)
            {
                if (track.IsSelected)
                {
                    string link;
                    string[] links = await NhacCuaTui.GetSongDownloadLinkAsync(track.Song.Info);
                    if (links[1] != null)
                        link = links[1];
                    else if (links[0] != null)
                        link = links[0];
                    else
                        link = track.Location;
                       
                        BackgroundTransferRequest transferRequest = new BackgroundTransferRequest(new Uri(link, UriKind.RelativeOrAbsolute));
                    transferRequest.Method = "GET";

                    transferRequest.DownloadLocation = new Uri("shared/transfers/" + track.Title.Replace(' ', '-') + ".mp3", UriKind.RelativeOrAbsolute);
                    try
                    {
                        BackgroundTransferService.Add(transferRequest);
                    }
                    catch (Exception) { }      
                }
            }
        }

        private void audio_MediaEnded(object sender, RoutedEventArgs e)
        {
            try
            {
                ++currentTrack;
                if (currentTrack == App.AlbumVM.TrackList.Count)
                    currentTrack = 0;
                App.TrackVM.Copy(App.AlbumVM.TrackList.ElementAt(currentTrack));
            }
            catch (Exception) { }
        }
    }
}