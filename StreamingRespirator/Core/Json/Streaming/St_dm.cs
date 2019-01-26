using System;
using System.Diagnostics;
using Newtonsoft.Json;
using StreamingRespirator.Core.Json.Tweetdeck;

namespace StreamingRespirator.Core.Json.Streaming
{
    [DebuggerDisplay("{Item.Sender.ScreenName} -> {Item.Recipient.ScreenName} : {Item.Text}")]
    internal class St_dm
    {
        [JsonProperty("direct_message")]
        public St_dm_item Item { get; set; } = new St_dm_item();

        public override string ToString()
            => $"{this.Item.Sender.ScreenName} -> {this.Item.Recipient.ScreenName} : {this.Item.Text}";
    }

    internal class St_dm_item
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        
        [JsonProperty("sender")]
        public TwitterUser Sender { get; set; }

        [JsonProperty("sender_id")]
        public long SenderId { get; set; }

        [JsonProperty("sender_screen_name")]
        public string SenderScreenName { get; set; }
        
        [JsonProperty("recipient")]
        public TwitterUser Recipient { get; set; }

        [JsonProperty("recipient_id")]
        public long RecipientId { get; set; }

        [JsonProperty("recipient_screen_name")]
        public string RecipientScreenName { get; set; }

    }
}
