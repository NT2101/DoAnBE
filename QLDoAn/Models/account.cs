using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDoAn.Models
{
    [Table("tblAccount")]
    public class account
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Password { get; set; }       
        public int RoleID { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        [MaxLength(50)]
        public string CreatedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        [MaxLength(50)]
        public string ModifiedUser { get; set; }
       
    }
}
