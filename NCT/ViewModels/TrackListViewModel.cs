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
    public class TrackListViewModel : ViewModel
    {
        public ObservableCollection<TrackViewModel> TrackList
        {
            get; set;
        }
        //private string name;
        //public string Name
        //{
        //    get
        //    {
        //        return name;
        //    }
        //    set
        //    {
        //        if (name != value)
        //        {
        //            name = value;
        //            NotifyPropertyChanged("TrackListNameChanged");
        //        }
        //    }
        //}
        string link;  
        public TrackListViewModel()
        {
            TrackList = new ObservableCollection<TrackViewModel>();
        }

        public bool IsDataLoaded { get; set; } = false;
        
        public async Task LoadInit(string link)
        {
            if (link == null)
                link = this.link;
            else
                this.link = link;
            List<Track> trackList = await NhacCuaTui.GetTrackByGenreAsync(link, 0);
            TrackList.Clear();
            foreach(var track in trackList)
            {
                TrackViewModel tvm = new TrackViewModel(track);
                TrackList.Add(tvm);
            }
            IsDataLoaded = true;
        }

        public async Task LoadMore()
        {                    
            List<Track> trackList = await NhacCuaTui.GetTrackByGenreAsync(link, 0);
            foreach (var track in trackList)
            {
                TrackViewModel tvm = new TrackViewModel(track);
                TrackList.Add(tvm);
            }
        }
    }
}
