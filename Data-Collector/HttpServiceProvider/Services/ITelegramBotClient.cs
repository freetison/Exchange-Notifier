using HttpServiceProvider.Models.Telegram;

namespace HttpServiceProvider.Services;

public interface ITelegramBotClient
{
    Task<SendMessageResponse> SendMessageAsync(long chatId, string message);
    
    Task<UpdateResponse> GetUpdates();
    
    Task<GetMeResponse> GetMe();
    
    Task<ChatResponse> GetChat(int chatId);

}