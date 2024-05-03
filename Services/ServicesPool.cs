using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services {
    public static class ServicesPool {
        private static Dictionary<Type, object> _services = new Dictionary<Type, object>();

        static ServicesPool() {
            _services[typeof(AccommodationService)] = new AccommodationService(RepositoryInjector.GetInstance<IAccommodationRepository>());
            _services[typeof(LocationService)] = new LocationService(RepositoryInjector.GetInstance<ILocationRepository>());
            _services[typeof(RescheduleRequestService)] = new RescheduleRequestService(RepositoryInjector.GetInstance<IRescheduleRequestRepository>());
            _services[typeof(OwnerService)] = new OwnerService(RepositoryInjector.GetInstance<IOwnerRepository>());
            _services[typeof(LanguageService)] = new LanguageService();
            _services[typeof(PassengerService)] = new PassengerService();
            _services[typeof(PointOfInterestService)] = new PointOfInterestService();
            _services[typeof(TourService)] = new TourService();
            _services[typeof(TourReservationService)] = new TourReservationService();
            _services[typeof(TourReviewService)] = new TourReviewService();
            _services[typeof(VoucherService)] = new VoucherService();
            _services[typeof(AccommodationStatisticsService)] = new AccommodationStatisticsService();
            _services[typeof(GuestService)] = new GuestService(RepositoryInjector.GetInstance<IGuestRepository>());
            _services[typeof(TourService)] = new TourService();
            _services[typeof(TourReviewService)] = new TourReviewService();
            _services[typeof(ReservationRecommenderService)] = new ReservationRecommenderService(RepositoryInjector.GetInstance<IAccommodationReservationRepository>());
            _services[typeof(TourRequestService)] = new TourRequestService(RepositoryInjector.GetInstance<ITourRequestRepository>());


            _services[typeof(UserService)] = new UserService(
                RepositoryInjector.GetInstance<IUserRepository>(),
                GetService<OwnerService>(),
                GetService<GuestService>()
                );

            _services[typeof(AccommodationReservationService)] = new AccommodationReservationService(
                RepositoryInjector.GetInstance<IAccommodationReservationRepository>(),
                GetService<RescheduleRequestService>(),
                GetService<AccommodationService>(),
                GetService<ReservationRecommenderService>(),
                GetService<AccommodationStatisticsService>(),
                GetService<GuestService>()
                );

            _services[typeof(AccommodationRenovationService)] = new AccommodationRenovationService(
                RepositoryInjector.GetInstance<IAccommodationRenovationRepository>(),
                GetService<AccommodationReservationService>(),
                GetService<AccommodationService>()
                );

            _services[typeof(ReviewService)] = new ReviewService(
                RepositoryInjector.GetInstance<IReviewRepository>(),
                GetService<AccommodationReservationService>(),
                GetService<OwnerService>(),
                GetService<AccommodationStatisticsService>()
                );

            _services[typeof(NotificationService)] = new NotificationService(
                RepositoryInjector.GetInstance<INotificationRepository>(),
                GetService<RescheduleRequestService>(),
                GetService<AccommodationReservationService>()
                );
        }

        public static T GetService<T>() {
            return (T) _services[typeof(T)];
        }
    }
}
