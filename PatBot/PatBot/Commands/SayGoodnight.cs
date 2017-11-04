using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatBot.Commands
{
    class SayGoodnight : ICommand
    {
        public IEnumerable<string> MessageCommandAntiMatchRegexes => new List<string>();

        public IEnumerable<string> MessageCommandMatchRegexes => new List<string> { "say goodnight, patbot" };

        public async Task Do(DiscordMessage msg, DiscordGuild server)
        {
            await msg.RespondAsync("Night night!");
        }
    }
}
