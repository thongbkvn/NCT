using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Web.Http;

namespace NCT.Models
{
    public class NhacCuaTui
    {
        public enum SearchType {All = 0, Album = 1, Song = 2, Singer = 4}
        public static async Task<Album> GetTopTrackListAsync(string link)
        {
            Album album = new Album();
            album.Link = link;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.Add(new Windows.Web.Http.Headers.HttpProductInfoHeaderValue("Mozilla / 5.0(Windows NT 10.0; WOW64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 45.0.2454.101 Safari / 537.36"));
            string response = await client.GetStringAsync(new Uri(link));
            client.Dispose();
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(response);

            //Lay album title tu header
            album.Title = doc.DocumentNode.ChildNodes["html"].ChildNodes["head"].ChildNodes["title"].InnerText;
            //Chon ra the ul chua danh sach bai hat
            var ulTags = from ul in doc.DocumentNode.Descendants("ul").Where(x => x.Attributes["class"].Value == "list_show_chart")
                         select ul;
            var ulTag = ulTags.First();

            //Moi node la 1 the li
            foreach (HtmlNode node in ulTag.ChildNodes)
            {
                //Loai bo the #text
                if (node.Name == "li")
                {
                    try
                    {
                        HtmlNode trackInfoNode = node.ChildNodes[5];
                        Track track = new Track();
                        track.Title = trackInfoNode.ChildNodes[3].ChildNodes[0].InnerText;
                        track.Info = trackInfoNode.ChildNodes[3].ChildNodes[0].Attributes["href"].Value;
                        track.Image = trackInfoNode.ChildNodes[1].ChildNodes["img"].Attributes["src"].Value;
                        track.Artist = trackInfoNode.ChildNodes[5].ChildNodes[1].InnerText;
                        track.ArtistLink = trackInfoNode.ChildNodes[5].ChildNodes[1].Attributes["href"].Value;
                        album.TrackList.Add(track);
                    }

                    catch (Exception)
                    { }

                }
            }

            return album;
        }
        public static async Task<Album> GetPlaylistAsync(string link)
        {
            try
            {
                HttpClient hc = new HttpClient();
                
                hc.DefaultRequestHeaders.UserAgent.Add(new Windows.Web.Http.Headers.HttpProductInfoHeaderValue("Mozilla / 5.0(Windows NT 10.0; WOW64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 45.0.2454.101 Safari / 537.36"));
                string htmlPage = await hc.GetStringAsync(new Uri(link));
                string xmlLink = Regex.Split(htmlPage, "amp;file=")[1].Split('\"')[0];
                XDocument xdoc = XDocument.Parse(await hc.GetStringAsync(new Uri(xmlLink)));
                hc.Dispose();

                //Parse xml
                var trackList = from t in xdoc.Descendants("track")
                                select new Track()
                                {
                                    Title = t.Element("title").Value,
                                    Artist = t.Element("creator").Value,
                                    Location = t.Element("location").Value,
                                    Info = t.Element("info").Value,
                                    ArtistLink = t.Element("newtab").Value,
                                    Image = t.Element("bgimage").Value
                                };

                Album album = new Album();
                album.Link = link;
                album.TrackList = trackList.ToList();
                return album;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public static async Task<List<Album>> GetListOfPlaylistAsync(string link, int page)
        {
            if (page != 0)
            {
                link = link.Remove(link.Length - 4) + page + ".html";
            }
            HttpClient hc = new HttpClient();
            hc.DefaultRequestHeaders.UserAgent.Add(new Windows.Web.Http.Headers.HttpProductInfoHeaderValue("Mozilla / 5.0(Windows NT 10.0; WOW64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 45.0.2454.101 Safari / 537.36"));
            string response = await hc.GetStringAsync(new Uri(link));
            HtmlDocument hdoc = new HtmlDocument();
            hdoc.LoadHtml(response);
            hc.Dispose();           

            //tai sao lai x.Attributes["class"] lai khong duoc
            var divtag = (from divtags in hdoc.DocumentNode.Descendants("div").Where(x => x.Attributes[0].Value == "fram_select")
                          select divtags).First();
            var albumList = from litag in divtag.ChildNodes[3].ChildNodes.Where(x => x.Name == "li")
                            select new Album()
                            {
                                Cover = litag.ChildNodes[1].ChildNodes[1].ChildNodes[3].ChildNodes[0].Attributes["src"].Value,
                                Title = litag.ChildNodes[3].ChildNodes[1].ChildNodes[0].InnerText,
                                Link = litag.ChildNodes[3].ChildNodes[1].ChildNodes[0].Attributes["href"].Value,
                                Artist = litag.ChildNodes[3].ChildNodes[3].ChildNodes[0].InnerText
                            };
            return albumList.ToList();
        }

        public static async Task<List<Track>> GetRelatedTrackListAsync(Track track)
        {
            //Kiem tra an toan
            if (track.Info == null)
                return null;
            string link = track.Info;
            HttpClient hc = new HttpClient();
            hc.DefaultRequestHeaders.UserAgent.Add(new Windows.Web.Http.Headers.HttpProductInfoHeaderValue("Mozilla / 5.0(Windows NT 10.0; WOW64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 45.0.2454.101 Safari / 537.36"));
            string response = await hc.GetStringAsync(new Uri(link, UriKind.Absolute));
            hc.Dispose();
            HtmlDocument hdoc = new HtmlDocument();
            hdoc.LoadHtml(response);
            HtmlNode ultag = (from ultags in hdoc.DocumentNode.Descendants("ul").Where(x=> x.Attributes["id"].Value == "recommendZone")
                        select ultags).First();
            var relatedTrackList = from litag in ultag.ChildNodes.Where(x => x.Name == "li")
                                   select new Track()
                                   {
                                       Info = litag.ChildNodes[3].ChildNodes[1].ChildNodes[0].Attributes["href"].Value,
                                       Title = litag.ChildNodes[3].ChildNodes[1].ChildNodes[0].InnerText,
                                       Artist = litag.ChildNodes[3].ChildNodes[3].ChildNodes[0].InnerText,
                                       ArtistLink = litag.ChildNodes[3].ChildNodes[3].ChildNodes[0].Attributes["href"].Value
                                   };

            return relatedTrackList.ToList();
        }

        public static async Task<List<Track>> GetTrackByGenreAsync(string link, int page)
        {
            if (page != 0)
                link = link.Remove(link.Length-4) + page + ".html";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.Add(new Windows.Web.Http.Headers.HttpProductInfoHeaderValue("Mozilla / 5.0(Windows NT 10.0; WOW64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 45.0.2454.101 Safari / 537.36"));
            string response = await client.GetStringAsync(new Uri(link));
            client.Dispose();
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(response);

            //Chon ra the chua danh sach bai hat
            var trackList = from ultag in doc.DocumentNode.Descendants("ul").Where(x => x.Attributes["class"] != null && x.Attributes["class"].Value == "list_item_music")
                        from litag in ultag.ChildNodes.Where(x => x.Name == "li")
                        from h3tag in litag.ChildNodes[1].ChildNodes
                         select new Track()
                         {
                             Title = h3tag.ChildNodes[0].InnerText,
                             Info = h3tag.ChildNodes[0].Attributes["href"].Value,
                             ArtistLink = h3tag.ChildNodes[2].Attributes["href"].Value,
                             Artist = h3tag.ChildNodes[2].InnerText
                         };    
            return trackList.ToList();
        }
        public static void SearchingFor(string keyword, SearchType type)
        {

        }         
        public static async Task<string[]> GetSongDownloadLinkAsync(string link)
        {
            string[] result = new string[2];
            try
            {
                string id = link.Split('.')[3];
                string link320 = "http://www.nhaccuatui.com/download/song/" + id;
                HttpClient hc = new HttpClient();
                var tmp1 = hc.GetStringAsync(new Uri(link320, UriKind.Absolute));
                var tmp2 = hc.GetStringAsync(new Uri(link320 + "_128", UriKind.Absolute));

                string rep = await tmp1;
                StringBuilder sb = new StringBuilder(rep.Split('\"')[9]);
                sb.Replace("\\", "");
                if (sb.ToString().Remove(4) == "http")
                    result[1] = sb.ToString(); 
                else
                    result[1] = null;

                rep = await tmp2;
                sb = new StringBuilder(rep.Split('\"')[9]);
                sb.Replace("\\", "");
                if (sb.ToString().Remove(4) == "http")
                    result[0] = sb.ToString();
                else
                    result[0] = null;
                hc.Dispose();

                ////Lay link nhac ban quyen
                //if (result[0] == null && result[1] == null)
                //{
                //    Track tmp = new Track() { Info = link };
                //    tmp.GetTrackDetail();
                //    result[0] = tmp.Location;
                //}

                return result;
            }
            catch (Exception) { }
            //Khong co ket qua
            return null;
        }

        public static async Task<List<Topic>> GetListTopicAsync()
        {
            string link = "http://www.nhaccuatui.com/chu-de.html";
            string response;
            using (HttpClient hc = new HttpClient())
            {
                hc.DefaultRequestHeaders.UserAgent.Add(new Windows.Web.Http.Headers.HttpProductInfoHeaderValue("Mozilla / 5.0(Windows NT 10.0; WOW64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 45.0.2454.101 Safari / 537.36"));
                response = await hc.GetStringAsync(new Uri(link, UriKind.Absolute));
            }

            HtmlDocument hdoc = new HtmlDocument();
            hdoc.LoadHtml(response);

            HtmlNode ultag = (from divtags in hdoc.DocumentNode.Descendants("div").Where(x => x.Attributes[0].Value == "fram_select")
                                           select divtags).First().ChildNodes[1];
            var topicList = from litag in ultag.ChildNodes.Where(x => x.Name == "li")
                            select new Topic()
                            {
                                Title = litag.ChildNodes[1].ChildNodes[3].ChildNodes[1].InnerText,
                                Link = litag.ChildNodes[3].Attributes["href"].Value,
                                Cover = litag.ChildNodes[3].ChildNodes[3].Attributes["src"].Value
                            };
            return topicList.ToList();

        }

    }
}
