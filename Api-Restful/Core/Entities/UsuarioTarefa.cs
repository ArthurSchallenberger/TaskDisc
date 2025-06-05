public class UsuarioTarefa
{
    public int ID_Usuario { get; set; }
    public int ID_Tarefa { get; set; }

    public Usuario Usuario { get; set; }
    public Tarefa Tarefa { get; set; }
}