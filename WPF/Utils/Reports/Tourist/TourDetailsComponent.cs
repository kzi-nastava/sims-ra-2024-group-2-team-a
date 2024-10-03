using BookingApp.WPF.DTO;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.Utils.Reports.Tourist {
    internal class TourDetailsComponent : IComponent {
        private TourDTO _tourDetails;
        public TourDetailsComponent(TourDTO tourDetails) {
            _tourDetails = tourDetails;
        }
        public void Compose(IContainer container) {
            container.Column(column => {
                column.Spacing(2);

                column.Item().BorderBottom(1).PaddingBottom(5).Text("Information").SemiBold();

                column.Item().Text($"Name: {_tourDetails.Name}");
                column.Item().Text($"Location: {_tourDetails.Location.City} - {_tourDetails.Location.Country}");
                column.Item().Text($"Language: {_tourDetails.Language.Name}");
                column.Item().Text($"Date: {_tourDetails.Beggining:g} - {_tourDetails.End:g}");
                column.Item().Text($"Duration: {_tourDetails.Duration}h");
            });
        }
    }
}
