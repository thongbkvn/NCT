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

        private ObservableCollection<TopicViewModel> listOfTopic = new ObservableCollection<TopicViewModel>();

        public  ObservableCollection<TopicViewModel> ListOfTopic
        {
            get
            {
                return listOfTopic;
            }
            set
            {
                if (listOfTopic != value)
                {
                    listOfTopic = value;
                    NotifyPropertyChanged("ListOfTopic");
                }
            }
        }

        public async Task LoadData()
        {
            if (listOfTopic == null)
                listOfTopic = new ObservableCollection<TopicViewModel>();
            ListOfTopic.Clear();
            var topicList = await NhacCuaTui.GetListTopicAsync();
            foreach (Topic topic in topicList)
            {
                ListOfTopic.Add(new TopicViewModel(topic));
            }
        }

    }
}
