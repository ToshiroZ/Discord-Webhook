# Discord.Webhook

Used for sending webhook messages to Discord using a Discord Webhook URL - Also completely dependency free!

[![NuGet version](https://badge.fury.io/nu/Discord.Webhook.svg)](https://badge.fury.io/nu/Discord.Webhook)

## Samples

### Creating a WebhookObject

```cs
var webhookObject = new WebhookObject();
```

### Building an embed object

```cs
webhookObject.AddEmbed(builder =>
{
	builder.WithTitle("Discord-Webhook lib")
		.WithDescription("Building embed with 'AddEmbed(Action<EmbedBuilder> embedBuilderFunction)'")
		.WithUrl("https://github.com/ToshiroZ/Discord-Webhook")
		.WithThumbnail("https://www.thegatewaypundit.com/wp-content/uploads/trump-mugshot-1.jpg")
		.WithColor(Colors.Magenta)
		.WithImage("https://i.imgur.com/ZGPxFN2.jpg")
		.AddField("New Field", "This is a new field")
		.WithFooter("DiscordUser",
			"https://www.thegatewaypundit.com/wp-content/uploads/trump-mugshot-1.jpg");
});
```

### Sending a message
```cs
await new Webhook("my-webhook-url").SendAsync(webhookObject);
```

### Sending a message with an avatar URL and username

```cs
await new Webhook("my-webhook-url", "username", "avatar url").SendAsync(webhookObject);
```

### Sending a message to a thread
> ⚠️  Ensure that your webhook URL is targeting a forum channel, any other channel type will throw an exception!

Since `v1.0.9`, the library now supports threads, you can set your messages to be targeted to a thread.

```cs
await new Webhook("my-webhook-url", "username", "avatar url")
            .SendAsync(webhookObject);
```

### Sending a message even easier
Since `v1.0.9`, you can send webhook messages easier by using a static method in the `Webhook` class called `SendAsync` This method is used to simplify the above call even further, although this is realistically only superior when you're only sending a single webhook message and not multiple calls.

```cs
await Webhook.SendAsync("my-webhook-url", webhookObject);
```

... and you can add an avatar URL, username, etc in the same way as before:

```cs
await Webhook.SendAsync("my-webhook-url", webhookObject, "username", "avatar url"); 
await Webhook.SendAsync("my-webhook-url", webhookObject, "username", "avatar url", "thread name");  // For threads
```

### Result

![image](https://raw.githubusercontent.com/ToshiroZ/Discord-Webhook/master/example.png)


## Error handling
Since `v1.0.9`, the library now handles exceptions from Discord's API to allow for easier debugging and handling on an application-level. All exceptions thrown by Discord are handled with a `DiscordWebhookException` in the `Discord.Webhook.Exceptions` namespace. You can handle exceptions like this:

```cs
try 
{
            var webhookObject = new WebhookObject();
            webhookObject.AddEmbed(builder =>
            {
            	builder.WithTitle("Discord-Webhook lib")
            		.WithDescription("Building embed with 'AddEmbed(Action<EmbedBuilder> embedBuilderFunction)'")
            		.WithUrl("https://github.com/ToshiroZ/Discord-Webhook")
            		.WithThumbnail("https://www.thegatewaypundit.com/wp-content/uploads/trump-mugshot-1.jpg")
            		.WithColor(Colors.Magenta)
            		.WithImage("https://i.imgur.com/ZGPxFN2.jpg")
            		.AddField("New Field", "This is a new field")
            		.WithFooter("DiscordUser",
            			"https://www.thegatewaypundit.com/wp-content/uploads/trump-mugshot-1.jpg");
            });
            
            await new Webhook("my-webhook-url").SendAsync(webhookObject);
}
catch (DiscordWebhookException ex)
{
            Console.WriteLine($"Exception from Discord: {ex.Message}");
}
