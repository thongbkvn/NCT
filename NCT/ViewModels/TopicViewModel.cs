using NCT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCT.ViewModels
{
    public class TopicViewModel : ViewModel
    {
        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (title != value)
                {
                    title = value;
                    NotifyPropertyChanged("Title");
                }
            }
        }

        private string cover;
        public string Cover
        {
            get
            {
                return cover;
            }
            set
            {
                if (cover != value)
                {
                    cover = value;
                    NotifyPropertyChanged("Cover");
                }
            }
        }
        private ObservableCollection<AlbumViewModel> albumList = new ObservableCollection<AlbumViewModel>();
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

        public Topic topic;

        public TopicViewModel()
        {
        }
        public TopicViewModel(Topic topic)
        {
            this.topic = topic;
            Title = topic.Title;
            Cover = topic.Cover;
            //AlbumList.Clear();
            //if (topic.AlbumList != null)
            //{
            //    foreach (var album in topic.AlbumList)
            //        AlbumList.Add(new AlbumViewModel(album));
            //    IsDataLoaded = true;
            //}
        }

        public bool IsDataLoaded;
        public async Task LoadData()
        {
            if (topic == null)
                return;
            await topic.GetAlbumListAsync();
            AlbumList.Clear();
            foreach (var album in topic.AlbumList)  
                AlbumList.Add(new AlbumViewModel(album));     
            IsDataLoaded = true;
        }

    }
}
