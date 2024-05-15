// ReSharper disable InconsistentNaming
namespace HttpServiceProvider.Models.RapidApi;

public class ConvertRate
{
    public bool success { get; set; }
    public Query? query { get; set; }
    public Info? info { get; set; }
    public string? date { get; set; }
    public double? result { get; set; }
}