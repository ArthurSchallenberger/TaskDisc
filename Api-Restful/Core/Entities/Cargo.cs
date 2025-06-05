using System.ComponentModel.DataAnnotations;

public class Cargo
{
    [Key]
    public int ID_PK { get; set; }
    public string Nome { get; set; }
    public string Abreviacao { get; set; }

    public ICollection<Usuario> Usuarios { get; set; }
}