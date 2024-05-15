// ReSharper disable InconsistentNaming
namespace HttpServiceProvider.Models.RapidApi
{
    public class ExchangeRates
    {
        public long Timestamp { get; set; }
        public string? Base { get; set; }
        public bool Success { get; set; }
        public string? Date { get; set; }
        public Dictionary<string, double>? Rates { get; set; }
    }

}