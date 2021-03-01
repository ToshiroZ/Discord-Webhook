using Discord.Webhook;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discord.Webhook
{
    public static class Extensions
    {
        public static DColor ToDColor(this System.Drawing.Color color)
        {
            return new DColor(color.R, color.G, color.B);
        }
    }
}
