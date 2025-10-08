namespace VaskEnTidLib.Models
{
    public class Machine
    {
        public int MachineID { get; set; }
        public string MachineType { get; set; }
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public Machine()
        {

        }
        public Machine(int machineID, string machineType, int departmentID, string name)
        {
            MachineID = machineID;
            MachineType = machineType;
            DepartmentID = departmentID;
            Name = name;
        }

    }
}
