﻿using System;
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
    }
}
