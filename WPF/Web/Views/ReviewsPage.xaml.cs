using BookingApp.Services;
using BookingApp.WPF.DTO;
using BookingApp.WPF.Utils.Reports.Guest;
using BookingApp.WPF.Web.ViewModels;
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
using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using System.Windows.Forms;

namespace BookingApp.WPF.Web.Views {
    /// <summary>
    /// Interaction logic for ReviewsPage.xaml
    /// </summary>
    public partial class ReviewsPage : Page {

        public ObservableCollection<ReviewCardViewModel> ReviewCards { get; set; } = new ObservableCollection<ReviewCardViewModel>();

        private int _guestId;

        private readonly AccommodationReviewService _reviewService = ServicesPool.GetService<AccommodationReviewService>();
        private readonly UserService _userService = ServicesPool.GetService<UserService>();

        public ReviewsPage(int guestId) {
            InitializeComponent();
            _guestId = guestId;

            var reviews = _reviewService.GetByGuestId(guestId).OrderByDescending(r => r.Id).ToList();

            foreach(var review in reviews) {
                AccommodationReviewDTO reviewDTO = new AccommodationReviewDTO(review);

                if (!reviewDTO.IsGradedByOwner && reviewDTO.IsGradedByGuest)
                    continue;

                ReviewCards.Add(new ReviewCardViewModel(reviewDTO));
            }

            DataContext = this;
        }

        private void GenerateReportClick(object sender, RoutedEventArgs e) {
            String guestUsername = _userService.GetById(_guestId).Username;

            var guestReportGenerator = new GuestReportGenerator(guestUsername, ReviewCards.Select(r => r.Review).ToList());
            QuestPDF.Settings.License = LicenseType.Community;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            saveFileDialog.DefaultExt = "pdf";
            saveFileDialog.FileName = "GuestRatingReport.pdf";

            DialogResult? result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK) {
                string filePath = saveFileDialog.FileName;
                guestReportGenerator.GeneratePdf(filePath);
            }
        }
    }
}
