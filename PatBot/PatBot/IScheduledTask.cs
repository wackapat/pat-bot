using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatBot
{
    interface IScheduledTask
    {
        TimeSpan Frequency { get; }
        Task Do(DiscordGuild discord);
    }
}
