using System.Collections.Generic;
using VaskEnTidLib.Models;

namespace VaskEnTidLib.Services
{
    public interface IBookingService
    {
        IReadOnlyList<Booking> GetBookingsByUser(int userId);
        IReadOnlyList<Booking> GetBookingsByMachine(int machineId);
        Booking? GetBookingById(int bookingId);
        bool CancelBooking(int bookingId);
        Booking CreateBooking(Booking booking);
        bool UpdateBooking(Booking updatedBooking);
    }
}
