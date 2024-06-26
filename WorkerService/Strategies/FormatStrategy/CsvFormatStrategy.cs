using System.Text;
using WorkerService.Models;
using WorkerService.Strategies.FormatStrategy;

namespace WorkerService.FormatStrategy
{
    public class CsvFormatStrategy : BaseFormatStrategy
    {
        protected override void ConvertorInternal(IEnumerable<CurrencyRate> rates)
        {
            var csv = new StringBuilder();

            csv.AppendLine("StartDate,TimeSign,CurrencyCode,CurrencyCodeL,Units,Amount");

            foreach (var rate in rates)
            {
                csv.AppendLine($"{rate.StartDate},{rate.TimeSign},{rate.CurrencyCode},{rate.CurrencyCodeL},{rate.Units},{rate.Amount}");
            }

            File.WriteAllText(@"C:\temp\workerservice\rates.txt", csv.ToString());
        }
    }
}
