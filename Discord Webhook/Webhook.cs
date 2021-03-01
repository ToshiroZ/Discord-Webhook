using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Discord.Webhook
{
    /// <summary>
    /// The class that handles sending requests to the discord webhook api
    /// </summary>
    public class Webhook
    {
        private static HttpClient webhookClient = new HttpClient();
        private readonly string _url;
        private readonly string _username = null;
        private readonly string _avatarurl = null;

        /// <summary>
        /// The constructor for the webhook
        /// </summary>
        /// <param name="webhookurl">The url for the webhook</param>
        /// <param name="username">The username the webhook will send with if its null defaults to what was assigned in the webhook menu on discord</param>
        /// <param name="avatarurl">The url to the picture the webhook will use if its null defaults to what was assigned in the webhook menu on discord</param>
        public Webhook(string webhookurl, string username = null, string avatarurl = null)
        {
            _url = webhookurl;
            _username = username;
            _avatarurl = avatarurl;
        }
        /// <summary>
        /// Sends a message to the discord webhook
        /// </summary>
        /// <param name="message"></param>
        public void Send(string message)
        {
            try
            {
                Dictionary<string, string> contents = new Dictionary<string, string>
                    {
                        { "content", message },
                        { "username", _username },
                        { "avatar_url", _avatarurl }
                    };

                webhookClient.PostAsync(_url, new FormUrlEncodedContent(contents)).GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// sends a webhookobject to the discord webhook used mostly for embeds
        /// </summary>
        /// <param name="obj">the webhook object</param>
        public void Send(WebhookObject obj)
        {
            try
            {
                obj.username = _username == null ? obj.username : _username;
                obj.avatar_url = _username == null ? obj.avatar_url : _avatarurl;
                var json = new StringContent(obj.ToString(), Encoding.UTF8, "application/json");

                webhookClient.PostAsync(_url, json).GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// send a message to the discord webhook asynchronously
        /// </summary>
        /// <param name="message"></param>
        /// <returns>the message to send</returns>
        public async Task SendAsync(string message)
        {
            try
            {
                Dictionary<string, string> contents = new Dictionary<string, string>
                    {
                        { "content", message },
                        { "username", _username },
                        { "avatar_url", _avatarurl }
                    };

                await webhookClient.PostAsync(_url, new FormUrlEncodedContent(contents));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Send a webhook object to the discord api asynchronously 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task SendAsync(WebhookObject obj)
        {
            try
            {
                var json = new StringContent(obj.ToString(), Encoding.UTF8, "application/json");

                await webhookClient.PostAsync(_url, json);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
