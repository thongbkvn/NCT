using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCT.Models
{
    public class Album
    {
        public Album()
        {
            trackList = new List<Track>();
        }
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
                    title = value;
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
                    artist = value;
            }
        }
        private List<Track> trackList;

        public List<Track> TrackList
        {
            get
            {
                return trackList;
            }
            set
            {
                if (trackList != value)
                    trackList = value;
            }
        }

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
                    cover = value;
            }

        }

        private string link;
        public string Link
        {
            get
            {
                return link;
            }
            set
            {
                if (link != value)
                    link = value;
            }

        }

        public async Task LoadTrackListAsync()
        {
            if (link == null)
                return;
            var tmp = NhacCuaTui.GetPlaylistAsync(link);
            Album album = await tmp;
            trackList = album.trackList;
        }
    
    }
}
