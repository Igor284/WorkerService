using WorkerService.Strategies;

namespace WorkerService.Services.Interfaces
{
    public interface IFormatStrategyFactory
    {
        bool TryGet(string format, out IFormatStrategy strategy);
    }
}
