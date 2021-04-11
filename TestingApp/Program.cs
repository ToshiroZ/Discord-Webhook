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
            webobj.embeds.Add(new Embed
            {
                description = "async",
                title = "Testing Webhook",
                Color = Colors.Magenta
            });
            await new Webhook(url).SendAsync(webobj);
        }
    }
}
