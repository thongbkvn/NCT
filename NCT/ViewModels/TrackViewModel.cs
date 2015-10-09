using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCT.Models;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.Windows;

namespace NCT.ViewModels
{
    public class TrackViewModel : ViewModel
    {
        private string title;
        public string Title
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
                    NotifyPropertyChanged("TitleOfSongChanged");
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
                    NotifyPropertyChanged("ArtistOfSongChanged");
                }
            }
        }
        private Track song;
        public   Track Song
        {
            get
            {
                return song;
            }
            set
            {
                if (song != value)
                {
                    song = value;
                    NotifyPropertyChanged("SongChanged");
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
                    NotifyPropertyChanged("TrackCoverChanged");
                }
            }
        }

        private string location;
        public string Location
        {
            get
            {
                return location;
            }
            set
            {
                if (location != value)
                {
                    location = value;
                    NotifyPropertyChanged("LocationOfTrackChanged");
                }
            }
        }

        public TrackViewModel() { }
        public TrackViewModel(Track track)
        {
            try
            {
                Title = track.Title;
                Artist = track.Artist;
                Song = track;
                Location = track.Location;
                if (track.Image != null)
                {
                    Cover = new BitmapImage();
                    Cover.UriSource = new Uri(track.Image);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Coppy(TrackViewModel trackvm)
        {
            Title = trackvm.Title;
            Cover = trackvm.Cover;
            Song = trackvm.Song;
            Artist = trackvm.Artist;
            location = trackvm.Location;
        }
    }
}
