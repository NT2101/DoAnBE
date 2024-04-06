using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDoAn.Models
{
    [Table("tblTeacher")]
    public class teacher
    {
        [Key]
        public int TeacherID { get; set; }
        [ForeignKey("tblAccount")]
        public int AccountID { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public int Sex { get; set; }
        [MaxLength(11)]
        public string PhoneNumber { get; set; }
        [MaxLength(350)]
        public string EmailAddress { get; set; }
        [MaxLength]
        public string Description { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }

        [MaxLength(50)]
        public string CreatedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        [MaxLength(50)]
        public string ModifiedUser { get; set; }

        public account account { get; set; }
    }
}
