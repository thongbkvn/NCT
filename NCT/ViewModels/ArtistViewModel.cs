using NCT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace NCT.ViewModels
{
    public class ArtistViewModel : ViewModel
    {
        public ArtistViewModel()
        {

        }
        public ArtistViewModel(Artist artist)
        {
            Name = artist.Name;
            Cover = artist.Cover;
            ArtistInfo = artist;
            if (artist.Cover != null)
            {
                BgImage = new BitmapImage();
                BgImage.UriSource = new Uri(artist.Cover);
            }
            if (artist.TrackList != null)
                foreach (var track in artist.TrackList)
                    TrackList.Add(new TrackViewModel(track));

            if (artist.AlbumList != null)
                foreach (var album in artist.AlbumList)
                    AlbumList.Add(new AlbumViewModel(album));
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }


        public Artist ArtistInfo;
        private string cover;
        public string Cover
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

        private BitmapImage bgimage;
        public BitmapImage BgImage
        {
            get
            {
                return bgimage;
            }
            set
            {
                if (bgimage != value)
                {
                    bgimage = value;
                    NotifyPropertyChanged("BgImage");
                }
            }
        }

        private ObservableCollection<AlbumViewModel> albumList = new ObservableCollection<AlbumViewModel>();
        public ObservableCollection<AlbumViewModel> AlbumList
        {
            get
            {
                return albumList;
            }
            set
            {
                if (albumList != value)
                {
                    albumList = value;
                    NotifyPropertyChanged("AlbumList");
                }
            }
        }

        private ObservableCollection<TrackViewModel> trackList = new ObservableCollection<TrackViewModel>();
        public ObservableCollection<TrackViewModel> TrackList
        {
            get
            {
                return trackList;
            }
            set
            {
                if (trackList != value)
                {
                    trackList = value;
                    NotifyPropertyChanged("TrackList");
                }
            }
        }

        private int trackPage = 1;
        private int albumPage = 1;
        public async Task LoadDataInitAsync()
        {
            if (ArtistInfo == null)
                return;
            await ArtistInfo.LoadDataInitAsync();
            TrackList.Clear();
            AlbumList.Clear();
            foreach (var track in ArtistInfo.TrackList)
                TrackList.Add(new TrackViewModel(track));
            foreach (var album in ArtistInfo.AlbumList)
                AlbumList.Add(new AlbumViewModel(album));
        }

        public async Task LoadMoreTrackAsync()
        {
            if (ArtistInfo == null)
                return;
            var trackList = await ArtistInfo.LoadMoreTrackList(++trackPage);
            foreach (var track in trackList)
                TrackList.Add(new TrackViewModel(track));
        }

        public async Task LoadMoreAlbumAsync()
        {
            if (ArtistInfo == null)
                return;
            var albumList = await ArtistInfo.LoadMoreAlbumList(++albumPage);
            foreach (var album in albumList)
                AlbumList.Add(new AlbumViewModel(album));
        }

        public void Coppy(ArtistViewModel artistVM)
        {
            Name = artistVM.Name;
            ArtistInfo = artistVM.ArtistInfo;
            Cover = artistVM.Cover;
            BgImage = artistVM.BgImage;
            AlbumList.Clear();
            TrackList.Clear();
            foreach (var album in artistVM.AlbumList)
                AlbumList.Add(album);
            foreach (var track in artistVM.TrackList)
                TrackList.Add(track);
            trackPage = artistVM.trackPage;
            albumPage = artistVM.albumPage;
        }
    }
}
