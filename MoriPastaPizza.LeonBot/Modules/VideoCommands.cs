using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
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
        [Alias("pfui", "ostne", "osten", "dresden", "dynamo", "ronny")]
        [MediaGroup("spucken")]
        public async Task SendOssi()
        {
            await Context.Channel.SendFileAsync(Constants.MediaBasePath + "ossi.mp4", "<:Dynamo:1017680244699828287>");
        }

        [Command("party")]
        public async Task SendParty([Remainder] int index = 0)
        {
            await SendMedia("party", index);
        }

        [Command("adidas")]
        public async Task SendAdidas()
        {
            await SendMedia("adidas.mp4");
        }

        [Command("akrobat")]
        public async Task SendAkrobat([Remainder] int index = 0)
        {
            await SendMedia("akrobat", index);
        }

        [Command("alkohol")]
        [Alias("opa", "großvater", "konsum")]
        public async Task SendAlkohol()
        {
            await SendMedia("alkohol.mp4");
        }

        [Command("apfelschorle")]
        [Alias("schorle", "apfel", "lecker")]
        [MediaGroup("spucken")]
        public async Task SendApfelSchorle()
        {
            await SendMedia("apfelschorle.mp4");
        }

        [Command("auflauern")]
        [Alias("erlangen")]
        [MediaGroup("haider"), MediaGroup("hater")]
        public async Task SendAuflauern()
        {
            await SendMedia("auflauern.mp4");
        }

        [Command("bellen")]
        [Alias("beißen", "beiß")]
        public async Task SendBellen()
        {
            await SendMedia("bellen.mp4");
        }

        [Command("bier")]
        public async Task SendBier([Remainder] int index = 0)
        {
            await SendMedia("bier", index);
        }

        [Command("blauesauge")]
        [Alias("blau", "blaues", "auge", "schlägerei")]
        public async Task SendBlauesAuge()
        {
            await SendMedia("blauesauge.mp4");
        }

        [Command("corona")]
        [Alias("gorona", "gesund", "infektion")]
        public async Task SendCorona()
        {
            await SendMedia("corona.mp4");
        }

        [Command("datenschutz")]
        [Alias("datengeheimniss", "daten", "datenschutzverletzung")]
        public async Task SendDatenschutz()
        {
            await SendMedia("datenschutz.mp4");
        }

        [Command("dreckssack")]
        [MediaGroup("haider"), MediaGroup("hater")]
        public async Task SendDreckssack()
        {
            await SendMedia("dreckssack.mp4");
        }

        [Command("dreckssau")]
        [Alias("stinkekaktus", "kaktus", "frank", "treiber", "frank treiber")]
        [MediaGroup("haider"), MediaGroup("hater")]
        public async Task SendDreckssau()
        {
            await SendMedia("dreckssau.mp4");
        }

        [Command("ehre")]
        [Alias("hobe die ehre", "hobe", "habe die ehre")]
        public async Task SendEhre()
        {
            await SendMedia("ehre.mp4");
        }

        [Command("fanclub")]
        [Alias("mauthausen")]
        [MediaGroup("haider"), MediaGroup("hater")]
        public async Task SendFanclub()
        {
            await SendMedia("fanclub.mp4");
        }

        [Command("flucht")]
        [Alias("gnade euch gott")]
        public async Task SendFlucht()
        {
            await SendMedia("flucht.mp4");
        }

        [Command("gendermarie")]
        [Alias("gender", "marie", "gendarmerie", "polizei")]
        public async Task SendGender()
        {
            await SendMedia("gendermarie.mp4");
        }

        [Command("spucken")]
        public async Task Spucken([Remainder] int index = 0)
        {
            var methods = _mediaGroupController.GetMethodsForGroup("spucken");
            ;
        }


        private async Task SendMedia(string name)
        {
            await Context.Channel.SendFileAsync(Constants.MediaBasePath + name);
        }

        private async Task SendMedia(string basePath, int index)
        {
            if (index == 0)
            {
                await Context.Channel.SendFileAsync(GetRandomMedia(basePath));
            }
            else
            {
                try
                {
                    await Context.Channel.SendFileAsync(GetAllMedia(basePath)[index - 1]);
                }
                catch (ArgumentOutOfRangeException)
                {
                    await Context.Message.ReplyAsync(
                        "Des Video gibt's net du Spinner! Da hat der Zimmermann kei Loch gelassen!");
                }
            }
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
