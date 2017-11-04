using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace PatBot.Commands
{
    class Ping : ICommand
    {
        public IEnumerable<string> MessageCommandAntiMatchRegexes => new List<string>();

        public IEnumerable<string> MessageCommandMatchRegexes => new List<string> { "ping" };

        public async Task Do(DiscordMessage msg, DiscordGuild server)
        {
            await msg.RespondAsync("pong!");
        }
    }
}
