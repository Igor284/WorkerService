using System.Xml.Serialization;
using WorkerService.Models;
using WorkerService.Strategies.FormatStrategy;

namespace WorkerService.FormatStrategy
{
    public class XmlFormatStrategy : BaseFormatStrategy
    {
        protected override void ConvertorInternal(IEnumerable<CurrencyRate> rates)
        {
            var serializer = new XmlSerializer(typeof(List<CurrencyRate>));

            using (var stringWriter = new StringWriter())
            {
                serializer.Serialize(stringWriter, rates);
                var xml = stringWriter.ToString();

                File.WriteAllText(@"C:\temp\workerservice\rates.txt", xml);
            }
        }
    }
}
