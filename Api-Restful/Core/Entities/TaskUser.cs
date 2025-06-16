public class TaskUser
{
    public int ID_User { get; set; }
    public int ID_Task { get; set; }

    public User User { get; set; }
    public Task Task { get; set; }
}