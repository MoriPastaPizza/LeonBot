using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;

namespace MoriPastaPizza.LeonBot.Controller
{
    internal class GlobalCommandsController
    {
        private readonly ILogger<GlobalCommandsController> _logger;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commService;

        public GlobalCommandsController(ILogger<GlobalCommandsController> logger, DiscordSocketClient client, CommandService commService)
        {
            _logger = logger;
            _client = client;
            _commService = commService;
        }

        public void StartModule()
        {
            _logger.LogInformation("Starting: " + nameof(GlobalCommandsController));
            _client.ButtonExecuted += ClientOnButtonExecuted;
        }

        private async Task ClientOnButtonExecuted(SocketMessageComponent component)
        {
            _logger.LogDebug("Button pressed: " + component.Data.CustomId);

            switch (component.Data.CustomId)
            {
                case "btn-media":
                    await MediaHelp(component);
                    break;
                case "btn-birthday":
                    await BirthdayHelp(component);
                    break;
            }
        }

        private async Task MediaHelp(SocketMessageComponent component)
        {
            var mediaModule = _commService.Modules.FirstOrDefault(m => m.Name == "MediaCommands");
            var response = string.Empty;

            if (mediaModule == null)
                return;

            foreach (var mediaModuleCommand in mediaModule.Commands)
            {
                response += mediaModuleCommand.Name;

                if (mediaModuleCommand.Aliases.Count > 0)
                {
                    response += " (";

                    response = mediaModuleCommand.Aliases.Aggregate(response, (current, alias) => current + $"{alias}, ");

                    response = response.Remove(response.Length - 2, 2);

                    response += ")";
                }

                response += Environment.NewLine;
            }

            var embed = new EmbedBuilder()
                .WithTimestamp(DateTimeOffset.Now)
                .WithColor(Color.DarkGreen)
                .WithTitle("Medien Kommandos")
                .WithDescription(response);

            await component.RespondAsync(embed: embed.Build(), ephemeral: true);
        }

        private async Task BirthdayHelp(SocketMessageComponent component)
        {
            var response = "Mit dem Geburtstagsgruß kann Leon dir an einem Tag im Jahr einen Gruß schicken!"
                           + Environment.NewLine
                           + "Füge einen Tag mit **'/birthday-add'** hinzu!"
                           + Environment.NewLine
                           + "Mit dem optionale Parameter **'video-number'** kann du dir ein bestimmtes 'un party' Video für den Gruß aussuchen! Sonst kommt ein zufälliges."
                           + Environment.NewLine
                           + Environment.NewLine
                           + "mit **'/birthday-delete'** löscht du deinen Gruß"
                           + Environment.NewLine
                           + Environment.NewLine
                           + "mit **'/birthday'** siehst du deinen akutell konfigurierten Gruß!";

            var embed = new EmbedBuilder()
                .WithTimestamp(DateTimeOffset.Now)
                .WithColor(Color.DarkBlue)
                .WithTitle("Gebrutstagsgruß Info")
                .WithDescription(response);

            await component.RespondAsync(embed: embed.Build(), ephemeral: true);
        }
    }
}
