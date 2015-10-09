using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using NCT.Resources;

namespace NCT.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.Items = new ObservableCollection<ItemViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ItemViewModel> Items { get; private set; }


        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadMainView()
        {
            // Sample data; replace with real data
            this.Items.Add(new ItemViewModel() { Content = "nowplaying", NavigatePage = "/Views/Player.xaml"});
            this.Items.Add(new ItemViewModel() { Content = "album", NavigatePage = "/Views/AlbumView.xaml"});
            this.Items.Add(new ItemViewModel() { Content = "artist", NavigatePage = "Habitant inceptos interdum lobortis"});
            this.Items.Add(new ItemViewModel() { Content = "song", NavigatePage = "Nascetur pharetra placerat pulvinar"});
            this.Items.Add(new ItemViewModel() { Content = "favourite", NavigatePage = "Maecenas praesent accumsan bibendum"});
            this.Items.Add(new ItemViewModel() { Content = "history", NavigatePage = "Dictumst eleifend facilisi faucibus"});
            this.IsDataLoaded = true;
        }

        public void LoadAlbumView()
        {
            this.Items.Add(new ItemViewModel() { Content = "news", NavigatePage = "http://www.nhaccuatui.com/playlist/playlist-moi.html" });
            this.Items.Add(new ItemViewModel() { Content = "v-pop", NavigatePage = "http://www.nhaccuatui.com/playlist/nhac-tre-moi.html" });
            this.Items.Add(new ItemViewModel() { Content = "romantic", NavigatePage = "http://www.nhaccuatui.com/playlist/tru-tinh-moi.html" });
            this.Items.Add(new ItemViewModel() { Content = "trinh cong son", NavigatePage = "http://www.nhaccuatui.com/playlist/nhac-trinh-moi.html" });
            this.Items.Add(new ItemViewModel() { Content = "rap", NavigatePage = "http://www.nhaccuatui.com/playlist/rap-viet-moi.html" });
            this.Items.Add(new ItemViewModel() { Content = "rock", NavigatePage = "http://www.nhaccuatui.com/playlist/rock-viet-moi.html" });
            this.Items.Add(new ItemViewModel() { Content = "us & uk", NavigatePage = "http://www.nhaccuatui.com/playlist/au-my-moi.html" });
            this.Items.Add(new ItemViewModel() { Content = "korean", NavigatePage = "http://www.nhaccuatui.com/playlist/han-quoc-moi.html" });
            this.Items.Add(new ItemViewModel() { Content = "chinese", NavigatePage = "http://www.nhaccuatui.com/playlist/nhac-hoa-moi.html" });
            this.Items.Add(new ItemViewModel() { Content = "japanese", NavigatePage = "http://www.nhaccuatui.com/playlist/nhac-nhat-moi.html" });
            this.Items.Add(new ItemViewModel() { Content = "none lyric", NavigatePage = "http://www.nhaccuatui.com/playlist/khong-loi-moi.html" });
            this.Items.Add(new ItemViewModel() { Content = "soundtrack", NavigatePage = "http://www.nhaccuatui.com/playlist/nhac-phim-moi.html" });
            this.Items.Add(new ItemViewModel() { Content = "other", NavigatePage = "http://www.nhaccuatui.com/playlist/the-loai-khac-moi.html" });
            IsDataLoaded = true;
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