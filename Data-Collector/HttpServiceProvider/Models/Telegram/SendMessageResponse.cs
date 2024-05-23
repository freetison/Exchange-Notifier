// ReSharper disable InconsistentNaming

using Newtonsoft.Json;

namespace HttpServiceProvider.Models.Telegram;

public class SendMessageResponse
{
    public bool ok { get; set; }
    
    [JsonProperty("result")]
    public Message? result { get; set; }
}