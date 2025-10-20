using System;

namespace VaskEnTidLib.Models
{
    public class ErrorReport
    {
        public int ErrorID { get; set; }
        public int MachineID { get; set; }
        public string? MachineName { get; set; }
        public int UserID { get; set; }
        public string? UserName { get; set; }
        public string? ErrorType { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
        public DateTime DateCreated { get; set; }

        public ErrorReport()
        {

        }
    }
}
