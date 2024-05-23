using Flurl.Http;
using Flurl.Http.Configuration;
using HttpServiceProvider.Models.Telegram;

namespace HttpServiceProvider.Services
{
    public class TelegramBotClient(IFlurlClientCache clients) : ITelegramBotClient
    {
        private readonly IFlurlClient _flurlCli = clients.Get("TelegramBot");

        public async Task<SendMessageResponse> SendMessageAsync(long chatId, string message)
        {
            var payload = new Dictionary<string, string>
            {
                { "chat_id", chatId.ToString() },
                { "text", message }
            };

            return await _flurlCli
                .Request("sendMessage")
                .PostJsonAsync(payload).ReceiveJson<SendMessageResponse>();
                
        }

        public async Task<UpdateResponse> GetUpdates()
        {
            return await _flurlCli
                .Request("getUpdates")
                .GetJsonAsync<UpdateResponse>();

        }

        public async Task<GetMeResponse> GetMe()
        {
            return await _flurlCli
                .Request("getMe")
                .PostAsync().ReceiveJson<GetMeResponse>();
        }

        public async Task<ChatResponse> GetChat(int chatId)
        {
            return await _flurlCli
                .Request("getchat")
                .SetQueryParams(new { chat_id = chatId })
                .GetJsonAsync<ChatResponse>();
        }
    }

}