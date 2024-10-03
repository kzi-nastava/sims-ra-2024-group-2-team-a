using BookingApp.WPF.DTO;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.Utils.Reports.Tourist {
    public class PointsOfInterestComponent : IComponent {
        private List<PointOfInterestDTO> _pointsOfInterest;
        public PointsOfInterestComponent(List<PointOfInterestDTO> pointsOfInterest) {
            _pointsOfInterest = pointsOfInterest;
        }
        public void Compose(IContainer container) {
            container.Column(column => {
                column.Spacing(2);

                column.Item().BorderBottom(1).PaddingBottom(5).Text("Points of interest").SemiBold();

                foreach (var poi in _pointsOfInterest)
                    column.Item().Text($"- {poi.Name}");
            });
        }
    }
}
