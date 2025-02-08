using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using MoriPastaPizza.LeonBot.Interfaces;
using MoriPastaPizza.LeonBot.Models;
using MoriPastaPizza.LeonBot.Modules;
using Timer = System.Timers.Timer;

namespace MoriPastaPizza.LeonBot.Controller
{
    internal class BirthdayController
    {
        private readonly ILogger<BirthdayController> _logger;
        private readonly IPersistentDataHandler _persistentDataHandler;
        private readonly DiscordSocketClient _client;
        private readonly Timer _timer;

        private const ulong BirthdayChannelId = 1334797885044293725;

        public BirthdayController(ILogger<BirthdayController> logger, IPersistentDataHandler persistentDataHandler, DiscordSocketClient client)
        {
            _logger = logger;
            _persistentDataHandler = persistentDataHandler;
            _client = client;
            _timer = new Timer();
        }

        public void StartBirthdayController()
        {
            _logger.LogInformation("Starting: " + nameof(BirthdayController));
            _timer.Interval = GetNextDifference().TotalMilliseconds;
            _timer.Start();
            _timer.Elapsed += TimerElapsed;

        }

        private async Task CheckBirthdays()
        {
            _logger.LogInformation("Checking birthdays...");
            var users = _persistentDataHandler.GetAllUsers();

            if (users == null)
            {
                _logger.LogWarning("Users is null returning!");
                return;
            }

            var currentDate = DateTime.Now;

            foreach (var user in users)
            {
                if (user.Birthday == null) continue;
                if (user.Birthday.Value.Day == currentDate.Day && user.Birthday.Value.Month == currentDate.Month)
                {
                    await SendBirthdayGreet(user);
                }

            }
        }

        private async Task SendBirthdayGreet(User user)
        {
            try
            {
                if (_client.GetChannel(BirthdayChannelId) is not IMessageChannel channel)
                {
                    _logger.LogWarning("Birthday channel not found");
                    return;
                }

                var video = user.BirthdayVideoIndex == 0 ? MediaCommands.GetRandomMedia("party") : MediaCommands.GetAllMedia("party")[user.BirthdayVideoIndex - 1];

                var userMention = MentionUtils.MentionUser(user.Id);
                await channel.SendFileAsync(video,
                    $"@everyone!!! {userMention} hat Geburtstag! Lasst uns feiern!!!!!\ud83c\udf89\ud83c\udf89\ud83c\udf7b\ud83c\udf7b\ud83c\udf7b");
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(SendBirthdayGreet));
            }
        }

        private async void TimerElapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            _logger.LogInformation("Birthday timer elapsed!");
            _timer.Interval = GetNextDifference().TotalMilliseconds;
            await CheckBirthdays();
        }

        private static TimeSpan GetNextDifference()
        {
            var nextDay = DateTime.Today.AddDays(1).AddHours(8);
            return nextDay - DateTime.Now;
        }
    }
}
