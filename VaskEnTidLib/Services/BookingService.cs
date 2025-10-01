using System;
using System.Collections.Generic;
using VaskEnTidLib.Models;

namespace VaskEnTidLib.Services
{

    public class BookingService : IBookingService
    {
        public Booking Book(Booking booking)
        {
            throw new NotImplementedException();
        }

        public bool CancelBooking(int bookingId)
        {
            throw new NotImplementedException();
        }

        public Booking? GetBookingById(int bookingId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Booking> GetBookings()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Booking> GetBookingsByMachine(int machineId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Booking> GetBookingsByUser(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
