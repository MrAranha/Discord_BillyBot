using BillyBosta_DiscordApp.Interfaces;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillyBosta_DiscordApp.Handlers
{
    public class MessageErrorsHandler : IMessageErrorsHandler
    {
        public MessageErrorsHandler()
        {

        }

        public async Task Handle(ICommandContext context, IResult result)
        {
            switch(result.Error)
            {
                case CommandError.BadArgCount:
                    await context.Channel.SendMessageAsync($"Contagem incorreta de parâmetros, caso esteja com dúvida digite o comando sem parâmetro algum!");
                    break;
                default:
                    break;
            }
        }
    }
}
