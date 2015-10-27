﻿using System;                
using NCT.Models;            
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
                    NotifyPropertyChanged("Title");
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
                    NotifyPropertyChanged("Song");
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
        
        private bool isSelected;
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    NotifyPropertyChanged("Check");
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
                    NotifyPropertyChanged("Location");
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

        public void Copy(TrackViewModel trackvm)
        {
            Title = trackvm.Title;
            Cover = trackvm.Cover;
            Song = trackvm.Song;
            Artist = trackvm.Artist;
            Location = trackvm.Location;
        }
    }
}
