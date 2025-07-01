using TaskDisc.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Index(nameof(Token), IsUnique = true)]
public class TokenEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Token { get; set; }

    [ForeignKey(nameof(User))]
    public int ID_User { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime Creation_Date { get; set; }
    public DateTime Expiration_Date { get; set; }
    public DateTime? LastUsed { get; set; }

    public UserEntity User { get; set; }
}