using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatBot
{
    interface ICommand
    {
        IEnumerable<string> MessageCommandMatchRegexes { get; }
        IEnumerable<string> MessageCommandAntiMatchRegexes { get; }
        Task Do(DiscordMessage msg, DiscordGuild server);
    }
}
