namespace VaskEnTidLib.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public int PostalCode { get; set; }
        public string? City { get; set; } = string.Empty;
        public string? RoadNR { get; set; } = string.Empty;
        public int HouseNR { get; set; }
    }
}
