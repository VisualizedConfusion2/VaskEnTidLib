using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using VaskEnTidLib.Models;
using VaskEnTidLib.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VaskEnTidLib.Services
{
    public class BookingService
    {
        private readonly BookingRepo _repository;

        public BookingService(BookingRepo repository)
        {
            _repository = repository;
        }

        public List<Booking> GetBookingsByUserId(int userId)
        {
            return _repository.GetBookingsByUserId(userId);
        }
    }
}
