using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NCT.Models;
using NCT.ViewModels;

namespace NCT.Views
{
    public partial class TopicView : PhoneApplicationPage
    {
        class Request
        {
            public static int Count;
            public int Id;
            public Request(int Id)
            {
                this.Id = Id;
                Count++;
            }
        }
        public TopicView()
        {
            InitializeComponent();
            Topic.DataContext = App.ListOfTopic;
            TopicAlbumList.DataContext = App.TopicVM;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (App.ListOfTopic.ListOfTopic.Count == 0)
                await App.ListOfTopic.LoadData();
            var tmp = App.ListOfTopic.ListOfTopic.First();
            await tmp.LoadData();
            App.TopicVM.AlbumList = tmp.AlbumList;
        }
        private void StackPanel_ManipulationStarted(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            (sender as StackPanel).Opacity = 0.5;
        }

        private void StackPanel_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            (sender as StackPanel).Opacity = 1.0;
        }

        private async void Topic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LongListSelector lls = sender as LongListSelector;
            if (lls.SelectedItem == null)
                return;
            TopicViewModel topicvm = lls.SelectedItem as TopicViewModel;
            var rq = new Request(Request.Count);
            await topicvm.LoadData();
            if (rq.Id == Request.Count - 1)
            {
                App.TopicVM.AlbumList = topicvm.AlbumList;
                topicViewPivot.SelectedIndex = 1;
            }
            lls.SelectedItem = null;
        }

        private void TopicAlbumList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LongListSelector lls = sender as LongListSelector;
            if (lls.SelectedItem == null)
                return;
            App.PlayerPlaylistArg = lls.SelectedItem as AlbumViewModel;
            NavigationService.Navigate(new Uri("/Views/Player.xaml", UriKind.Relative));
            lls.SelectedItem = null;
        }
    }
}