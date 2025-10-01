using VaskEnTidLib.Models;

namespace VaskEnTidLib.Services
{
    public interface IBookingService
    {
        Booking Book(Booking booking);
        IEnumerable<Booking> GetBookings();
        IEnumerable<Booking> GetBookingsByUser(int userId);
        IEnumerable<Booking> GetBookingsByMachine(int machineId);
        bool CancelBooking(int bookingId);
        Booking? GetBookingById(int bookingId);
    }
}
