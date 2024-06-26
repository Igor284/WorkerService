namespace WorkerService.Models
{
    public class CurrencyRate
    {
        public string StartDate { get; set; }

        public string TimeSign { get; set; }

        public int CurrencyCode { get; set; }

        public string CurrencyCodeL { get; set; }

        public int Units { get; set; }

        public decimal Amount { get; set; }
    }
}
