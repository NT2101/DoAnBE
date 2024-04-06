using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDoAn.Models
{
    [Table("tblTopic")]
    public class Topic
    {
        [Key]
        public int TopicID { get; set; }
        [MaxLength(14)]
        [ForeignKey("tblStudent")]
        public string StudentID { get; set; }
        [ForeignKey("tblTeacher")]
        public int TeacherID { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }

        [ForeignKey("tblTopicType")]
        public int TopicTypeID { get; set; }
        [MaxLength]
        public string Description { get; set; }
        public int TopicYear { get; set; }

        public topicType topicType { get; set; }
        public student student { get; set; }
        public teacher teacher { get; set; }
    }
}
