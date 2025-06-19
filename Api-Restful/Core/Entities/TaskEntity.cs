using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Restful.Core.Entities;

public class TaskEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DateTime Creation_Date { get; set; }
    public DateTime? Completion_Date { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
    public string Subject { get; set; }
    public string Status { get; set; }

    public ICollection<TaskUserEntity> TaskUsers { get; set; }
}