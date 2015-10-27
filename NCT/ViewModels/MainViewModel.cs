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
            this.Items.Add(new ItemViewModel() { Content = "nowplaying", LineTwo = "/Views/Player.xaml"});
            this.Items.Add(new ItemViewModel() { Content = "genre", LineTwo = "/Views/GenreView.xaml"});
            this.Items.Add(new ItemViewModel() { Content = "artist", LineTwo = "/Views/ArtistView.xaml"});
            this.Items.Add(new ItemViewModel() { Content = "topic", LineTwo = "/Views/TopicView.xaml"});
            //this.Items.Add(new ItemViewModel() { Content = "favourite", LineTwo = "Maecenas praesent accumsan bibendum"});
            //this.Items.Add(new ItemViewModel() { Content = "history", LineTwo = "Dictumst eleifend facilisi faucibus"});
            this.IsDataLoaded = true;
        }

        public void LoadAlbumView()
        {
            this.Items.Add(new ItemViewModel() { Content = "news", LineTwo = "http://www.nhaccuatui.com/playlist/playlist-moi.html", LineThree = "http://www.nhaccuatui.com/bai-hat/bai-hat-moi.html" });
            this.Items.Add(new ItemViewModel() { Content = "v-pop", LineTwo = "http://www.nhaccuatui.com/playlist/nhac-tre-moi.html", LineThree = "http://www.nhaccuatui.com/bai-hat/nhac-tre-moi.html" });
            this.Items.Add(new ItemViewModel() { Content = "us & uk", LineTwo = "http://www.nhaccuatui.com/playlist/au-my-moi.html", LineThree = "http://www.nhaccuatui.com/bai-hat/pop-moi.html" });
            this.Items.Add(new ItemViewModel() { Content = "korean", LineTwo = "http://www.nhaccuatui.com/playlist/han-quoc-moi.html", LineThree = "http://www.nhaccuatui.com/bai-hat/han-quoc-moi.html" });
            this.Items.Add(new ItemViewModel() { Content = "romantic", LineTwo = "http://www.nhaccuatui.com/playlist/tru-tinh-moi.html", LineThree = "http://www.nhaccuatui.com/bai-hat/tru-tinh-moi.html" });
            this.Items.Add(new ItemViewModel() { Content = "none lyric", LineTwo = "http://www.nhaccuatui.com/playlist/khong-loi-moi.html", LineThree = "http://www.nhaccuatui.com/bai-hat/khong-loi-moi.html" });
            this.Items.Add(new ItemViewModel() { Content = "trinh cong son", LineTwo = "http://www.nhaccuatui.com/playlist/nhac-trinh-moi.html", LineThree = "http://www.nhaccuatui.com/bai-hat/nhac-trinh-moi.html" });
            this.Items.Add(new ItemViewModel() { Content = "rap viet", LineTwo = "http://www.nhaccuatui.com/playlist/rap-viet-moi.html", LineThree = "http://www.nhaccuatui.com/bai-hat/rap-viet-moi.html" });
            this.Items.Add(new ItemViewModel() { Content = "rock viet", LineTwo = "http://www.nhaccuatui.com/playlist/rock-viet-moi.html", LineThree = "http://www.nhaccuatui.com/bai-hat/rock-viet-moi.html" });
            this.Items.Add(new ItemViewModel() { Content = "chinese", LineTwo = "http://www.nhaccuatui.com/playlist/nhac-hoa-moi.html", LineThree = "http://www.nhaccuatui.com/bai-hat/nhac-hoa-moi.html" });
            this.Items.Add(new ItemViewModel() { Content = "japanese", LineTwo = "http://www.nhaccuatui.com/playlist/nhac-nhat-moi.html", LineThree = "http://www.nhaccuatui.com/bai-hat/nhac-nhat-moi.html" });
            this.Items.Add(new ItemViewModel() { Content = "other", LineTwo = "http://www.nhaccuatui.com/playlist/the-loai-khac-moi.html", LineThree = "http://www.nhaccuatui.com/bai-hat/the-loai-khac-moi.html" });
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