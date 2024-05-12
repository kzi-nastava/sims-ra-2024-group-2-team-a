using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public static class StatisticsCalculator
    {
        public static TouristStatistics CalculateTouristStatistics(IEnumerable<TourRequest> accepted, IEnumerable<TourRequest> all) {
            var acceptedPercentage = CalculatePercentage(accepted, all);

            return new TouristStatistics(acceptedPercentage, 1 - acceptedPercentage, CalculateAverage(accepted, r => r.PassengerNumber));
        }

        private static double CalculatePercentage<T>(IEnumerable<T> subset, IEnumerable<T> total) {
            int subsetCount = subset.Count();
            int totalCount = total.Count();

            if (totalCount == 0)
                return 0; 

            return ((double)subsetCount / totalCount);
        }

        private static double CalculateAverage<T>(IEnumerable<T> source, Func<T, double> selector) {
            if (source == null || !source.Any())
                throw new ArgumentException("Source collection cannot be null or empty");

            if (selector == null)
                throw new ArgumentException("Selector function cannot be null");

            return source.Select(selector).Average();
        }

        public static void CalculateRequestStats(int yearMonth, List<int> stats) {
            switch (yearMonth) {
                case 2021:
                case 1:
                    stats[0]++;
                    break;

                case 2022:
                case 2:
                    stats[1]++;
                    break;

                case 2023:
                case 3:
                    stats[2]++;
                    break;

                case 2024:
                case 4:
                    stats[3]++;
                    break;
                case 2025:
                case 5:
                    stats[4]++;
                    break;

                case 6:
                    stats[5]++;
                    break;

                case 7:
                    stats[6]++;
                    break;

                case 8:
                    stats[7]++;
                    break;

                case 9:
                    stats[8]++;
                    break;

                case 10:
                    stats[9]++;
                    break;

                case 11:
                    stats[10]++;
                    break;

                case 12:
                    stats[11]++;
                    break;

                default:
                    break;

            }
        }
    }
}
