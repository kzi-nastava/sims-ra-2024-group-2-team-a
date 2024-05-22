using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using IContainer = QuestPDF.Infrastructure.IContainer;

namespace BookingApp.WPF.Android.Views {
    /// <summary>
    /// Interaction logic for AccomodationPage.xaml
    /// </summary>
    public partial class AccommodationPage : Page {
        public Frame mainFrame;

        private readonly User _user;

        private AccommodationService accommodationService = ServicesPool.GetService<AccommodationService>();

        private readonly AccommodationStatisticsService _statisticsService = ServicesPool.GetService<AccommodationStatisticsService>();  

        private LocationService locationService = ServicesPool.GetService<LocationService>();

        private AccommodationReviewService _reviewService = ServicesPool.GetService<AccommodationReviewService>();
        public ObservableCollection<AccommodationDTO> AccommodationDTOs { get; set; }
        public AccommodationDTO SelectedAccommodation { get; set; }

        public AccommodationPage(Frame mFrame, User user) {
            InitializeComponent();
            mainFrame = mFrame;
            this._user = user;
            DataContext = this;
            AccommodationDTOs = new ObservableCollection<AccommodationDTO>();

            Update();
        }

        public void Update() {
            foreach (var acc in accommodationService.GetByOwnerId(_user.Id)) {
                AccommodationDTO accDTO = new AccommodationDTO(acc);
                Location location = locationService.GetById(acc.LocationId);
                accDTO.SetDisplayLocation(location.City, location.Country);
                AccommodationDTOs.Add(accDTO);
            }
        }

        private void AddAccomodation_Click(object sender, RoutedEventArgs e) {
            mainFrame.Content = new AddAccommodationPage(mainFrame, _user, null);
        }

        private void RenovationsButton_Click(object sender, RoutedEventArgs e) {
            if (SelectedAccommodation == null) {
                return;
            }
            else {
                mainFrame.Content = new AccommodationRenovationPage(SelectedAccommodation, mainFrame);
            }
        }

        private void StatisticsButton_Click(object sender, RoutedEventArgs e) {
            if (SelectedAccommodation == null ) {
                return;
            }
            else if (_statisticsService.IsStatisticEmpty(SelectedAccommodation.Id)) {
                AndroidDialogWindow dialogWindow = new AndroidDialogWindow("Selected accommodation does not have any required statistic!");
                dialogWindow.ShowDialog();
            }
            else {
                mainFrame.Content = new AccommodationStatisticsPage(SelectedAccommodation);
            }
        }

        private void GuidanceButton_Click(object sender, RoutedEventArgs e) {
            if (_statisticsService.GetHottestAndColdestLocations(_user.Id)[0] == -1) {
                AndroidDialogWindow dialogWindow = new AndroidDialogWindow("Insufficent accommodations registered on various locations to enable this feature!");
                dialogWindow.ShowDialog();
                return;
            }
            else if (_statisticsService.GetHottestAndColdestLocations(_user.Id)[0] == -2) {
                AndroidDialogWindow dialogWindow = new AndroidDialogWindow("Insufficent statistics recorded to enable this feature!");
                dialogWindow.ShowDialog();
                return;
            }

            mainFrame.NavigationService.Navigate(new AccommodationGuidancePage(_user,mainFrame));
        }

        private void GeneratePdfButton_Click(object sender, RoutedEventArgs e) {
            QuestPDF.Settings.License = LicenseType.Community;

            Document.Create(container => {
                container.Page(page => {
                    page.Margin(2, Unit.Centimetre);
                    page.Size(PageSizes.A4);
                    page.DefaultTextStyle(x => x.FontSize(14));
                    page.Header().Text("Average grades for your accommodations")
                    .SemiBold().FontSize(20)
                    .FontColor(Colors.Black).AlignCenter();

                    page.Content().Element(ComposeContent);
                });
            }).GeneratePdf("C:/Users/Strahinja/Desktop/niggers.pdf");
        }

        void ComposeContent(IContainer container) {
            container
                .PaddingVertical(20)
                .Background(Colors.Grey.Lighten3)
                .AlignTop()
                .AlignCenter().Column
                    (column =>
                {
                    column.Spacing(5);
                    column.Item().Text(accommodationType.ToString().ToUpper()).AlignCenter().Underline(true).Bold();
                    column.Item().Element(ComposeTable);

                    accommodationType = AccommodationType.house;
                    column.Item().PaddingTop(20).Text(accommodationType.ToString().ToUpper()).AlignCenter().Underline(true).Bold();
                    column.Item().Element(ComposeTable);

                    accommodationType = AccommodationType.hut;
                    column.Item().PaddingTop(20).Text(accommodationType.ToString().ToUpper()).AlignCenter().Underline(true).Bold();
                    column.Item().Element(ComposeTable);
                });
        }

        private AccommodationType accommodationType = AccommodationType.apartment;

        void ComposeTable(IContainer container) {
            int id = 1;
            Dictionary<Accommodation, double> data = _reviewService.GetAverageAccommodationGradesByOwnerId(_user.Id, accommodationType);

            container
                .Table(table => {
                    table.ColumnsDefinition(columns => {
                        columns.ConstantColumn(25);
                        columns.RelativeColumn(2);
                        columns.RelativeColumn(2);
                        columns.RelativeColumn();
                    });


                    table.Header(header => {
                        header.Cell().Element(CellStyle).Text("#");
                        header.Cell().Element(CellStyle).Text("Accommodation name");
                        header.Cell().Element(CellStyle).Text("Location");
                        header.Cell().Element(CellStyle).AlignCenter().Text("Average");

                        static IContainer CellStyle(IContainer container) {
                            return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                        }
                    });

                    foreach (var item in data) {
                        table.Cell().Element(CellStyle).Text(id.ToString());
                        id++;
                        table.Cell().Element(CellStyle).Text(item.Key.Name);

                        LocationDTO locationDTO = new LocationDTO(locationService.GetById(item.Key.LocationId));

                        table.Cell().Element(CellStyle).Text(locationDTO.LocationInfoTemplate);
                        table.Cell().Element(CellStyle).AlignCenter().Text(System.Math.Round(item.Value, 2).ToString());

                        static IContainer CellStyle(IContainer container) {
                            return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                        }
                    }
                });
        }

    }
}
