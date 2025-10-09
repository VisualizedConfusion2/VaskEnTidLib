using System;

namespace VaskEnTidLib.Models
{
    public class ErrorReport
    {
        public int ErrorID { get; set; }
        public int MachineID { get; set; }
        public int UserID { get; set; }
        public int ErrorTypeID { get; set; }
        public int StatusID { get; set; }
        public string? Description { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
