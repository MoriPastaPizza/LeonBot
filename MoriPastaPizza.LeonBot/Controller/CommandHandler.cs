using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MoriPastaPizza.LeonBot.Controller
{
    internal class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commService;
        private readonly ILogger<CommandHandler> _logger;

        public CommandHandler(DiscordSocketClient client, CommandService commService, ILogger<CommandHandler> logger)
        {
            _client = client;
            _commService = commService;
            _logger = logger;
        }

        public async Task StartCommandHandler()
        {
            _client.MessageReceived += ClientOnMessageReceived;

            await _commService.AddModulesAsync(assembly: Assembly.GetEntryAssembly(),
                services: null);
        }

        private async Task ClientOnMessageReceived(SocketMessage messageParam)
        {

            var message = messageParam as SocketUserMessage;
            if (message == null) return;

            int argPos = 0;

            if(!message.HasStringPrefix("un ", ref argPos, StringComparison.OrdinalIgnoreCase) || message.Author.IsBot)
                return;

            _logger.LogDebug($"Command received! {message.Content}");

            var context = new SocketCommandContext(_client, message);

            await _commService.ExecuteAsync(context: context, argPos: argPos, services: null);
        }
    }
}
