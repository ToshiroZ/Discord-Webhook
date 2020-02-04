using Discord_Webhook;
using Discord.Webhook;
using System;

namespace TestingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = Console.ReadLine();
            var embeds = new Embed[1];
            embeds[0] = new Embed
            {
                description = "HI",
                title = " hefwq"
            };
            var webobj = new WebhookObject
            {
                content = "Testing",
                embeds = embeds,
                username = "Toshi"
            };
            new Webhook(url).Send(webobj);
        }
    }
}
