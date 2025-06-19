namespace Api_Restful.Core.Entities;
public class TaskUser
{
    public int ID_User { get; set; }
    public int ID_Task { get; set; }

    public User User { get; set; }
    public TaskEntity Task { get; set; }
}