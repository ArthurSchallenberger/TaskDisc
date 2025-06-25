using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Restful.Core.Entities;
public class TokenEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Token { get; set; }
    public int ID_User { get; set; }
    public DateTime Creation_Date { get; set; }
    public DateTime Expiration_Date { get; set; }
    public UserEntity User { get; set; }
}