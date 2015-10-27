using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace NCT.Models
{
    public class Topic
    {
        public string Title { get; set; }
        public string Cover { get; set; }
        public List<Album> AlbumList { get; set; } = new List<Album>();
        public string Link { get; set; }
        public async Task GetAlbumListAsync()
        {
            if (Link == null)
                return;
            HttpClient hc = new HttpClient();
            hc.DefaultRequestHeaders.UserAgent.Add(new Windows.Web.Http.Headers.HttpProductInfoHeaderValue("Mozilla / 5.0(Windows NT 10.0; WOW64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 45.0.2454.101 Safari / 537.36"));
            string response = await hc.GetStringAsync(new Uri(Link, UriKind.Absolute));
            HtmlDocument hdoc = new HtmlDocument();
            hdoc.LoadHtml(response);

            var tmp = (from divtags in hdoc.DocumentNode.Descendants("div").Where(x => x.Attributes[0].Value == "box_pageview")
                       select divtags).First();
            string realLink = tmp.ChildNodes[2].Attributes["onclick"].Value.Split('\'')[1];
            int numpage = tmp.ChildNodes.Count - 2;

            AlbumList.Clear();
            for (int i=1; i<=numpage; i++)
            {
                string link = realLink.Replace("page=2", "page=" + i);
                response = await hc.GetStringAsync(new Uri(link, UriKind.Absolute));
                response = response.Replace("\\n", "\n").Replace("\\/","/").Replace("\\\"","\"");
                hdoc.LoadHtml(response);
                var litags = from litag in hdoc.DocumentNode.Descendants("li")
                             select new Album()
                             {
                                 Link = litag.ChildNodes[1].ChildNodes[1].Attributes["href"].Value,
                                 Cover = litag.ChildNodes[1].ChildNodes[1].ChildNodes[3].ChildNodes[0].Attributes["src"].Value,
                                 Title = litag.ChildNodes[3].ChildNodes[1].InnerText,
                                 Artist = litag.ChildNodes[3].ChildNodes[3].ChildNodes[0].InnerText
                             };
                AlbumList.AddRange(litags);
                
            }
            hc.Dispose();
            return;
        }
    }
}
