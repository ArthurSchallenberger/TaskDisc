namespace Api_Restful.Presentation.Dto;

public class TaskUserDto
{
    public int Id { get; set; }
    public int ID_User { get; set; }
    public int ID_Task { get; set; }
    public UserDto User { get; set; }
    public TaskDto Task { get; set; }
}
