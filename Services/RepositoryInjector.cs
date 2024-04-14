using BookingApp.Repository;
using BookingApp.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
            { typeof(IRescheduleRequestRepository), new Dictionary<RepositoryType, object>() },
            { typeof(IUserRepository), new Dictionary<RepositoryType, object>() },
            { typeof(IReviewRepository), new Dictionary<RepositoryType, object>() },
            { typeof(IOwnerRepository), new Dictionary<RepositoryType, object>() },
        };

        static RepositoryInjector() {
            // CSV repository registration
            _repositoryInstances[typeof(IAccommodationRepository)][RepositoryType.CSV] = new AccommodationRepository();
            _repositoryInstances[typeof(IAccommodationReservationRepository)][RepositoryType.CSV] = new AccommodationReservationRepository();
            _repositoryInstances[typeof(ILocationRepository)][RepositoryType.CSV] = new LocationRepository();
            _repositoryInstances[typeof(IRescheduleRequestRepository)][RepositoryType.CSV] = new RescheduleRequestRepository();
            _repositoryInstances[typeof(IUserRepository)][RepositoryType.CSV] = new UserRepository();
            _repositoryInstances[typeof(IReviewRepository)][RepositoryType.CSV] = new ReviewRepository();
            _repositoryInstances[typeof(IOwnerRepository)][RepositoryType.CSV] = new OwnerRepository();

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
