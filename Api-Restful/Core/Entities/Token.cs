using System.ComponentModel.DataAnnotations;

namespace Api_Restful.Core.Entities;
public class Token
{
    [Key]
    public int ID_PK { get; set; }
    public DateTime Creation_Date { get; set; }
    public int ID_User { get; set; }

    public User User { get; set; }
}