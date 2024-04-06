using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDoAn.Models
{
    [Table("tblTopicType")]
    public class topicType
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
    }
}
