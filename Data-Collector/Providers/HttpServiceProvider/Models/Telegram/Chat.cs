// ReSharper disable InconsistentNaming
namespace HttpServiceProvider.Models.Telegram;

public class Chat
{
    public long id { get; set; }
    public string? first_name { get; set; }
    public string? last_name { get; set; }
    public string? username { get; set; }
    public string? type { get; set; }
}