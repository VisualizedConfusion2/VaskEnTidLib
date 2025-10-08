using System;
using System.Collections.Generic;
using VaskEnTidLib.Models;
using VaskEnTidLib.Repositories;

namespace VaskEnTidLib.Services
{
    public class BookingService
    {
        private readonly BookingRepo _repository;

        public BookingService(BookingRepo repository)
        {
            _repository = repository;
        }

        // Hent bookinger for en bestemt bruger
        public List<Booking> GetBookingsByUserId(int userId)
        {
            return _repository.GetBookingsByUserId(userId);
        }

        // Opret en ny booking
        public bool CreateBooking(int userId, int machineId, DateOnly date, TimeOnly startTime, TimeOnly endTime)
        {
            try
            {
                return _repository.CreateBooking(userId, machineId, date, startTime, endTime);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl i BookingService.CreateBooking: {ex.Message}");
                return false;
            }
        }
    }
}