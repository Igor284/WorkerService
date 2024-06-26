using WorkerService.Models;

namespace WorkerService.Strategies
{
    public interface IFormatStrategy
    {
        public void Convertor(IEnumerable<CurrencyRate> matrices);
    }
}
