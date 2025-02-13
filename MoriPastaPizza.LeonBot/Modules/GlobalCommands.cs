using Discord;
using Discord.Commands;

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

        [RequireOwner]
        [Command("status")]
        public async Task SetStatus([Remainder] string status)
        {
            await Context.Client.SetCustomStatusAsync(status);
        }
    }
}
