namespace QLDoAn.Models
{
    public class ChangePasswordDto
    {
        public int UserID { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
