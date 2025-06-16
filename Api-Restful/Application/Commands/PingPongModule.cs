using Discord.Commands;
using System.Threading.Tasks;

public class PingPong : ModuleBase<SocketCommandContext>
{
    [Command("ping")]
    public async System.Threading.Tasks.Task PingAsync()
    {
        Console.WriteLine("Comando ping executado!");
        await ReplyAsync("Pong!");
        
    }
    [Command("pong")]
    public async System.Threading.Tasks.Task PongAsync()
    {
        Console.WriteLine("Comando pong executado!");
        await ReplyAsync("Ping!");
    }
    [Command("inter")]
    public async System.Threading.Tasks.Task InterAsync()
    {
        Console.WriteLine("Comando inter executado!");
        await ReplyAsync("Inter > Gremio!");
    }
}