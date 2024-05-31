using BookingApp.Services;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BookingApp.WPF.Web.Views {
    /// <summary>
    /// Interaction logic for ForumsPage.xaml
    /// </summary>
    public partial class ForumsPage : Page {

        public List<ForumDTO> _forumDTOs { get; set; } = new List<ForumDTO>();
        public List<LocationDTO> _locationDTOs { get; set; } = new List<LocationDTO>();

        private readonly ForumService _forumService = ServicesPool.GetService<ForumService>();
        private readonly LocationService _locationService = ServicesPool.GetService<LocationService>();
        private readonly UserService _userService = ServicesPool.GetService<UserService>();

        public ForumsPage() {
            InitializeComponent();
            UpdateLocationDTOs();
            Update();
        }

        private void UpdateLocationDTOs() {
            var locations = _locationService.GetAll();
            _locationDTOs = locations.Select(l => new LocationDTO(l)).ToList();
            _locationDTOs.Insert(0, new LocationDTO());
            comboBoxLocation.ItemsSource = _locationDTOs;
            comboBoxLocation.SelectedIndex = 0;
        }

        public void Update() {
            if (comboBoxLocation.SelectedIndex == 0) {
                _forumDTOs = _forumService.GetAll().Select(f => new ForumDTO(f)).ToList();
            } else {
                _forumDTOs = _forumService.GetByLocationId(_locationDTOs[comboBoxLocation.SelectedIndex].Id).Select(f => new ForumDTO(f)).ToList();
            }

            foreach (var forum in _forumDTOs) {
                var loc = _locationDTOs.Find(l => l.Id == forum.LocationId);
                forum.Location = loc;
                forum.Username = _userService.GetById(forum.CreatorId).Username;
                forum.CommentNum = forum.GuestCommentNum + forum.OwnerCommentNum;
            }

            itemsControlForums.ItemsSource = _forumDTOs;
        }

        private void LocationSelectionChanged(object sender, SelectionChangedEventArgs e) {
            Update();
        }

        public void ButtonCreateForumClick(object sender, RoutedEventArgs e) {
            rectBlurBackground.Visibility = Visibility.Visible;

            GuestMainWindow window = (GuestMainWindow)Window.GetWindow(this);

            mainGrid.Children.Add(new CreateForumModalDialog(this, _locationDTOs, window.GuestId));
        }

        public void CloseModalDialog() {
            rectBlurBackground.Visibility = Visibility.Hidden;
            mainGrid.Children.RemoveAt(mainGrid.Children.Count - 1);
        }
    }
}
