using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MoriPastaPizza.LeonBot.Controller;
using Serilog;

namespace MoriPastaPizza.LeonBot;

public class Program
{

    private static IServiceProvider _serviceProvider;

    public static async Task Main()
    {
        _serviceProvider = CreateServices();

        var client = _serviceProvider.GetRequiredService<DiscordSocketClient>();

        var token = await File.ReadAllTextAsync("./bot_token.txt");

        await client.LoginAsync(TokenType.Bot, token);
        await client.StartAsync();

        client.Ready += ClientOnReady;

        await Task.Delay(-1);
    }

    private static async Task ClientOnReady()
    {
        var logger = _serviceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogInformation("Bot ready!");

        var commandHandler = _serviceProvider.GetRequiredService<CommandHandler>();
        await commandHandler.StartCommandHandler();
    }

    private static IServiceProvider CreateServices()
    {

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
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
            .AddSingleton<CommandService>();

        return collection.BuildServiceProvider();
    }
}