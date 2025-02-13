using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using MoriPastaPizza.LeonBot.Global;
using MoriPastaPizza.LeonBot.Interfaces;
using MoriPastaPizza.LeonBot.Models;

namespace MoriPastaPizza.LeonBot.Modules
{
    internal class BirthdayCommands
    {
        private readonly DiscordSocketClient _client;
        private readonly IPersistentDataHandler _dataHandler;
        private readonly ILogger<BirthdayCommands> _logger;

        private const string BirthdayCommand = "birthday";
        private const string AddBirthdayCommand = "birthday-add";
        private const string DeleteBirthdayCommand = "birthday-delete";

        public BirthdayCommands(DiscordSocketClient client, IPersistentDataHandler dataHandler, ILogger<BirthdayCommands> logger)
        {
            _client = client;
            _dataHandler = dataHandler;
            _logger = logger;
        }

        public async Task StartModule()
        {
            _logger.LogInformation("Starting: " + nameof(BirthdayCommands));
            var addCommand = new SlashCommandBuilder()
                .WithName(AddBirthdayCommand)
                .WithDescription("Füge deinen Geburtstagsgruß von Leon hinzu!")
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("day")
                    .WithDescription("Der Tag des Monats")
                    .WithRequired(true)
                    .WithType(ApplicationCommandOptionType.Integer)
                    .WithMinValue(1)
                    .WithMaxValue(31))
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("month")
                    .WithDescription("Der Monat")
                    .WithRequired(true)
                    .WithType(ApplicationCommandOptionType.Integer)
                    .WithMinValue(1)
                    .WithMaxValue(12))
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("video-number")
                    .WithDescription("Das Party Video das gesendet werden soll")
                    .WithType(ApplicationCommandOptionType.Integer)
                    .WithMinValue(0)
                    .WithMaxValue(9));

            var deleteCommand = new SlashCommandBuilder()
                .WithName(DeleteBirthdayCommand)
                .WithDescription("Löscht deinen Geburtstagsgruß");

            var birthdayCommand = new SlashCommandBuilder()
                .WithName(BirthdayCommand)
                .WithDescription("Bekomme Info über deinen Geburtstagsgruß");


            var guild = _client.GetGuild(Constants.ServerId);
            var commands = await guild.GetApplicationCommandsAsync();
            
            foreach (var command in commands)
            {
                await command.DeleteAsync();
            }

            await guild.CreateApplicationCommandAsync(addCommand.Build());
            await guild.CreateApplicationCommandAsync(deleteCommand.Build());
            await guild.CreateApplicationCommandAsync(birthdayCommand.Build());

            _client.SlashCommandExecuted += ClientOnSlashCommandExecuted;
        }


        private async Task ClientOnSlashCommandExecuted(SocketSlashCommand command)
        {
            try
            {
                _logger.LogDebug("command received: " + command.Data.Name);

                switch (command.Data.Name)
                {
                    case BirthdayCommand:
                        await HandleBirthDayCommand(command);
                        break;
                    case AddBirthdayCommand:
                        await HandleAddBirthDayCommand(command);
                        break;
                    case DeleteBirthdayCommand:
                        await HandleDeleteBirthday(command);
                        break;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(ClientOnSlashCommandExecuted));
            }
        }

        private async Task HandleBirthDayCommand(SocketSlashCommand command)
        {

            var user = GetUserFromCommand(command);

            if (user.Birthday == null)
            {
                await command.RespondAsync($"Du hast noch keinen Geburtstag angelegt! Benutze /{AddBirthdayCommand}", ephemeral: true);
            }
            else
            {
                await command.RespondAsync(
                    $"Dein Geburtstagsgruß kommt am: {user.Birthday.Value.Day}.{user.Birthday.Value.Month} mit dem Video: {user.BirthdayVideoIndex} (0 ist ein zufälliges)", ephemeral: true);
            }
        }

        private async Task HandleAddBirthDayCommand(SocketSlashCommand command)
        {
            try
            {
                _logger.LogInformation("Adding birthday...");

                var day = 0;
                var month = 0;
                var videoIndex = 0;

                foreach (var option in command.Data.Options)
                {
                    switch (option.Name)
                    {
                        case "day":
                            day = int.Parse(option.Value.ToString() ?? "0");
                            break;
                        case "month":
                            month = int.Parse(option.Value.ToString() ?? "0");
                            break;
                        case "video-number":
                            videoIndex = int.Parse(option.Value.ToString() ?? "0");
                            break;
                    }
                }

                if (day == 0 || month == 0)
                {
                    _logger.LogWarning($"Day or month was zero! Something went wrong! command from {command.User.Username} in {command.Channel.Name}");
                    await command.RespondAsync("Etwas ist schief gelaufen, wende dich an Mori :(", ephemeral: true);
                }

                _logger.LogInformation($"Birthday is day: {day} month: {month}");

                var user = GetUserFromCommand(command);

                try
                {
                    var birthDayDate = new DateTime(1, month, day);
                    user.Birthday = birthDayDate;
                    user.BirthdayVideoIndex = videoIndex;
                    user.NickName = command.User.Username;
                }
                catch (ArgumentOutOfRangeException)
                {
                    await command.RespondAsync(
                        "Das ist kein valides Datum! Mach dich aus meiner Leitung raus du Birne!", ephemeral: true);
                    return;
                }

                _dataHandler.SaveUser(user);
                
                _logger.LogInformation($"Saved data for user {user.NickName}");

                await command.RespondAsync(
                    $"Geburtstagsgruß eingerichtet! kommt am {user.Birthday.Value.Day}.{user.Birthday.Value.Month} mit dem Video {videoIndex} (0 ist ein zufälliges!)", ephemeral: true);

            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(HandleAddBirthDayCommand));
                await command.RespondAsync("Etwas ist schief gelaufen, wende dich an Mori :(", ephemeral: true);
            }
        }

        private async Task HandleDeleteBirthday(SocketSlashCommand command)
        {
            _logger.LogInformation("Deleting birthday...");

            var user = GetUserFromCommand(command);

            if (user.Birthday == null)
            {
                _logger.LogInformation("No birthday added! Aborting!");
                await command.RespondAsync($"Du hast noch keinen Geburtstag angelegt! Benutze /{AddBirthdayCommand}", ephemeral: true);
                return;
            }

            user.Birthday = null;
            user.BirthdayVideoIndex = 0;
            user.NickName = command.User.Username;

            _dataHandler.SaveUser(user);

            _logger.LogInformation("Birthday deleted for " + user.NickName);

            await command.RespondAsync("Geburtstagsgruß wurde gelöscht! Da ist kei Loch mehr für den Zimmermann!",
                ephemeral: true);
        }

        private User GetUserFromCommand(SocketSlashCommand command)
        {
            var user = _dataHandler.GetUser(command.User.Id);

            if (user == null)
            {
                user = new User
                {
                    Id = command.User.Id,
                    NickName = command.User.Username
                };
                _dataHandler.SaveUser(user);
            }

            return user;
        }
    }
}
