using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCT.ViewModels;
using System.ComponentModel;
using System.Collections.ObjectModel;
using NCT.Models;
using Windows.Web.Http;
using System.Diagnostics;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace NCT.ViewModels
{
    public class TrackListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TrackViewModel> TrackList
        {
            get; set;
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
                    NotifyPropertyChanged("TrackListNameChanged");
                }
            }
        }

        public TrackListViewModel()
        {
            TrackList = new ObservableCollection<TrackViewModel>();
        }

        
        public async void LoadData(string link)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.Add(new Windows.Web.Http.Headers.HttpProductInfoHeaderValue("Mozilla / 5.0(Windows NT 10.0; WOW64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 45.0.2454.101 Safari / 537.36"));
            string response = await client.GetStringAsync(new Uri(link));
            client.Dispose();
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(response);

            //Chon ra the chua danh sach bai hat
            var ulTags = from ul in doc.DocumentNode.Descendants("ul").Where(x => x.Attributes["class"].Value == "list_show_chart")
                         select ul;
            var ulTag = ulTags.First();
            int numberOfSong = 0;
            foreach (var node in ulTag.ChildNodes)
                if (node.Name == "li")
                    numberOfSong++;
            TrackViewModel[] trackViewModelList = new TrackViewModel[numberOfSong];

            //Moi node la 1 the li
            foreach (HtmlNode node in ulTag.ChildNodes)
                //Loai bo the #text
                if (node.Name == "li")
                {
                    //Chon ra the div chua ten bai hat
                    if (node.ChildNodes[5].ChildNodes[1].Attributes["title"] != null)
                    {
                        TrackViewModel track = new TrackViewModel();
                        track.Title = node.ChildNodes[5].ChildNodes[1].Attributes["title"].Value;

                        TrackList.Add(track);
                    }

                }

            //var titles = from liTag in ulTag.ChildNodes
            //             from divTitleTag in liTag.ChildNodes.Where(x =>x.Attributes["class"].Value == "box_info_field")
            //             from aTag in divTitleTag.ChildNodes;

            //string str = titles.ElementAt(0);
            //int i = 0;
            //foreach (var title in titles)
            //{
            //    trackViewModelList[i] = new TrackViewModel() { Title = title };
            //    TrackList.Add(trackViewModelList[i]);
            //    i++;
            //}



        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
