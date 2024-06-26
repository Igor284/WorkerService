using WorkerService.Models;

namespace WorkerService.Strategies.FormatStrategy
{
    public abstract class BaseFormatStrategy : IFormatStrategy
    {
        public void Convertor(IEnumerable<CurrencyRate> rates)
        {
            ConvertorInternal(rates);
        }

        protected abstract void ConvertorInternal(IEnumerable<CurrencyRate> rates);
    }
}
