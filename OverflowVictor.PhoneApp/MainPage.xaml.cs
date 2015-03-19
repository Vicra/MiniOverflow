using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Windows.Phone.Speech.VoiceCommands;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using OverflowVictor.PhoneApp.Resources;
using OverflowVictor.PhoneApp.ViewModels;

namespace OverflowVictor.PhoneApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            DataContext = App.ViewModel;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }
        

        private void Save_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Save button works!");
            //Do work for your application here.
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Settings button works!");
            //Do work for your application here.
        }

        private void Register_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Register.xaml", UriKind.Relative));
        }

        private void Login_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
        }

        private void AnswerQuestion_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Answere Question");
        }
    }
}