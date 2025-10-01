namespace VaskEnTidLib.Models
{
    public class Machine
    {
        public int MachineId { get; set; }
        public string? Name { get; set; } = string.Empty;
        public int MachineTypeId { get; set; }
        public int DepartmentID { get; set; }
    }
}
