using NCT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCT.ViewModels
{
    public class ListOfTopicViewModel : ViewModel
    {

        private ObservableCollection<TopicViewModel> topicList = new ObservableCollection<TopicViewModel>();

        public  ObservableCollection<TopicViewModel> TopicList
        {
            get
            {
                return topicList;
            }
            set
            {
                if (topicList != value)
                {
                    topicList = value;
                    NotifyPropertyChanged("ListOfTopic");
                }
            }
        }

        public async Task LoadDataAsync()
        {
            if (this.topicList == null)
                this.topicList = new ObservableCollection<TopicViewModel>();
            if (TopicList.Count == 0)
            {
                TopicList.Clear();
                var topicList = await NhacCuaTui.GetListTopicAsync();
                foreach (Topic topic in topicList)
                {
                    TopicList.Add(new TopicViewModel(topic));
                }
            }
        }

    }
}
