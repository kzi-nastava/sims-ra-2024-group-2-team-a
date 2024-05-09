using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class TouristStatistics {
        public double AcceptedPercentage { get; set; }
        public double NotAcceptedPercentage { get; set; }
        public double AveragePassengerNumber { get; set; }
        public TouristStatistics() { }
        public TouristStatistics(double acceptedPercentage, double notAcceptedPercentage, double averagePassengerNumber) {
            AcceptedPercentage = acceptedPercentage;
            NotAcceptedPercentage = notAcceptedPercentage;
            AveragePassengerNumber = averagePassengerNumber;
        }
    }
}
