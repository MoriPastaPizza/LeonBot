using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Microsoft.Extensions.Logging;

namespace MoriPastaPizza.LeonBot.Modules
{
    public class VideoCommands : ModuleBase<SocketCommandContext>
    {
        private const string VideoBasePath = "./data/videos/";


        [Command("ossi")]
        [Alias("pfui", "ostne", "osten", "dresden", "dynamo")]
        public async Task SendOssi()
        {
            await Context.Channel.SendFileAsync(VideoBasePath + "ossi.mp4");
        }
    }
}
