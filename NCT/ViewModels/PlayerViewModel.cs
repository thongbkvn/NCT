using NCT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCT.ViewModels
{
    public class PlayerViewModel : ViewModel
    {
        private AlbumViewModel nowPlayingAlbum;
        public AlbumViewModel NowPlayingAlbum
        {
            get
            {
                return nowPlayingAlbum;
            }
            set
            {
                if (nowPlayingAlbum != value)
                {
                    nowPlayingAlbum = value;
                    NotifyPropertyChanged("NowPlayingAlbum");
                }
            }
        }

        private TrackViewModel playingTrack;
        public TrackViewModel PlayingTrack
        {
            get
            {
                return playingTrack;
            }
            set
            {
                if (playingTrack != value)
                {
                    playingTrack = value;
                    NotifyPropertyChanged("PlayingTrackChanged");
                }
            }
        }


        /// <summary>
        /// Chuc nang tim kiem bai hat tuong tu
        /// </summary>
        /// <param name="album"></param>
        //private AlbumViewModel relatedTrack;
        //public AlbumViewModel RelatedTrack
        //{
        //    get
        //    {
        //        return relatedTrack;
        //    }
        //    set
        //    {
        //        if (relatedTrack != value)
        //        {
        //            relatedTrack = value;
        //            NotifyPropertyChanged("RelatedTrackChanged");
        //        }
        //    }
        //}
    }
}
