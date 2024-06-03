using BookingApp.Domain.Model;
using BookingApp.Services;
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

namespace BookingApp.WPF.Web.Views {
    /// <summary>
    /// Interaction logic for CreateForumModalDialog.xaml
    /// </summary>
    public partial class CreateForumModalDialog : UserControl {

        private ForumsPage _parentPage;
        private int _guestId { get; set; }

        private readonly ForumService _forumService = ServicesPool.GetService<ForumService>();
        private readonly CommentService _commentService = ServicesPool.GetService<CommentService>();

        public List<LocationDTO> LocationDTOs { get; set; }

        public CreateForumModalDialog(ForumsPage parentPage, List<LocationDTO> locationDTOs, int guestId) {
            InitializeComponent();
            _parentPage = parentPage;
            LocationDTOs = locationDTOs;
            buttonConfirm.IsEnabled = false;
            _guestId = guestId;

            comboBoxLocation.ItemsSource = LocationDTOs;
        }

        private void ButtonCancelClick(object sender, RoutedEventArgs e) {
            _parentPage.CloseModalDialog();
        }

        private void ButtonConfirmClick(object sender, RoutedEventArgs e) {

            LocationDTO selectedLocation = comboBoxLocation.SelectedItem as LocationDTO;
            Forum forum = new Forum(_guestId, selectedLocation.Id, textBoxTitle.Text);
            int forumId = _forumService.Save(forum).Id;

            Comment comment = new Comment(DateTime.Now, textBoxComment.Text, _guestId, forumId);
            _commentService.Save(comment);

            _parentPage.Update();
            App.NotificationService.ShowSuccess("Forum created successfully!");
            _parentPage.CloseModalDialog();
        }

        private void LocationSelectionChanged(object sender, SelectionChangedEventArgs e) {
            CheckInput();
        }

        private void TitleTextChanged(object sender, TextChangedEventArgs e) {
            CheckInput();
        }

        private void CheckInput() {
            buttonConfirm.IsEnabled = comboBoxLocation.SelectedIndex != 0 && !string.IsNullOrEmpty(textBoxTitle.Text) && !string.IsNullOrEmpty(textBoxComment.Text);
        }
    }
}
