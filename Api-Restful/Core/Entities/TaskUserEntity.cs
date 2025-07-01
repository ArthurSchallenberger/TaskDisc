using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskDisc.Core.Entities;
public class TaskUserEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int Id_User { get; set; }
    public int Id_Task { get; set; }

    public UserEntity User { get; set; }
    public TaskEntity Task { get; set; }
}