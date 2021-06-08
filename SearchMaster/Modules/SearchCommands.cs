using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace SearchMaster.Modules.Searches
{
    public class Searches : ModuleBase
    {
        [Command("WeatherSearch")]
        public async Task WeatherSearchAsync(string city)
        {
            var e = await new WeatherApiSearch().WeatherApiAsync(city);
            var embed = new EmbedBuilder()
                .AddField(":map: Location", e.name + ", " + e.sys.country)
                .AddField(":triangular_ruler: Latitude / Longitude", e.coord.lat + " , " + e.coord.lon)
                .AddField(":cloud: Cloud Coverage", e.clouds.all + "%")
                .AddField(":sweat: Humidity", e.main.humidity + "%")
                .AddField(":dash: Wind Speed", e.wind.speed + "m/s")
                .AddField(":thermometer: Low / High", new Conversions().KelvinToFehrenheit(e.main.temp_min) + " ° F - " + new Conversions().KelvinToFehrenheit(e.main.temp_max) + " ° F\n" + new Conversions().KelvinToCelcius(e.main.temp_min) + "° C - " + new Conversions().KelvinToCelcius(e.main.temp_max) + "° C");
            await Context.Channel.SendMessageAsync(embed: embed.Build());
            if (e.cod == 404)
            {
                var Eembed = new EmbedBuilder()
                    .WithDescription("City not found.");
                await Context.Channel.SendMessageAsync(embed: Eembed.Build());
            }
        }
        [Command("say")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        public async Task SayAsync([Remainder] string message)
        {
            var sayEmbed = new EmbedBuilder()
            .WithDescription(message);
            await Context.Channel.SendMessageAsync(embed: sayEmbed.Build());
        }
        [Command("bible")]
        public async Task BibleAsync(string book, string chapterAndVerse)
        {
            var bibleLookup = await new BibleApiSearch().BibleApiAsync(book, chapterAndVerse);
            var embed = new EmbedBuilder()
                .WithTitle($"{bibleLookup.text}");
            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }

    }
}