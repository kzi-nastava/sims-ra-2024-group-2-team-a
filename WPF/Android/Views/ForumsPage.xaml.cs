﻿using BookingApp.Domain.Model;
using BookingApp.WPF.Android.ViewModels;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.Android.Views {
    /// <summary>
    /// Interaction logic for ForumsPage.xaml
    /// </summary>
    public partial class ForumsPage : Page {

        public ForumsPageViewmodel ForumsPageViewmodel { get; set; }

        private Frame mainFrame;

        private User _user;
        public ForumsPage(User user , Frame mainFrame) {
            InitializeComponent();
            ForumsPageViewmodel = new ForumsPageViewmodel(user);
            DataContext = ForumsPageViewmodel;
            _user = user;
            this.mainFrame = mainFrame;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (ForumsPageViewmodel.SelectedForum == null) {
                return;
            }

            ForumDTO forumDTO = new ForumDTO(ForumsPageViewmodel.SelectedForum);

            ForumsPageViewmodel.SelectedForum = null;
            Dispatcher.BeginInvoke(new Action(() =>
            {
                listBox.SelectedItem = ForumsPageViewmodel.SelectedForum;
            }), System.Windows.Threading.DispatcherPriority.Background);

            mainFrame.NavigationService.Navigate(new ForumCommentsPage(forumDTO, _user, ForumsPageViewmodel));
            
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e) {
            FilterForumsWindow filterForumsWindow = new FilterForumsWindow(ForumsPageViewmodel);
            filterForumsWindow.ShowDialog();
        }
    }
}
