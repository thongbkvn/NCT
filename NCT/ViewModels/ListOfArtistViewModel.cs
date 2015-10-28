using NCT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCT.ViewModels
{
    public class ListOfArtistViewModel : ViewModel
    {
        private ObservableCollection<ArtistViewModel> hotArtistList = new ObservableCollection<ArtistViewModel>();
        public ObservableCollection<ArtistViewModel> HotArtistList
        {
            get
            {
                return hotArtistList;
            }
            set
            {
                if (hotArtistList != value)
                {
                    hotArtistList = value;
                    NotifyPropertyChanged("HotArtistList");
                }
            }
        }
        private ObservableCollection<ArtistViewModel> topArtistList = new ObservableCollection<ArtistViewModel>();
        public ObservableCollection<ArtistViewModel> TopArtistList
        {
            get
            {
                return topArtistList;
            }
            set
            {
                if (topArtistList != value)
                {
                    topArtistList = value;
                    NotifyPropertyChanged("TopArtistList");
                }
            }
        }
                 
        public async Task LoadDataAsync()
        {
            if (topArtistList.Count == 0 || hotArtistList.Count == 0)
            {
                HotArtistList.Clear();
                TopArtistList.Clear();

                var tmp = await NhacCuaTui.GetListOfHotArtistAsync();
                foreach (var artist in tmp[0])
                    TopArtistList.Add(new ArtistViewModel(artist));
                foreach (var artist in tmp[1])
                    HotArtistList.Add(new ArtistViewModel(artist));
            }


        }
    }
}
