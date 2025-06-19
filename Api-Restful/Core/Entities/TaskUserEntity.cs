namespace Api_Restful.Core.Entities;
public class TaskUserEntity
{
    public int Id_User { get; set; }
    public int Id_Task { get; set; }

    public UserEntity User { get; set; }
    public TaskEntity Task { get; set; }
}