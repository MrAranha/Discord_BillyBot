using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillyBosta_DiscordApp.Interfaces
{
    public interface IMessageErrorsHandler
    {
        Task Handle(ICommandContext context, IResult result);
    }
}
