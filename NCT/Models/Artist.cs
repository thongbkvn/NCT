using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace NCT.Models
{
    public class Artist
    {
        public string Name { get; set; }
        public string Info { get; set; }
        public string Cover { get; set; }
        public List<Album> AlbumList { get; set; } = new List<Album>();
        public List<Track> TrackList { get; set; } = new List<Track>();

        public async Task LoadDataInitAsync()
        {
            if (Info == null)
                return;
            var tmp1 = LoadMoreTrackList();
            var tmp2 = LoadMoreAlbumList();
            var trackList = await tmp1;
            TrackList.AddRange(trackList);
            var albumList = await tmp2;
            AlbumList.AddRange(albumList);  
        }
        public async Task<List<Track>> LoadMoreTrackList(int page = 1)
        {
            if (Info == null)
                return null;
            string link;
            if (page <= 1)
                link = Info.Remove(Info.Length-4) + "bai-hat.html";
            else
                link = Info.Remove(Info.Length-4) + "bai-hat." + page + ".html";
            string response;
            using (HttpClient hc = new HttpClient())
            {
                hc.DefaultRequestHeaders.UserAgent.Add(new Windows.Web.Http.Headers.HttpProductInfoHeaderValue("Mozilla / 5.0(Windows NT 10.0; WOW64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 45.0.2454.101 Safari / 537.36"));
                response = await hc.GetStringAsync(new Uri(link, UriKind.Absolute));
            }

            HtmlDocument hdoc = new HtmlDocument();
            hdoc.LoadHtml(response);

            //var ultag = (from ultags in hdoc.DocumentNode.Descendants("ul").Where(x => x.Attributes["class"].Value == "list_item_music")
            //            select ultags).First();
            //var trackList = from litag in ultag.ChildNodes.Where(x => x.Name == "li")
            //                select new Track()
            //                {

            //                };
            var trackList = from ultag in hdoc.DocumentNode.Descendants("ul").Where(x => x.Attributes["class"] != null && x.Attributes["class"].Value == "list_item_music")
                            from litag in ultag.ChildNodes.Where(x => x.Name == "li") 
                            select new Track()
                            {
                                Title = litag.ChildNodes[1].ChildNodes[0].InnerText,
                                Info = litag.ChildNodes[1].ChildNodes[0].ChildNodes[0].Attributes["href"].Value,
                                ArtistLink = litag.ChildNodes[1].ChildNodes[2].ChildNodes[0].Attributes["href"].Value,
                                Artist = litag.ChildNodes[1].ChildNodes[2].InnerText
                            };
            return trackList.ToList();           
        }

        public async Task<List<Album>> LoadMoreAlbumList(int page = 1)
        {
            if (Info == null)
                return null;
            string link;
            if (page <= 1)
                link = Info.Remove(Info.Length - 4) + "playlist.html";
            else
                link = Info.Remove(Info.Length - 4) + "playlist." + page + ".html";
            string response;
            using (HttpClient hc = new HttpClient())
            {
                hc.DefaultRequestHeaders.UserAgent.Add(new Windows.Web.Http.Headers.HttpProductInfoHeaderValue("Mozilla / 5.0(Windows NT 10.0; WOW64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 45.0.2454.101 Safari / 537.36"));
                response = await hc.GetStringAsync(new Uri(link, UriKind.Absolute));
            }

            HtmlDocument hdoc = new HtmlDocument();
            hdoc.LoadHtml(response);

            var divtag = (from divtags in hdoc.DocumentNode.Descendants("div").Where(x => x.Attributes[0].Value == "fram_select")
                          select divtags).First();
            var albumList = from litag in divtag.ChildNodes[4].ChildNodes.Where(x => x.Name == "li")
                            select new Album()
                            {
                                Cover = litag.ChildNodes[1].ChildNodes[1].ChildNodes[3].ChildNodes[0].Attributes["src"].Value,
                                Title = litag.ChildNodes[3].ChildNodes[1].ChildNodes[0].InnerText,
                                Link = litag.ChildNodes[3].ChildNodes[1].ChildNodes[0].Attributes["href"].Value,
                                Artist = litag.ChildNodes[3].ChildNodes[3].ChildNodes[0].InnerText
                            };
            return albumList.ToList();
        }
    }
}
