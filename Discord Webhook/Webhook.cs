using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Discord.Webhook
{
    public class Webhook
    {
        private readonly string _url;
        private readonly string _username = "Toshi's Webhook Library";
        private readonly string _avatarurl;

        public Webhook(string webhookurl, string username = null, string avatarurl = null)
        {
            _url = webhookurl;
            _username = username;
            _avatarurl = avatarurl;
        }

        public void Send(string message)
        {
            try
            {
                HttpClient client = new HttpClient();
                Dictionary<string, string> contents = new Dictionary<string, string>
                    {
                        { "content", message },
                        { "username", _username },
                        { "avatar_url", _avatarurl }
                    };

                client.PostAsync(_url, new FormUrlEncodedContent(contents)).GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void Send(WebhookObject obj)
        {
            using (WebClient wb = new WebClient())
            {
                wb.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                wb.UploadString(_url, "POST", obj.ToString());
            }
        }
    }
}
