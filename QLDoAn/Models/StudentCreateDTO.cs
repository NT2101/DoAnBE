namespace QLDoAn.Models
{
    public class StudentCreateDTO
    {
        public string StudentID { get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public int Sex { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Country { get; set; }
        public string ImageUrl { get; set; }
    }
}
