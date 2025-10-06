using System;
using System.Collections.Generic;
using System.Linq;
using VaskEnTidLib.Models;

namespace VaskEnTidLib.Services
{
    public class BookingService : IBookingService
    {
        private readonly List<Booking> _bookings = new();

        public IReadOnlyList<Booking> GetBookingsByUser(int userId)
        {
            return _bookings
                .Where(b => b.UserId == userId)
                .OrderBy(b => b.Date)
                .ThenBy(b => b.StartTime)
                .ToList();
        }

        public IReadOnlyList<Booking> GetBookingsByMachine(int machineId)
        {
            return _bookings
                .Where(b => b.MachineId == machineId)
                .OrderBy(b => b.Date)
                .ThenBy(b => b.StartTime)
                .ToList();
        }

        public Booking? GetBookingById(int bookingId)
        {
            return _bookings.FirstOrDefault(b => b.BookingID == bookingId);
        }

        public bool CancelBooking(int bookingId)
        {
            var booking = _bookings.FirstOrDefault(b => b.BookingID == bookingId);
            if (booking == null)
                return false;

            _bookings.Remove(booking);
            return true;
        }

        public Booking? CreateBooking(Booking booking)
        {
            if (booking == null)
                throw new ArgumentNullException(nameof(booking));

            // Check for overlapping bookings on the same machine & date
            bool hasConflict = _bookings.Any(b =>
                b.MachineId == booking.MachineId &&
                b.Date == booking.Date &&
                ((booking.StartTime >= b.StartTime && booking.StartTime < b.EndTime) ||
                 (booking.EndTime > b.StartTime && booking.EndTime <= b.EndTime) ||
                 (booking.StartTime <= b.StartTime && booking.EndTime >= b.EndTime)));

            if (hasConflict)
                return null; // Conflict found — booking not created

            booking.BookingID = _bookings.Any() ? _bookings.Max(b => b.BookingID) + 1 : 1;
            _bookings.Add(booking);
            return booking;
        }

        public bool UpdateBooking(Booking updatedBooking)
        {
            var existing = _bookings.FirstOrDefault(b => b.BookingID == updatedBooking.BookingID);
            if (existing == null)
                return false;

            // Check for overlap excluding the booking being updated
            bool hasConflict = _bookings.Any(b =>
                b.MachineId == updatedBooking.MachineId &&
                b.Date == updatedBooking.Date &&
                b.BookingID != updatedBooking.BookingID &&
                ((updatedBooking.StartTime >= b.StartTime && updatedBooking.StartTime < b.EndTime) ||
                 (updatedBooking.EndTime > b.StartTime && updatedBooking.EndTime <= b.EndTime) ||
                 (updatedBooking.StartTime <= b.StartTime && updatedBooking.EndTime >= b.EndTime)));

            if (hasConflict)
                return false; // Conflict found — cannot update

            existing.Date = updatedBooking.Date;
            existing.StartTime = updatedBooking.StartTime;
            existing.EndTime = updatedBooking.EndTime;
            existing.MachineId = updatedBooking.MachineId;
            existing.UserId = updatedBooking.UserId;

            return true;
        }
    }
}
