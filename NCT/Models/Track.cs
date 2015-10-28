using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace NCT.Models
{
    public class Track
    {
        private string title, artist, artistLink, location, info, lyric, image = null;
        public string Title //Ten bai hat
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
        public string Artist    //Link stream
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
        public string Location  //Link stream
        {
            get
            {
                return location;
            }
            set
            {
                if (location != value)
                    location = value;
            }
        }
        public string Info //Link bai hat
        {
            get
            {
                return info;
            }
            set
            {
                if (info != value)
                    info = value;
            }
        }

        public string Lyric //Link loi bai hat
        {
            get
            {
                return lyric;
            }
            set
            {
                if (lyric != value)
                    lyric = value;
            }
        }

        public string ArtistLink    //Link ca si
        {
            get
            {
                return artistLink;
            }
            set
            {
                if (artistLink != value)
                    artistLink = value;
            }
        }

        public string Image //Link hinh anh
        {
            get
            {
                return image;
            }
            set
            {
                if (image != value)
                    image = value;
            }
        }

        public async Task GetTrackDetailAsync()
        {
            if (info == null)
                return;
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.UserAgent.Add(new Windows.Web.Http.Headers.HttpProductInfoHeaderValue("Mozilla / 5.0(Windows NT 10.0; WOW64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 45.0.2454.101 Safari / 537.36"));
                string response = await client.GetStringAsync(new Uri(info));
                
                string xmlLink = Regex.Split(response, "player.peConfig.xmlURL = \"")[1].Split('\"')[0];
                XDocument xdoc;
                //Bo qua khoang trang
                xdoc = XDocument.Parse(await client.GetStringAsync(new Uri(xmlLink)), LoadOptions.None);
                client.Dispose();

                var track = from t in xdoc.Descendants("track")
                            select new
                            {
                                Title = t.Element("title").Value,
                                Artist = t.Element("creator").Value,
                                Location = t.Element("location").Value,
                                Info = t.Element("info").Value,
                                ArtistLink = t.Element("newtab").Value,
                                Image = t.Element("bgimage").Value
                            };
                var song = track.FirstOrDefault();
                title = song.Title;
                artist = song.Artist;
                location = song.Location;
                info = song.Info;
                artistLink = song.ArtistLink;
                image = song.Image;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }










    }
}
