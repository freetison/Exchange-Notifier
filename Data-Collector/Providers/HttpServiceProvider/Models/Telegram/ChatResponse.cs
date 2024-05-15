// ReSharper disable InconsistentNaming
namespace HttpServiceProvider.Models.Telegram;

public class ChatResponse
{
    public bool ok { get; set; }
    public Chat? result { get; set; }
}