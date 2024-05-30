using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using System;
using System.Collections.Generic;

namespace BookingApp.Services {

    public enum RepositoryType {
        CSV
    }

    public static class RepositoryInjector {

        // Dictionary of dictionaries of repository instances
        // Type: Interface Type
        // RepositoryType: Type of repository (CSV, SQL, TXT etc.)
        // object: Repository instance
        private static Dictionary<Type, Dictionary<RepositoryType, object>> _repositoryInstances = new Dictionary<Type, Dictionary<RepositoryType, object>> {
            { typeof(IAccommodationRepository), new Dictionary<RepositoryType, object>() },
            { typeof(IAccommodationReservationRepository), new Dictionary<RepositoryType, object>() },
            { typeof(ILocationRepository), new Dictionary<RepositoryType, object>() },
            { typeof(IAccommodationRescheduleRequestRepository), new Dictionary<RepositoryType, object>() },
            { typeof(IUserRepository), new Dictionary<RepositoryType, object>() },
            { typeof(IAccommodationReviewRepository), new Dictionary<RepositoryType, object>() },
            { typeof(IOwnerRepository), new Dictionary<RepositoryType, object>() },
            { typeof(ILanguageRepository), new Dictionary<RepositoryType, object>() },
            { typeof(IPassengerRepository), new Dictionary<RepositoryType, object>() },
            { typeof(IPointOfInterestRepository), new Dictionary<RepositoryType, object>() },
            { typeof(ITourRepository), new Dictionary<RepositoryType, object>() },
            { typeof(ITourReservationRepository), new Dictionary<RepositoryType, object>() },
            { typeof(ITourReviewRepository), new Dictionary<RepositoryType, object>() },
            { typeof(IVoucherRepository), new Dictionary<RepositoryType, object>() },
            { typeof(INotificationRepository), new Dictionary<RepositoryType, object>() },
            { typeof(ITourRequestRepository), new Dictionary<RepositoryType, object>() },
            { typeof(IAccommodationRenovationRepository), new Dictionary<RepositoryType, object>() },
            { typeof(IAccommodationStatisticsRepository), new Dictionary<RepositoryType, object>() },
            { typeof(IGuestRepository), new Dictionary<RepositoryType, object>() },
            { typeof(IComplexTourRequestRepository), new Dictionary<RepositoryType, object>() },
            { typeof(IForumRepository), new Dictionary<RepositoryType, object>()},
            { typeof(ICommentRepository), new Dictionary<RepositoryType, object>()},
            { typeof(IVisitedTourRepository), new Dictionary<RepositoryType, object>() }
        };

        static RepositoryInjector() {
            // CSV repository registration
            _repositoryInstances[typeof(IAccommodationRepository)][RepositoryType.CSV] = new AccommodationRepository();
            _repositoryInstances[typeof(IAccommodationReservationRepository)][RepositoryType.CSV] = new AccommodationReservationRepository();
            _repositoryInstances[typeof(ILocationRepository)][RepositoryType.CSV] = new LocationRepository();
            _repositoryInstances[typeof(IAccommodationRescheduleRequestRepository)][RepositoryType.CSV] = new AccommodationRescheduleRequestRepository();
            _repositoryInstances[typeof(IUserRepository)][RepositoryType.CSV] = new UserRepository();
            _repositoryInstances[typeof(IAccommodationReviewRepository)][RepositoryType.CSV] = new AccommodationReviewRepository();
            _repositoryInstances[typeof(IOwnerRepository)][RepositoryType.CSV] = new OwnerRepository();
            _repositoryInstances[typeof(ILanguageRepository)][RepositoryType.CSV] = new LanguageRepository();
            _repositoryInstances[typeof(IPassengerRepository)][RepositoryType.CSV] = new PassengerRepository();
            _repositoryInstances[typeof(IPointOfInterestRepository)][RepositoryType.CSV] = new PointOfInterestRepository();
            _repositoryInstances[typeof(ITourRepository)][RepositoryType.CSV] = new TourRepository();
            _repositoryInstances[typeof(ITourReservationRepository)][RepositoryType.CSV] = new TourReservationRepository();
            _repositoryInstances[typeof(ITourReviewRepository)][RepositoryType.CSV] = new TourReviewRepository();
            _repositoryInstances[typeof(IVoucherRepository)][RepositoryType.CSV] = new VoucherRepository();
            _repositoryInstances[typeof(INotificationRepository)][RepositoryType.CSV] = new NotificationRepository();
            _repositoryInstances[typeof(ITourRequestRepository)][RepositoryType.CSV] = new TourRequestRepository();
            _repositoryInstances[typeof(IAccommodationRenovationRepository)][RepositoryType.CSV] = new AccommodationRenovationRepository();
            _repositoryInstances[typeof(IAccommodationStatisticsRepository)][RepositoryType.CSV] = new AccommodationStatisticsRepository();
            _repositoryInstances[typeof(IGuestRepository)][RepositoryType.CSV] = new GuestRepository();
            _repositoryInstances[typeof(IComplexTourRequestRepository)][RepositoryType.CSV] = new ComplexTourRequestRepository();
            _repositoryInstances[typeof(IForumRepository)][RepositoryType.CSV] = new ForumRepository();
            _repositoryInstances[typeof(ICommentRepository)][RepositoryType.CSV] = new CommentRepository();
            _repositoryInstances[typeof(IVisitedTourRepository)][RepositoryType.CSV] = new VisitedTourRepository();

            // SQL repository registration
            // ...
        }

        // By default returns CSV repository
        public static TInterface GetInstance<TInterface>(RepositoryType repoType = RepositoryType.CSV) where TInterface : class {

            if(!_repositoryInstances.TryGetValue(typeof(TInterface), out var repositories))
                throw new Exception($"No repository implementations found for {typeof(TInterface)} interface");

            if(!repositories.TryGetValue(repoType, out var repository))
                throw new Exception($"No repository implementation found for {repoType} repository type");

            return (TInterface) repository;
        }
    }
}
