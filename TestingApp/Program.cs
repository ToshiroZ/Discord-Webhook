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

            var webobj = new WebhookObject();

            webobj.AddEmbed(builder =>
            {
                builder.WithTitle("Discord-Webhook lib")
                    .WithDescription("Building embed with 'AddEmbed(Action<EmbedBuilder> embedBuilderFunction)'")
                    .WithUrl("https://github.com/ToshiroZ/Discord-Webhook")
                    .WithThumbnail("https://upload.wikimedia.org/wikipedia/commons/thumb/6/6b/Font_Awesome_5_brands_discord_color.svg/800px-Font_Awesome_5_brands_discord_color.svg.png")
                    .WithColor(Colors.Magenta)
                    .WithImage("https://i.imgur.com/ZGPxFN2.jpg")
                    .AddField("New Field", "This is a new field")
                    .WithFooter("DiscordUser", "https://upload.wikimedia.org/wikipedia/commons/thumb/6/6b/Font_Awesome_5_brands_discord_color.svg/800px-Font_Awesome_5_brands_discord_color.svg.png");
            });
                
            webobj.AddEmbed(new EmbedBuilder()
                    .WithTitle("Discord-Webhook lib second message")
                    .WithDescription("Testing discord Webhook")
                    .WithColor(Colors.Orange)
            );

            await new Webhook(url).SendAsync(webobj);
        }
    }
}