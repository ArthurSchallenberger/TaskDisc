using System.ComponentModel.DataAnnotations;

public class Token
{
    [Key]
    public int ID_PK { get; set; }
    public DateTime Data_Criacao { get; set; }
    public int ID_Usuario { get; set; }

    public Usuario Usuario { get; set; }
}