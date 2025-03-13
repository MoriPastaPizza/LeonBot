using Discord;
using Discord.Commands;
using MoriPastaPizza.LeonBot.Attributes;
using MoriPastaPizza.LeonBot.Controller;
using MoriPastaPizza.LeonBot.Global;
using System.Xml.Linq;

namespace MoriPastaPizza.LeonBot.Modules
{
    public class MediaCommands : ModuleBase<SocketCommandContext>
    {
        private readonly MediaGroupController _mediaGroupController;

        public MediaCommands(MediaGroupController mediaGroupController)
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

        [Command("gericht")]
        public async Task SendGericht()
        {
            await SendMedia("gericht.mp4");
        }

        [Command("gott")]
        [Alias("gläubig", "essen spielen")]
        public async Task SendGott()
        {
            await SendMedia("gott.mp4");
        }


        [Command("hanswurst")]
        [Alias("klatsche", "vogel")]
        [MediaGroup("haider"), MediaGroup("hater")]
        public async Task SendHanswurst()
        {
            await SendMedia("hanswurst.mp4");
        }

        [Command("homosexuell")]
        [Alias("schwul", "tänze", "runterholen", "pervers", "homo")]
        [MediaGroup("spucken")]
        public async Task SendHomoSexuell()
        {
            await SendMedia("homosexuell.mp4");
        }

        [Command("kamera")]
        [Alias("verbot")]
        public async Task SendKamera()
        {
            await SendMedia("kamera.mp4");
        }

        [Command("kinder")]
        [Alias("kind", "bluten")]
        public async Task SendKinder()
        {
            await SendMedia("kinder.mp4");
        }

        [Command("kinderschänder")]
        [Alias("verbrecher")]
        public async Task SendKinderschänder()
        {
            await SendMedia("kinderschänder.mp4");
        }

        [Command("lachen")]
        [Alias("lol", "lache")]
        public async Task SendLachen()
        {
            await SendMedia("lachen.mp4");
        }

        [Command("lifestyle")]
        public async Task SendLifeStyle()
        {
            await SendMedia("lifestyle.mp4");
        }

        [Command("kotzen")]
        public async Task SendKotzen()
        {
            await SendMedia("kotzen.mp4");
        }

        [Command("mett")]
        [Alias("zündeln", "zündelt", "windmühle")]
        [MediaGroup("haider"), MediaGroup("hater")]
        public async Task SendMett()
        {
            await SendMedia("mett.mp4");
        }

        [Command("mittelfinger")]
        public async Task SendMittelfinger([Remainder] int index = 0)
        {
            await SendMedia("mittelfinger", index);
        }

        [Command("oger")]
        [Alias("emskirchen", "verwirrt")]
        public async Task SendOger()
        {
            await SendMedia("oger.mp4");
        }

        [Command("nix")]
        public async Task SendNix()
        {
            await SendMedia("nix.mp4");
        }

        [Command("rebekka")]
        [Alias("wing", "rebekka wing", "rebeka", "rebecca", "24h", "klo")]
        public async Task SendRebekka()
        {
            await SendMedia("rebekka.mp4");
        }

        [Command("reisstdiehütteab")]
        [Alias("schlager", "laune", "reiss", "reisstdiehütteab")]
        public async Task ReissDieHütteAb()
        {
            await SendMedia("reisstdiehütteab.mp4");
        }

        [Command("normal")]
        public async Task SendNormal()
        {
            await SendMedia("normal.mp4");
        }

        [Command("residental")]
        [Alias("suburbs", "bus", "englisch")]
        public async Task SendResidental()
        {
            await SendMedia("residental.mp4");
        }

        [Command("schwein")]
        [Alias("tage gezählt", "tage", "pfeift", "lied", "absturz")]
        public async Task SendSchwein()
        {
            await SendMedia("schwein.mp4");
        }

        [Command("siemens")]
        public async Task SendSiemens()
        {
            await SendMedia("siemens.mp4");
        }

        [Command("söder")]
        [Alias("soeder", "beil", "axt", "wald")]
        public async Task SendSöder()
        {
            await SendMedia("söder.mp4");
        }

        [Command("speien")]
        public async Task SendSpeien()
        {
            await SendMedia("speien.mp4");
        }

        [Command("spital")]
        [Alias("suff", "alder")]
        public async Task SendSpital()
        {
            await SendMedia("spital.mp4");
        }

        [Command("staatsanwalt")]
        [Alias("anwalt")]
        public async Task SendAndwalt()
        {
            await SendMedia("staatsanwalt.mp4");
        }

        [Command("staatsbürger")]
        [Alias("staatsbürgerschaft", "jonglieren")]
        [MediaGroup("haider"), MediaGroup("hater")]
        public async Task SendStaatsbürger()
        {
            await SendMedia("staatsbürger.mp4");
        }

        [Command("strauss")]
        [Alias("strauß", "franz joseph")]
        public async Task SendStrauß()
        {
            await SendMedia("strauss.mp4");
        }

        [Command("streiken")]
        [Alias("streik")]
        public async Task SendStreiken()
        {
            await SendMedia("streiken.mp4");
        }

        [Command("unwiderstehlich")]
        public async Task SendUnwiderstehlich()
        {
            await SendMedia("unwiderstehlich.mp4");
        }

        [Command("unzwam")]
        public async Task SendUnzwam([Remainder] int index = 0)
        {
            await SendMedia("unzwam", index);
        }

        [Command("viper")]
        [Alias("weiper", "weipa")]
        public async Task SendViper()
        {
            await SendMedia("viper.mp4");
        }

        [Command("wichsfresse")]
        public async Task SendWichsfresse()
        {
            await SendMedia("wichsfresse.mp4");
        }

        [Command("würzburg")]
        [Alias("wuerzburg", "regensburg", "layla")]
        public async Task SendWürzburg()
        {
            await SendMedia("würzburg.mp4");
        }

        [Command("youtube")]
        public async Task SendYoutube()
        {
            await SendMedia("youtube.mp4");
        }

        [Command("spucken")]
        [Alias("spuck")]
        public async Task Spucken([Remainder] int index = 0)
        {
            await SendMediaFromMethodGroup("spucken", index);
        }

        [Command("hater")]
        [Alias("haider")]
        public async Task Hater([Remainder] int index = 0)
        {
            await SendMediaFromMethodGroup("hater", index);
        }

        [Command("weiter")]
        [Alias("weider", "weida", "weiter?", "weider?", "weida?")]
        public async Task SendUndWeiter()
        {
            await SendMedia("unweida.webp", true);
        }

        [Command("weida nuke")]
        [RequireRole(831216370242945076)]
        [RequireRole(935943281563553803)]
        public async Task UndWeiterNuke([Remainder] int count = 2)
        {
            var files = new List<FileAttachment>();
            for (var i = 0; i < count; i++)
            {
                files.Add(new FileAttachment(Constants.MediaBasePath + "unweida.webp", isSpoiler: true));
            }

            await Context.Channel.SendFilesAsync(files);
        }

        [Command("schnauf")]
        public async Task SendSchnauf()
        {
            await SendMedia("schnauf.webm");
        }


        private async Task SendMedia(string name, bool isSpoiler = false)
        {
            await Context.Channel.SendFileAsync(Constants.MediaBasePath + name, isSpoiler: isSpoiler);
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

        private async Task SendMediaFromMethodGroup(string methodGroup, int index)
        {
            var methods = _mediaGroupController.GetMethodsForGroup(methodGroup).ToList();
            if (index == 0)
            {
                var method = methods[Random.Shared.Next(0, methods.Count)];
                method.Invoke(this, null);
            }
            else
            {
                try
                {
                    var method = methods[index - 1];
                    method.Invoke(this, null);
                }
                catch (ArgumentOutOfRangeException)
                {
                    await Context.Message.ReplyAsync(
                        "Des Video gibt's net du Spinner! Da hat der Zimmermann kei Loch gelassen!");
                }
            }
        }

        public static string GetRandomMedia(string baseName)
        {
            var videos = GetAllMedia(baseName);
            return videos[Random.Shared.Next(0, videos.Count)];
        }

        public static List<string> GetAllMedia(string baseName)
        {
            return Directory.EnumerateFiles(Constants.MediaBasePath, baseName + "*").ToList();
        }
    }
}
