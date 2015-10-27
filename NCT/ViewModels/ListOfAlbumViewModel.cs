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
        public string link;
        int page = 0;
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
                    NotifyPropertyChanged("AlbumList");
                }
            }
        }

        public ListOfAlbumViewModel()
        {
            albumList = new ObservableCollection<AlbumViewModel>();
        }
        public async Task LoadDataInit(string link)
        {
            if (link == null)
                return;
            this.link = link;
            List<Album> listAlbum = await NhacCuaTui.GetListOfPlaylistAsync(link, 0);
            AlbumList.Clear();
            foreach (var album in listAlbum)
                AlbumList.Add(new AlbumViewModel(album) { IsDataLoaded = false });
            IsDataLoaded = true;
        }

        public async Task LoadDataFeature(string link, int numberOfItem)
        {
            if (link == null)
                return;
            this.link = link;
            List<Album> listAlbum = await NhacCuaTui.GetListOfPlaylistAsync(link, 0);
            AlbumList.Clear();
            int i = 0;
            foreach (var album in listAlbum)
                if (i++ < numberOfItem)
                    AlbumList.Add(new AlbumViewModel(album) { IsDataLoaded = false });
            IsDataLoaded = true;
        }

        public async Task LoadMore()
        {
            List<Album> listAlbum = await NhacCuaTui.GetListOfPlaylistAsync(link, ++page);
            foreach (var album in listAlbum)
                AlbumList.Add(new AlbumViewModel(album) { IsDataLoaded = false });
        }
        public bool IsDataLoaded
        {
            get; set;
        }
    }
}
