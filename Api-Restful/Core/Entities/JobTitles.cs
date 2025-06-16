using System.ComponentModel.DataAnnotations;

public class JobTitles
{
    [Key]
    public int ID_PK { get; set; }
    public string Name { get; set; }
    public string Abbreviation { get; set; }

    public ICollection<User> User { get; set; }
}