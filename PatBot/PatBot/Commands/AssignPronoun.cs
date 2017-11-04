using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace PatBot.Commands
{
    class AssignPronoun : ICommand
    {
        public IEnumerable<string> MessageCommandAntiMatchRegexes => new List<string> { "unassign self pronoun: " };
        public IEnumerable<string> MessageCommandMatchRegexes => new List<string> { cmd };

        const string cmd = "assign self pronoun: ";

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

                if (!roleFound)
                {
                    rl = await server.CreateRoleAsync(pronounRole);
                }

                await server.GrantRoleAsync(msg.Author as DiscordMember, rl);
            }
        }
    }
}
