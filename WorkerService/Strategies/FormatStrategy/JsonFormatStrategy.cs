using System.Text.Json;
using WorkerService.Models;
using WorkerService.Strategies.FormatStrategy;

namespace WorkerService.FormatStrategy
{
    public class JsonFormatStrategy : BaseFormatStrategy
    {
        protected override void ConvertorInternal(IEnumerable<CurrencyRate> rates)
        {
            var json = JsonSerializer.Serialize(rates, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(@"C:\temp\workerservice\rates.txt", json);
        }
    }
}
