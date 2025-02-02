using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Microsoft.Extensions.Logging;
using MoriPastaPizza.LeonBot.Attributes;
using MoriPastaPizza.LeonBot.Controller;
using MoriPastaPizza.LeonBot.Global;

namespace MoriPastaPizza.LeonBot.Modules
{
    public class VideoCommands : ModuleBase<SocketCommandContext>
    {
        private readonly MediaGroupController _mediaGroupController;

        public VideoCommands(MediaGroupController mediaGroupController)
        {
            _mediaGroupController = mediaGroupController;
        }

        [Command("ossi")]
        [Alias("pfui", "ostne", "osten", "dresden", "dynamo")]
        [MediaGroup("spucken")]
        public async Task SendOssi()
        {
            await Context.Channel.SendFileAsync(Constants.MediaBasePath + "ossi.mp4", "<:Dynamo:1017680244699828287>");
        }

        [Command("party")]
        public async Task SendParty([Remainder] int index = 0)
        {
            if (index == 0)
            {
                await Context.Channel.SendFileAsync(GetRandomMedia("party"));
            }
            else
            {
                await Context.Channel.SendFileAsync(GetAllMedia("party")[index - 1]);
            }
        }


        [Command("spucken")]
        public async Task Spucken([Remainder] int index = 0)
        {
            var methods = _mediaGroupController.GetMethodsForGroup("spucken");
            ;
        }

        private static string GetRandomMedia(string baseName)
        {
            var videos = GetAllMedia(baseName);
            return videos[Random.Shared.Next(0, videos.Count)];
        }

        private static List<string> GetAllMedia(string baseName)
        {
            return Directory.EnumerateFiles(Constants.MediaBasePath, baseName + "*").ToList();
        }
    }
}
