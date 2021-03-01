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
                content = "Testing"
            };
            webobj.embeds.Add(new Embed
            {
                description = "async",
                title = " hefwq",
                Color = new DColor(255, 0, 255)
            });
            await new Webhook(url).SendAsync("gug");
        }
    }
}
