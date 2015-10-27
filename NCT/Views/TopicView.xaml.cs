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
        public TopicView()
        {
            InitializeComponent();
            Topic.DataContext = App.ListOfTopic;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (App.ListOfTopic.ListOfTopic.Count == 0)
                await App.ListOfTopic.LoadData();
        }
        private void StackPanel_ManipulationStarted(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            (sender as StackPanel).Opacity = 0.5;
        }

        private void StackPanel_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            (sender as StackPanel).Opacity = 1.0;
        }
    }
}