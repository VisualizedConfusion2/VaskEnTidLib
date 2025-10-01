namespace VaskEnTidLib.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int MachineId { get; set; }
        public int UserId { get; set; }
    }
}
