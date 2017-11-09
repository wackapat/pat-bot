using DSharpPlus.Entities;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PatBot.ScheduledTasks
{
    class TellDadJoke : IScheduledTask
    {
        public TimeSpan Frequency => TimeSpan.FromMinutes(5.0);

        static Logger logger = LogManager.GetCurrentClassLogger();
        static Random rnd = new Random();

        private DataTable table;

        OleDbConnection connection = new OleDbConnection(
                "Provider=Microsoft.Jet.OleDb.4.0; Data Source = " +
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
                "; Extended Properties = \"Text;HDR=YES;FMT=Delimited\"");

        public async Task Load()
        {
            await connection.OpenAsync();

            OleDbDataAdapter adapter = new OleDbDataAdapter(
                "SELECT * FROM shortjokes.csv", connection);

            DataSet ds = new DataSet("JokesSet");
            adapter.FillError += Adapter_FillError;
            adapter.Fill(ds);

            connection.Close();

            table = ds.Tables[0];
        }

        private string GetJoke()
        {
            return table.Rows[rnd.Next(0, table.Rows.Count - 1)]["Joke"].ToString();
        }

        private void Adapter_FillError(object sender, FillErrorEventArgs e)
        {
            logger.Error(e.Errors);
        }

        public async Task Do(DiscordGuild discord)
        {
            DiscordChannel channel = discord.Channels.First(f => f.Name == "dad_jokes");
            string joke = GetJoke();

            await channel.SendMessageAsync(joke);
        }
    }
}
