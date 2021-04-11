using Discord.Webhook;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestingApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var url = Console.ReadLine();

            var webobj = new WebhookObject
            {
                content = null
            };
            webobj.embeds.Add(
                new EmbedBuilder()
                    .WithTitle("Discord-Webhook lib")
                    .WithDescription("Testing discord Webhook")
                    .WithUrl("https://github.com/ToshiroZ/Discord-Webhook")
                    .WithThumbnail("https://upload.wikimedia.org/wikipedia/commons/thumb/6/6b/Font_Awesome_5_brands_discord_color.svg/800px-Font_Awesome_5_brands_discord_color.svg.png")
                    .WithColor(Colors.Magenta)
                    .WithImage("https://i.imgur.com/ZGPxFN2.jpg")
                    .AddField("New Field", "This is a new field")
                    .WithFooter("DiscordUser", "https://upload.wikimedia.org/wikipedia/commons/thumb/6/6b/Font_Awesome_5_brands_discord_color.svg/800px-Font_Awesome_5_brands_discord_color.svg.png")
                    .Build()
            );

            await new Webhook(url).SendAsync(webobj);
        }
    }
}