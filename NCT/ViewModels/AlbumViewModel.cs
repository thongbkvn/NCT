using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCT.Models;
using System.Windows;
using System.Windows.Media.Imaging;

namespace NCT.ViewModels
{
    public class AlbumViewModel : ViewModel
    {
        private string title;
        public String Title
        {
            get
            {
                return title;
            }
            set
            {
                if (title != value)
                {
                    title = value;
                    NotifyPropertyChanged("Title");
                }
            }
        }

        private BitmapImage cover;
        public BitmapImage Cover
        {
            get
            {
                return cover;
            }
            set
            {
                if (cover != value)
                {
                    cover = value;
                    NotifyPropertyChanged("Cover");
                }
            }
        }

        private string artist;
        public string Artist
        {
            get
            {
                return artist;
            }
            set
            {
                if (artist != value)
                {
                    artist = value;
                    NotifyPropertyChanged("Artist");
                }
            }
        }
        public Album album;

        public ObservableCollection<TrackViewModel> TrackList
        {
            get;
        }


        public AlbumViewModel()
        {
            TrackList = new ObservableCollection<TrackViewModel>();
        }
        public bool IsDataLoaded
        {
            get; set;
        }
        public AlbumViewModel(Album album)
        {
            try
            {
                title = album.Title;
                this.album = album;
                artist = album.Artist;
                if (album.Cover != null)
                    Cover = new BitmapImage(new Uri(album.Cover, UriKind.Absolute));
                TrackList = new ObservableCollection<TrackViewModel>();
                foreach (var track in album.TrackList)
                {
                    try
                    {
                        TrackViewModel trackVM = new TrackViewModel(track);
                        TrackList.Add(trackVM);
                    }
                    catch (Exception) { }
                }
            }
            catch (Exception) { }

        }

        public async Task LoadTrackList()
        {
            if (!IsDataLoaded)
            {
                await album.LoadTrackListAsync();
                foreach (var track in album.TrackList)
                {
                    try
                    {
                        TrackViewModel trackVM = new TrackViewModel();
                        trackVM = new TrackViewModel(track);
                        TrackList.Add(trackVM);
                    }
                    catch (Exception) { }
                }
            }
        }

        public void Coppy(AlbumViewModel albumvm)
        {
            Title = albumvm.Title;
            if (TrackList != null)
                TrackList.Clear();
            if (albumvm.TrackList != null)
                foreach (var track in albumvm.TrackList)
                    TrackList.Add(track);
            Cover = albumvm.Cover;
            Artist = albumvm.Artist;
            album = albumvm.album;
            IsDataLoaded = albumvm.IsDataLoaded;
        }
        //public async void LoadTopTrackListLink(string link)
        //{

        //    Album album = await NhacCuaTui.GetTopTrackList(link);
        //    foreach (var track in album.TrackList)
        //    {
        //        try
        //        {
        //            TrackViewModel trackVM = new TrackViewModel();
        //            trackVM = new TrackViewModel(track);
        //            TrackList.Add(trackVM);
        //        }
        //        catch (Exception) { }
        //    }                     
        //}

        //public async void LoadPlaylistLink(string link)
        //{
        //    Album album = await NhacCuaTui.GetPlaylist(link);
        //    foreach (var track in album.TrackList)
        //    {
        //        try
        //        {
        //            TrackViewModel trackVM = new TrackViewModel();
        //            trackVM = new TrackViewModel(track);
        //            TrackList.Add(trackVM);
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //        }
        //    }                    
        //}
    }
}
