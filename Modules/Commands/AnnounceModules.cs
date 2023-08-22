using Discord.Commands;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using System.Threading.Channels;

namespace BillyBosta_DiscordApp.Modules.Commands
{
    public class AnouncesModules : ModuleBase<SocketCommandContext>
    {
        [Command("anuncio")]
        public async Task Anuncio(string title, string anuncio, ulong channelID)
        {
            var client = Context.Client;
            var channel = client.GetChannel(channelID) as SocketTextChannel;


            var embed = new EmbedBuilder
            {
                Title = title,
                Description = anuncio,
                Color = Color.DarkRed
            };
            embed.WithFooter(EmbedFooter => EmbedFooter.Text = "Void™");
            await channel.SendMessageAsync(embed: embed.Build());
        }
        [Command("anuncio")]
        public async Task Anuncio(string title, string anuncio, string imagem, ulong channelID)
        {
            var client = Context.Client;
            var channel = client.GetChannel(channelID) as SocketTextChannel;
            var embed = new EmbedBuilder
            {
                Title = title,
                Description = anuncio,
                ImageUrl = imagem,
                Color = Color.DarkRed
            };
            embed.WithFooter(EmbedFooter => EmbedFooter.Text = "Void™");
            await channel.SendMessageAsync(embed: embed.Build());
        }

        [Command("anuncio")]
        public async Task Anuncio()
        {
            await ReplyAsync(@"O Padrão para anúncios é de:
!anuncio (titulo) (anuncio) (imagem) (canal)
!anuncio (titulo) (anuncio) (canal)

usar aspas entre parâmetros");
        }
    }
}
