namespace Api_Restful.Presentation.Dto
{
    public class TaskDto
    {
        public int ID_PK { get; set; }
        public DateTime? Creation_Date { get; set; }
        public DateTime? Completion_Date { get; set; }
        public string Description { get; set; }
        public int? Priority { get; set; }
        public string? Subject { get; set; }
        public string Status { get; set; }
    }
}
