using BookingApp.Services;
using BookingApp.WPF.DTO;
using LiveCharts.Wpf;
using LiveCharts;
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

        private readonly int PieSeriesCount = 4;
        private Dictionary<LocationDTO, int> _locationCommentNums;

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
            UpdateForumDTOs();
            SetupCharts();
        }

        public void UpdateForumDTOs() {
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

            _forumDTOs = _forumDTOs.OrderByDescending(l => l.CommentNum).ToList();

            itemsControlForums.ItemsSource = _forumDTOs;
        }

        private void LocationSelectionChanged(object sender, SelectionChangedEventArgs e) {
            UpdateForumDTOs();
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
    
        public void SetupCharts() {
            SetupPieChart();
        }

        private void SetupPieChart() {
            PopularLocationsPieChart.Series = new SeriesCollection();
            _locationCommentNums = new Dictionary<LocationDTO, int>();
            var locations = _locationDTOs.GetRange(1, _locationDTOs.Count - 1);

            foreach (var location in locations) {
                _locationCommentNums.Add(location, _forumService.GetNumCommentsByLocationId(location.Id));
            }

            _locationCommentNums = _locationCommentNums.OrderByDescending(l => l.Value).ToDictionary(l => l.Key, l => l.Value);

            foreach (var pair in _locationCommentNums.Take(PieSeriesCount)) {

                PopularLocationsPieChart.Series.Add(new PieSeries {
                    Title = pair.Key.LocationInfoTemplate,
                    Values = new ChartValues<int> { pair.Value },
                });
            }

            int otherCount = _locationCommentNums.Skip(PieSeriesCount).Sum(l => l.Value);

            PopularLocationsPieChart.Series.Add(new PieSeries {
                Title = "Other",
                Values = new ChartValues<int> { otherCount },
            });
        }
    }
}
