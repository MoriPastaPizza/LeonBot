using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MoriPastaPizza.LeonBot.Controller;
using MoriPastaPizza.LeonBot.Modules;
using Serilog;

namespace MoriPastaPizza.LeonBot;

public class Program
{

    private static IServiceProvider _serviceProvider;
    private static ILogger<Program> _logger;

    public static async Task Main()
    {
        _serviceProvider = CreateServices();

        var client = _serviceProvider.GetRequiredService<DiscordSocketClient>();
        var commands = _serviceProvider.GetRequiredService<CommandService>();
        _logger = _serviceProvider.GetRequiredService<ILogger<Program>>();
        var mediaGroupController = _serviceProvider.GetRequiredService<MediaGroupController>();

        mediaGroupController.StartMediaGroupController();

        client.Ready += ClientOnReady;
        client.Log += OnLog;
        commands.Log += OnLog;

        var token = await File.ReadAllTextAsync("./bot_token.txt");
        await client.LoginAsync(TokenType.Bot, token);

        await client.StartAsync();

        await Task.Delay(-1);
    }

    private static Task OnLog(LogMessage arg)
    {
        switch (arg.Severity)
        {
            case LogSeverity.Critical:
                _logger.LogCritical(arg.ToString());
                break;
            case LogSeverity.Error:
                _logger.LogError(arg.ToString());
                break;
            case LogSeverity.Warning:
                _logger.LogWarning(arg.ToString());
                break;
            case LogSeverity.Info:
                _logger.LogInformation(arg.ToString());
                break;
            case LogSeverity.Verbose:
                _logger.LogTrace(arg.ToString());
                break;
            case LogSeverity.Debug:
                _logger.LogDebug(arg.ToString());
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return Task.CompletedTask;
    }

    private static async Task ClientOnReady()
    {
        var commandHandler = _serviceProvider.GetRequiredService<CommandHandler>();
        await commandHandler.StartCommandHandler();
    }

    private static IServiceProvider CreateServices()
    {

        Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Verbose()
#else
            .MinimumLevel.Information()
#endif

            .WriteTo.Console()
            .CreateLogger();

        var config = new DiscordSocketConfig()
        {
            GatewayIntents = GatewayIntents.Guilds |
                                              GatewayIntents.GuildMembers |
                                              GatewayIntents.GuildMessageReactions |
                                              GatewayIntents.GuildMessages |
                                              GatewayIntents.GuildVoiceStates |
                                              GatewayIntents.MessageContent
        };

        var collection = new ServiceCollection()
            .AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSerilog(Log.Logger);
            })
            .AddSingleton(config)
            .AddSingleton<DiscordSocketClient>()
            .AddSingleton<CommandHandler>()
            .AddSingleton<CommandService>()
            .AddSingleton<MediaGroupController>();

        return collection.BuildServiceProvider();
    }
}