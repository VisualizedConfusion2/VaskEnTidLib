namespace VaskEnTidLib.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string ApartmentNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int DepartmentID { get; set; }
        public int UserTypeID { get; set; }
    }
}
