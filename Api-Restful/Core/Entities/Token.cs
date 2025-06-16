using System.ComponentModel.DataAnnotations;

public class Token
{
    [Key]
    public int ID_PK { get; set; }
    public DateTime Creation_Date { get; set; }
    public int ID_User { get; set; }

    public User User { get; set; }
}