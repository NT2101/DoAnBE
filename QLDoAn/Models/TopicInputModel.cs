using System.ComponentModel.DataAnnotations;

public class TopicInputModel
{
    [Required]
    [MaxLength(250)]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public int TopicTypeID { get; set; }

    [Required]
    public string StudentID { get; set; }

    [Required]
    public int TeacherID { get; set; }

    [Required]
    public int TopicYear { get; set; }
}
