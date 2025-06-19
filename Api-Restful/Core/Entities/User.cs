using System.ComponentModel.DataAnnotations;

namespace Api_Restful.Core.Entities;
public class User
{
    [Key]
    public int ID_PK { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public int ID_JobTitle { get; set; }
    public int? ID_Token { get; set; }

    public JobTitles JobTitle { get; set; }
    public ICollection<TaskUser> TaskUsers { get; set; }
    public ICollection<Token> Tokens { get; set; }
}