using System.ComponentModel.DataAnnotations;

public class Tarefa
{
    [Key]
    public int ID_PK { get; set; }
    public DateTime Data_Criacao { get; set; }
    public DateTime? Data_Conclusao { get; set; }
    public string Descricao { get; set; }
    public int Prioridade { get; set; }
    public string Assunto { get; set; }
    public string Status { get; set; }

    public ICollection<UsuarioTarefa> UsuarioTarefas { get; set; }
}