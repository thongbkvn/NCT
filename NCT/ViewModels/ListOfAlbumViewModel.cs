using NCT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCT.ViewModels
{
    public class ListOfAlbumViewModel : ViewModel
    {
        private ObservableCollection<AlbumViewModel> albumList;
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
                    NotifyPropertyChanged("AlbumListChanged");
                }
            }
        }

        public ListOfAlbumViewModel()
        {
            albumList = new ObservableCollection<AlbumViewModel>();
        }
        public async Task LoadData(string link, int page = 0)
        {
            List<Album> listAlbum = await NhacCuaTui.GetListOfPlaylistAsync(link, page);
            foreach (var album in listAlbum)
                AlbumList.Add(new AlbumViewModel(album) { IsDataLoaded = false });
            IsDataLoaded = true;
        }

        
        public bool IsDataLoaded
        {
            get; set;
        }
    }
}
