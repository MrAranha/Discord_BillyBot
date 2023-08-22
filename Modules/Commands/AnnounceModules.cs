using Discord.Commands;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillyBosta_DiscordApp.Modules.Commands
{
    public class AnouncesModules : ModuleBase<SocketCommandContext>
    {


        [Command("anuncio")]
        public async Task Anuncio(string title, string anuncio)
        {
            var embed = new EmbedBuilder
            {
                Title = title,
                Description = anuncio
            };
            await ReplyAsync(embed: embed.Build());
        }

        [Command("anuncio")]
        public async Task Anuncio()
        {
            await ReplyAsync(@"O Padrão para anúncios é de:
!anuncio (canal) (titulo) (anuncio)
!anuncio (canal) (titulo) (anuncio) (imagem)
!anuncio (canal) (titulo) (anuncio) (cor) ");
        }
    }
}
