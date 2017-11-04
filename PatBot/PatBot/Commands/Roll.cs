using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace PatBot.Commands
{
    class Roll : ICommand
    {
        public IEnumerable<string> MessageCommandAntiMatchRegexes => new List<string> { "you rolled" };

        public IEnumerable<string> MessageCommandMatchRegexes => new List<string> { "roll", "\\!r" };

        static Random rnd = new Random();

        public async Task Do(DiscordMessage msg, DiscordGuild server)
        {
            string m = msg.Content.ToLower();

            int i = m.IndexOf("roll");
            string searchStr = m.Substring(i + 4);
            if (searchStr.Contains("d"))
            {
                i = searchStr.IndexOf('d');
                string searchStrValue = searchStr.Substring(i + 1);
                string searchStrCount = searchStr.Substring(0, i);
                bool successValue = false;
                bool successCount = false;
                int value = TakeNumber(searchStrValue, out successValue);
                int count = TakeNumber(searchStrCount, out successCount);

                if (count == 0 || value == 0)
                {
                    await msg.RespondAsync("Stopppp trying to trickk meeeee.....");
                }
                else if (value > 10000 || count > 10000)
                {
                    await msg.RespondAsync("Your values are too big! This bot isn't gonna do your calculatin' all day!");
                }
                else
                {

                    if (!successCount)
                    {
                        count = 1;
                    }

                    if (successValue)
                    {
                        StringBuilder builder = new StringBuilder();
                        int sum = 0;
                        for (int n = 0; n < count; n++)
                        {
                            int r = rnd.Next(1, value);
                            sum += r;
                            builder.Append(r);

                            if (n != count - 1)
                            {
                                builder.Append(" + ");
                            }
                        }


                        await msg.RespondAsync("You rolled " + sum.ToString() + "!!!" + 
                            (count > 1 ? " (" + builder.ToString() + ")" : string.Empty));
                    }
                }
            }
        }

        static int TakeNumber(string searchStr, out bool success)
        {
            success = false;
            string numberChars = string.Empty;
            bool numberStarted = false;

            foreach (char c in searchStr)
            {
                if (char.IsNumber(c))
                {
                    numberStarted = true;
                    numberChars += c;
                }
                else if (numberStarted)
                {
                    break;
                }
            }

            if (numberChars.Length > 0)
            {
                int result = 0;
                if (int.TryParse(numberChars, out result))
                {
                    success = true;
                    return result;
                }
            }

            success = false;
            return -1;
        }
    }
}
