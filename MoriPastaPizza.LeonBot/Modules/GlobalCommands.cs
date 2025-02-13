using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using MoriPastaPizza.LeonBot.Global;

namespace MoriPastaPizza.LeonBot.Modules
{
    public class GlobalCommands : ModuleBase<SocketCommandContext>
    {
        [Command("commands")]
        [Alias("help", "hilfe", "kommandos", "kommando")]
        public async Task Commands()
        {
            var builder = new ComponentBuilder()
                .WithButton("Medien", "btn-media")
                .WithButton("Geburtstags Gruß", "btn-birthday");

            await ReplyAsync("Worüber möchtest du mehr erfahren?", components: builder.Build());
        }
    }
}
