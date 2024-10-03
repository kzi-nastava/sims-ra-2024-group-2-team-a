using BookingApp.Domain.Model;
using BookingApp.Services;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace BookingApp.WPF.Android.Views {
    /// <summary>
    /// Interaction logic for PdfGenerationWindow.xaml
    /// </summary>
    public partial class PdfGenerationWindow : Window, INotifyPropertyChanged {

        private LocationService locationService = ServicesPool.GetService<LocationService>();

        private AccommodationReviewService _reviewService = ServicesPool.GetService<AccommodationReviewService>();

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

        private User _user;
        public PdfGenerationWindow(User user) {
            InitializeComponent();
            DataContext = this;
            GenerateEnabled = false;
            _user = user;
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

        private void CancelButton_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void Generate_Click(object sender, RoutedEventArgs e) {
            GeneratePdf();

            AndroidYesNoDialog androidDialogWindow = new AndroidYesNoDialog("Pdf generated successfully.\nDo you want to open it immediately?");
            bool? result = androidDialogWindow.ShowDialog();

            if (result == true) {
                try {
                    Process.Start(new ProcessStartInfo(FilePath + "/AccommodationsReport.pdf") { UseShellExecute = true });
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

                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.CurrentPageNumber();
                        x.Span(" / ");
                        x.TotalPages();
                    });
                });
            }).GeneratePdf(FilePath + "/AccommodationsReport.pdf");

            this.Close();
        }
        private void ComposeHeader(IContainer container) {
            var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Green.Darken4);

            container.Row(row => {
                row.RelativeItem().Column(column => {
                    column.Item().Text("Average grades for your accommodations").Style(titleStyle);

                    column.Item().Text(text => {
                        text.Span("Issue date: ").SemiBold();
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

        private void ComposeTable(IContainer container) {
            int id = 1;
            double sum = 0.0;
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
                        sum += item.Value;
                    }

                    if (id == 1) {
                        return;
                    }

                    table.Cell().Element(EmptyCellStyle).Text("");
                    table.Cell().Element(EmptyCellStyle).Text("");
                    table.Cell().Element(OverallCellStyle).Text("Overall: ");
                    table.Cell().Element(OverallCellStyle).AlignCenter().Text(System.Math.Round(sum/--id, 2).ToString());


                    static IContainer OverallCellStyle(IContainer container) {
                        return container.BorderTop(1).BorderColor(Colors.Grey.Medium).PaddingVertical(5);
                    }
                    static IContainer CellStyle(IContainer container) {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }

                    static IContainer EmptyCellStyle(IContainer container) {
                        return container.PaddingVertical(5);
                    }
                });
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
