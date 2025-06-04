using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

public class DiscordBotService : IHostedService
{
    private readonly DiscordSocketClient _client;
    private readonly CommandService _commands;
    private readonly IServiceProvider _services;
    private readonly string _token;

    public DiscordBotService(IServiceProvider services, IConfiguration configuration)
    {
        _client = new DiscordSocketClient(new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
        });
        _commands = new CommandService();
        _services = services;
        _token = configuration["Discord:Token"];
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _client.Log += LogAsync;
        _client.Ready += ReadyAsync;
        _client.MessageReceived += HandleCommandAsync;

        // Carrega todos os comandos disponíveis
        var modules = await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        Console.WriteLine($"Módulos carregados: {modules.Count()}");
        foreach (var module in modules)
        {
            Console.WriteLine($"Módulo: {module.Name}");
        }

        await _client.LoginAsync(TokenType.Bot, _token);
        await _client.StartAsync();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _client.StopAsync();
    }

    private Task LogAsync(LogMessage log)
    {
        Console.WriteLine(log.ToString());
        return Task.CompletedTask;
    }

    private Task ReadyAsync()
    {
        Console.WriteLine($"{_client.CurrentUser} está conectado!");
        return Task.CompletedTask;
    }

    private async Task HandleCommandAsync(SocketMessage messageParam)
    {
        var message = messageParam as SocketUserMessage;
        if (message == null)
        {
            Console.WriteLine("Mensagem recebida não é SocketUserMessage.");
            return;
        }

        int argPos = 0;
        bool isCommand = message.HasCharPrefix('/', ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos);
        Console.WriteLine($"Mensagem recebida: {message.Content}, É comando? {isCommand}");

        if (!isCommand) return;

        var context = new SocketCommandContext(_client, message);
        var result = await _commands.ExecuteAsync(context, argPos, _services);
        if (!result.IsSuccess)
        {
            Console.WriteLine($"Erro ao executar comando: {result.ErrorReason}");
        }
        else
        {
            Console.WriteLine("Comando executado com sucesso.");
        }
    }
}