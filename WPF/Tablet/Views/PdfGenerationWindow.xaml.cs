using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.Android.Views;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using IContainer = QuestPDF.Infrastructure.IContainer;
using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using Colors = QuestPDF.Helpers.Colors;
using BookingApp.WPF.DTO;
using System.Diagnostics;
using Syncfusion.Windows.Controls;
using System.Globalization;
using System.Data.Common;

namespace BookingApp.WPF.Tablet.Views {
    /// <summary>
    /// Interaction logic for PdfGenerationWindow.xaml
    /// </summary>
    public partial class PdfGenerationWindow : Window, INotifyPropertyChanged {

        private LocationService _locationService = ServicesPool.GetService<LocationService>();
        private LanguageService _languageService = ServicesPool.GetService<LanguageService>();
        private UserService _userService = ServicesPool.GetService<UserService>();
        private AccommodationReviewService _reviewService = ServicesPool.GetService<AccommodationReviewService>();
        private TourService _tourService = ServicesPool.GetService<TourService>();

        private string _filePath;
        public string FilePath {
            get {
                return _filePath;
            }
            set {
                if (value != _filePath) {
                    _filePath = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isEnabled;
        public bool GenerateEnabled {
            get {
                return _isEnabled;
            }
            set {
                if (value != _isEnabled) {
                    _isEnabled = value;
                    OnPropertyChanged();
                }
            }
        }
        private DateTime fromDate, toDate = DateTime.MinValue;
        private User _user;
        private List<Tour> tours;
        private int index = 0;
        public PdfGenerationWindow(int userId) {
            InitializeComponent();
            DataContext = this;
            GenerateEnabled = false;
            _user = _userService.GetById(userId);
            tours = _tourService.GetScheduledBetween(userId, fromDate, toDate);
        }
        private void OpenFolder_Click(object sender, RoutedEventArgs e) {
            using (var dialog = new FolderBrowserDialog()) {
                dialog.Description = "Select pdf generation folder";
                dialog.ShowNewFolderButton = true;
                dialog.RootFolder = Environment.SpecialFolder.Desktop;
                dialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath)) {
                    FilePath = dialog.SelectedPath;
                    FilePath = FilePath.Replace('\\', '/');
                    GenerateEnabled = true;
                }
            }
        }
        private void Cancel_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Cancel_Executed(object sender, ExecutedRoutedEventArgs e) {
            this.Close();
        }

        private void datePickerFrom_SelectedDateChanged(object sender, SelectionChangedEventArgs e) {
            fromDate = sender.ToDateTime();
        }

        private void datePickerTo_SelectedDateChanged(object sender, SelectionChangedEventArgs e) {
            toDate = sender.ToDateTime();
        }
        private void Generate_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            if(FilePath == null || fromDate == DateTime.MinValue || toDate == DateTime.MinValue) {
                e.CanExecute = false;
            }
            else {
                e.CanExecute = true;
            }
        }

        private void Generate_Executed(object sender, ExecutedRoutedEventArgs e) {
            GeneratePdf();

            AndroidYesNoDialog androidDialogWindow = new AndroidYesNoDialog("Pdf generated successfully.\nDo you want to open it immediately?");
            bool? result = androidDialogWindow.ShowDialog();

            if (result == true) {
                try {
                    Process.Start(new ProcessStartInfo(FilePath + "/ScheduledToursReport.pdf") { UseShellExecute = true });
                }
                catch (Exception ex) {
                    AndroidDialogWindow androidDialogWindow2 = new AndroidDialogWindow("Unexpected error: pdf file unable to open!");
                    androidDialogWindow2.ShowDialog();
                }
            }

            this.Close();
        }

        
        private void GeneratePdf() {
            QuestPDF.Settings.License = LicenseType.Community;

            Document.Create(container => {
                container.Page(page => {
                    page.Margin(2, Unit.Centimetre);
                    page.Size(PageSizes.A4);
                    page.DefaultTextStyle(x => x.FontSize(14));
                    page.Header().Element(ComposeHeader);

                    page.Content().Element(ComposeContent);

                    page.Footer().AlignCenter().Text(x => {
                        x.CurrentPageNumber();
                        x.Span(" / ");
                        x.TotalPages();
                    });
                });
            }).GeneratePdf(FilePath + "/ScheduledToursReport.pdf");

            this.Close();
        }
        private void ComposeHeader(IContainer container) {
            var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Green.Darken4);

            container.Row(row => {
                row.RelativeItem().Column(column => {
                    column.Item().Text("Scheduled Tours").Style(titleStyle);
                    column.Item().Text(text => {
                        text.Span("from: ").SemiBold();
                        text.Span(fromDate.ToString("dd-MM-yyyy"));
                        text.Span("\tto: ").SemiBold();
                        text.Span(toDate.ToString("dd-MM-yyyy"));
                    });

                    column.Item().Text(text => {
                        text.Span("\nIssue date: ").SemiBold();
                        text.Span(DateTime.Now.ToString());
                    });

                    column.Item().Text(text => {
                        text.Span("Username: ").SemiBold();
                        text.Span(_user.Username);
                    });
                });
            });
        }

        private void ComposeContent(IContainer container) {
            List<Tour> tours = _tourService.GetScheduledBetween(_user.Id, fromDate, toDate);

            container
                .PaddingVertical(20)
                .Background(Colors.Grey.Lighten3)
                .AlignTop()
                .AlignCenter().Column
                    (column => {
                         column.Spacing(10);
                         column.Item().Text("TOURS").AlignCenter().Underline(true).Bold();
                         

                        while (index < tours.Count()) {
                            column.Item().Element(ComposeTable);
                            index++;
                            column.Spacing(40);
                        }

                    });
        }

        private AccommodationType accommodationType = AccommodationType.apartment;

        private void ComposeTable(IContainer container) {
            List<Tour> tours = _tourService.GetScheduledBetween(_user.Id, fromDate, toDate);


            container
                .Table(table => {

                    table.ColumnsDefinition(columns => {
                        columns.RelativeColumn(1);
                        columns.RelativeColumn(5);
                    });
                    table.Cell().Element(CellStyle).Text("#" + (index + 1).ToString()).Bold();
                    table.Cell().Element(EmptyCellStyle).Text("");

                    table.Cell().Element(CellStyle).Text("Name:").SemiBold();
                    table.Cell().Element(CellStyle).Text(tours[index].Name).SemiBold();

                    table.Cell().Element(CellStyle).Text("Start:");
                    table.Cell().Element(CellStyle).Text(tours[index].Beggining.ToString("dd-MM-yyyy"));

                    table.Cell().Element(CellStyle).Text("Duration:");
                    table.Cell().Element(CellStyle).Text(tours[index].Duration.ToString() + " hours");

                    Location location = _locationService.GetById(tours[index].LocationId);
                    string locationName = location.City + ", " + location.Country;
                    string languageName = _languageService.GetById(tours[index].LanguageId).Name;

                    table.Cell().Element(CellStyle).Text("Location:");
                    table.Cell().Element(CellStyle).Text(locationName);

                    table.Cell().Element(CellStyle).Text("Language:");
                    table.Cell().Element(CellStyle).Text(languageName);

                    table.Cell().Element(CellStyle).Text("Current Tourists:");
                    table.Cell().Element(CellStyle).Text(tours[index].CurrentTouristNumber.ToString());

                    table.Cell().Element(CellStyle).Text("Description: ");
                    table.Cell().Element(CellStyle).Text(tours[index].Description);


                    static IContainer OverallCellStyle(IContainer container) {
                        return container.BorderTop(1).BorderColor(Colors.Grey.Medium).PaddingVertical(5);
                    }
                    static IContainer CellStyle(IContainer container) {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Medium).PaddingVertical(5);
                    }
                    static IContainer EmptyCellStyle(IContainer container) {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Medium).PaddingVertical(10);
                    }
                });
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
       
    }
}
