using Discord.Commands;
using Discord;
using Discord.Webhook;
using Discord.WebSocket;
using Discord.Net;
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
            
            //Verificação de Cargo do usuário, necessário automatizar isso e linkar
            //com banco de dados em um service genérico para uso futuro
            var user = (Context.User as SocketGuildUser)!;
            //Verificação manual não é prático nem bom
            var role = Context.Guild.GetRole(1004905652017188945);
            if (!user.Roles.Contains(role))
            {
                await ReplyAsync("Você não tem permissão para utilizar esse comando!");
                return;
            }
            
            
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

Obs: Usar aspas entre parâmetros!");
        }
    }
}
