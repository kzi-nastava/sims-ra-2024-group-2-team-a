using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BookingApp.Repository {
    public class TourReservationRepository : Repository<TourReservation> {

        public void MakeReservation(int userId, TourDTO tourDTO, List<PassengerDTO> passengerDTOs) {
            PassengerRepository passengerRepository = new PassengerRepository();
            UserRepository userRepository = new UserRepository();
            TourRepository tourRepository = new TourRepository();

            Save(new TourReservation(userId, tourDTO.Id));
            foreach(var passenger in passengerDTOs)
                passengerRepository.Save(new Passenger(tourDTO.Id, passenger.Name, passenger.Surname, passenger.Age, userId));
        }
    }  
}