using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatBot.Commands
{
    class UnassignPronoun : ICommand
    {
        public IEnumerable<string> MessageCommandAntiMatchRegexes => new List<string>();
        public IEnumerable<string> MessageCommandMatchRegexes => new List<string> { cmd };

        const string cmd = "unassign self pronoun: ";

        public async Task Do(DiscordMessage msg, DiscordGuild server)
        {
            string pronoun = msg.Content.Substring(cmd.Length).ToLower();
            if (!string.IsNullOrWhiteSpace(pronoun) && char.IsLetter(pronoun[0]))
            {
                string pronounRole = "Pronoun: " + pronoun;
                bool roleFound = false;
                DiscordRole rl = null;
                foreach (var role in server.Roles)
                {
                    if (role.Name == pronounRole)
                    {
                        rl = role;
                        roleFound = true;
                        break;
                    }
                }

                if (roleFound)
                {
                    await server.RevokeRoleAsync(msg.Author as DiscordMember, rl, "User requested this.");
                }
            }
        }
    }
}
