using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

public class Usuario
{
    [Key]
    public int ID_PK { get; set; }
    public string Nome { get; set; }
    public string Senha { get; set; }
    public string Email { get; set; }
    public int ID_Cargo { get; set; }
    public int? ID_Tokens { get; set; }

    public Cargo Cargo { get; set; }
    public ICollection<UsuarioTarefa> UsuarioTarefas { get; set; }
    public ICollection<Token> Tokens { get; set; }
}