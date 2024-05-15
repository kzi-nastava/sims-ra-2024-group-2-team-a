using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services {
    public static class ServicesPool {
        private static Dictionary<Type, object> _services = new Dictionary<Type, object>();

        static ServicesPool() {
            _services[typeof(AccommodationService)] = new AccommodationService(RepositoryInjector.GetInstance<IAccommodationRepository>());
            _services[typeof(LocationService)] = new LocationService(RepositoryInjector.GetInstance<ILocationRepository>());
            _services[typeof(AccommodationRescheduleRequestService)] = new AccommodationRescheduleRequestService(RepositoryInjector.GetInstance<IAccommodationRescheduleRequestRepository>());
            _services[typeof(OwnerService)] = new OwnerService(RepositoryInjector.GetInstance<IOwnerRepository>());
            _services[typeof(LanguageService)] = new LanguageService(RepositoryInjector.GetInstance<ILanguageRepository>());
            _services[typeof(PassengerService)] = new PassengerService(RepositoryInjector.GetInstance<IPassengerRepository>());
            _services[typeof(PointOfInterestService)] = new PointOfInterestService(RepositoryInjector.GetInstance<IPointOfInterestRepository>());
            _services[typeof(TourService)] = new TourService(RepositoryInjector.GetInstance<ITourRepository>());
            _services[typeof(TourReservationService)] = new TourReservationService(RepositoryInjector.GetInstance<ITourReservationRepository>());
            _services[typeof(TourReviewService)] = new TourReviewService(RepositoryInjector.GetInstance<ITourReviewRepository>());
            _services[typeof(VoucherService)] = new VoucherService(RepositoryInjector.GetInstance<IVoucherRepository>());
            _services[typeof(AccommodationStatisticsService)] = new AccommodationStatisticsService(RepositoryInjector.GetInstance<IAccommodationStatisticsRepository>());
            _services[typeof(GuestService)] = new GuestService(RepositoryInjector.GetInstance<IGuestRepository>());
            _services[typeof(ReservationRecommenderService)] = new ReservationRecommenderService(RepositoryInjector.GetInstance<IAccommodationReservationRepository>());
            _services[typeof(TourRequestService)] = new TourRequestService(RepositoryInjector.GetInstance<ITourRequestRepository>());
            _services[typeof(UserService)] = new UserService(RepositoryInjector.GetInstance<IUserRepository>());
            _services[typeof(AccommodationReservationService)] = new AccommodationReservationService(RepositoryInjector.GetInstance<IAccommodationReservationRepository>());
            _services[typeof(AccommodationRenovationService)] = new AccommodationRenovationService(RepositoryInjector.GetInstance<IAccommodationRenovationRepository>());
            _services[typeof(AccommodationReviewService)] = new AccommodationReviewService(RepositoryInjector.GetInstance<IAccommodationReviewRepository>());
            _services[typeof(NotificationService)] = new NotificationService(RepositoryInjector.GetInstance<INotificationRepository>());
            _services[typeof(ComplexTourRequestService)] = new ComplexTourRequestService(RepositoryInjector.GetInstance<IComplexTourRequestRepository>());
            _services[typeof(ForumService)] = new ForumService(RepositoryInjector.GetInstance<IForumRepository>());

            LinkAllServices();
        }

        private static void LinkAllServices() {
            GetService<TourService>().InjectService(
                GetService<PassengerService>(),
                GetService<TourReservationService>(),
                GetService<TourReviewService>(),
                GetService<PointOfInterestService>(),
                GetService<TourRequestService>(),
                GetService<NotificationService>()
                ); 

            GetService<AccommodationReservationService>().InjectServices(
                GetService<AccommodationRescheduleRequestService>(),
                GetService<AccommodationService>(),
                GetService<ReservationRecommenderService>(),
                GetService<AccommodationStatisticsService>(),
                GetService<GuestService>(),
                GetService<AccommodationReviewService>()
                );

            GetService<UserService>().InjectServices(
                GetService<OwnerService>(),
                GetService<GuestService>()
                );

            GetService<AccommodationRenovationService>().InjectServices(
                GetService<AccommodationReservationService>(),
                GetService<AccommodationService>()
                );

            GetService<AccommodationReviewService>().InjectServices(
                GetService<AccommodationReservationService>(),
                GetService<OwnerService>(),
                GetService<AccommodationStatisticsService>()
                );

            GetService<NotificationService>().InjectServices(
                GetService<AccommodationRescheduleRequestService>(),
                GetService<AccommodationReservationService>()
                );

            GetService<AccommodationStatisticsService>().InjectServices(
                GetService<AccommodationReservationService>(),
                GetService<AccommodationService>()
                );

            GetService<OwnerService>().InjectServices(
                GetService<AccommodationReviewService>()
                );

            GetService<TourRequestService>().InjectServices(
                GetService<LocationService>(),
                GetService<LanguageService>(),
                GetService<UserService>()
                );

            GetService<TourReservationService>().InjectServices(
                GetService<TourService>(),
                GetService<PassengerService>(),
                GetService<VoucherService>()
                );

            GetService<PassengerService>().InjectServices(
                GetService<TourReservationService>()
                );

            GetService<ComplexTourRequestService>().InjectServices(
                GetService<TourRequestService>()
                );

            GetService<ForumService>().InjectServices(
                GetService<AccommodationService>(),
                GetService<NotificationService>()
                );
        }

        public static T GetService<T>() {
            return (T) _services[typeof(T)];
        }
    }
}
