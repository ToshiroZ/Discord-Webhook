using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Discord.Webhook.Exceptions;
using Discord.Webhook.Models;

namespace Discord.Webhook;

/// <summary>
///     The class that handles sending requests to the Discord webhook
/// </summary>
public class Webhook
{
	private static readonly HttpClient WebhookClient = new();

	private readonly string _url;
	private readonly string? _username;
	private readonly string? _avatarUrl;
	private readonly string? _threadName;

	/// <summary>
	///     Constructs a new Discord webhook object
	/// </summary>
	/// <exception cref="ArgumentException">
	///     Thrown when one of the arguments provided does not match validation rules set at
	///     Discord's end, for example the username cannot contain 'Discord'.
	/// </exception>
	/// <param name="webhookUrl">The Discord webhook URL</param>
	/// <param name="username">
	///     The username the webhook will send with, if its null defaults to what was assigned in the webhook
	///     menu on Discord
	/// </param>
	/// <param name="avatarUrl">
	///     The URL that points to the avatar image the webhook will use, if its null defaults to what was assigned in the
	///     webhook menu on Discord
	/// </param>
	/// <param name="threadName">The name of the thread to put the webhook message in</param>
	public Webhook(string webhookUrl, string? username = null, string? avatarUrl = null, string? threadName = null)
	{
		username.ValidateWebhookUsername(); // Validate username
		
		_url = webhookUrl;
		_username = username;
		_avatarUrl = avatarUrl;
		_threadName = threadName;
	}

	/// <summary>
	///     Executes the Discord webhook request
	/// </summary>
	/// <param name="content">The content to send to Discord</param>
	/// <param name="cancellationToken">The cancellation token</param>
	/// <exception cref="DiscordWebhookException">Thrown when Discord returns an error, the message contains the details</exception>
	private async Task ExecuteAsync(HttpContent content, CancellationToken cancellationToken = default)
	{
		// Setup the base message and then the content
		using var message = new HttpRequestMessage(HttpMethod.Post, _url);
		message.Content = content;

		// Get the library version and then create a user agent
		var version = Assembly.GetCallingAssembly().GetName().Version.ToString();
		var productInfo = new ProductInfoHeaderValue("Discord-Webhook-NET", version);
		var userAgent = $"{productInfo} (+https://github.com/ToshiroZ/Discord-Webhook)";

		// Add the user agent
		message.Headers.UserAgent.ParseAdd(userAgent);

		// Now we can execute the request
		using var response = await WebhookClient.SendAsync(message, cancellationToken);

		// We check if it's a failure status code, if so parse it back to a DiscordError and throw an exception
		if (!response.IsSuccessStatusCode)
		{
			// Get the response string
			var str = await response.Content.ReadAsStringAsync();
			var error = JsonSerializer<DiscordError>.Deserialize(str);

			// If we couldn't deserialize it then just return a fallback error message
			if (string.IsNullOrWhiteSpace(error?.message))
				throw new DiscordWebhookException(
					$"Something went wrong whilst executing the request, server responded with {response.StatusCode}");

			// Throw the exception
			throw new DiscordWebhookException(error!);
		}
	}

	/// <summary>
	///     Sets the message arguments for a <see cref="WebhookObject" />, this includes the username, avatar URL and thread
	///     name
	/// </summary>
	/// <param name="obj">The webhook object to set the arguments for</param>
	private WebhookObject SetMessageArguments(WebhookObject obj)
	{
		obj.username = _username ?? obj.username;
		obj.avatar_url = _username is null ? obj.avatar_url : _avatarUrl;
		obj.thread_name = _threadName ?? obj.thread_name;

		return obj;
	}

	/// <summary>
	///     Gets the message body for a simple text-based message (no embeds)
	/// </summary>
	/// <param name="message">The message content to set</param>
	/// <returns>The new <see cref="Dictionary{TKey,TValue}" /> ready for serialization</returns>
	private Dictionary<string, string> GetMessageBody(string message)
	{
		var contents = new Dictionary<string, string>
		{
			{ "content", message }
		};

		if (_username is not null) contents.Add("username", _username);
		if (_avatarUrl is not null) contents.Add("avatar_url", _avatarUrl);
		if (_threadName is not null) contents.Add("thread_name", _threadName);

		return contents;
	}

	/// <summary>
	///     Sends a message to the Discord webhook synchronously
	/// </summary>
	/// <exception cref="DiscordWebhookException">Thrown when Discord returns an error, the message contains the details</exception>
	/// <param name="message">The string message to send to the webhook</param>
	public void Send(string message)
	{
		var contents = GetMessageBody(message);
		ExecuteAsync(new FormUrlEncodedContent(contents)).GetAwaiter().GetResult();
	}

	/// <summary>
	///     Sends a webhook object to the Discord webhook synchronously
	/// </summary>
	/// <exception cref="DiscordWebhookException">Thrown when Discord returns an error, the message contains the details</exception>
	/// <param name="obj">The object to send to the webhook</param>
	public void Send(WebhookObject obj)
	{
		obj = SetMessageArguments(obj);
		var json = new StringContent(obj.ToString(), Encoding.UTF8, "application/json");

		ExecuteAsync(json).GetAwaiter().GetResult();
	}

	/// <summary>
	///     Deletes the webhook
	/// </summary>
	public void Delete()
	{
		WebhookClient.DeleteAsync(_url).GetAwaiter().GetResult();
	}

	/// <summary>
	///     Sends a message to the Discord webhook asynchronously
	/// </summary>
	/// <param name="message">The string message to send to the webhook</param>
	/// <param name="cancellationToken">The cancellation token</param>
	/// <exception cref="DiscordWebhookException">Thrown when Discord returns an error, the message contains the details</exception>
	/// <returns>A <see cref="Task" /> representing the status of the operation</returns>
	public async Task SendAsync(string message, CancellationToken cancellationToken = default)
	{
		var contents = GetMessageBody(message);
		await ExecuteAsync(new FormUrlEncodedContent(contents), cancellationToken);
	}

	/// <summary>
	///     Sends a webhook object to the Discord webhook asynchronously
	/// </summary>
	/// <param name="obj">The object to send to the webhook</param>
	/// <param name="cancellationToken">The cancellation token</param>
	/// <exception cref="DiscordWebhookException">Thrown when Discord returns an error, the message contains the details</exception>
	/// <returns>A <see cref="Task" /> representing the status of the operation</returns>
	public async Task SendAsync(WebhookObject obj, CancellationToken cancellationToken = default)
	{
		obj = SetMessageArguments(obj);
		var json = new StringContent(obj.ToString(), Encoding.UTF8, "application/json");

		await ExecuteAsync(json, cancellationToken);
	}

	/// <summary>
	///     Deletes the webhook asynchronously
	/// </summary>
	/// <param name="cancellationToken">The cancellation token</param>
	/// <returns>A <see cref="Task" /> representing the status of the operation</returns>
	public async Task DeleteAsync(CancellationToken cancellationToken = default)
	{
		await WebhookClient.DeleteAsync(_url, cancellationToken);
	}
}