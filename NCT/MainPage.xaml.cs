﻿using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NCT.ViewModels;
using NCT.Models;
using Microsoft.Phone.BackgroundAudio;


namespace NCT
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            collectionView.ItemsSource = App.ViewModel.Items;
            featuresView.DataContext = App.FeaturesVM;

        }

        // Load data for the ViewModel Items
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            //#region debug
            //var track = new Track() { Info = "http://www.nhaccuatui.com/bai-hat/am-tham-ben-em-son-tung-m-tp.Sttn5Z5WPKPR.html" };
            //var tmp = await NhacCuaTui.GetRelatedTrackListAsync(track);
            //#endregion
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadMainView();
            }
            if (!App.FeaturesVM.IsDataLoaded)
                await App.FeaturesVM.LoadData("http://www.nhaccuatui.com/playlist/playlist-moi.html", 0);
        }

        private void collectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LongListSelector lls = sender as LongListSelector;
            if (lls.SelectedItem == null)
                return;
            string pageLocation = (lls.SelectedItem as ItemViewModel).NavigatePage;
            NavigationService.Navigate(new Uri(pageLocation, UriKind.Relative));
            lls.SelectedItem = null;
        }

        private void featuresView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LongListSelector lls = sender as LongListSelector;
            if (lls.SelectedItem == null)
                return;
            App.PlayerPlaylistArg =lls.SelectedItem as AlbumViewModel;
            NavigationService.Navigate(new Uri("/Views/Player.xaml", UriKind.Relative));
            lls.SelectedItem = null;
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