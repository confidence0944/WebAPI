using WebAPI.Interface;
using WebAPI.Model;

namespace WebAPI.Service
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ILogger<CurrencyService> _logger;
        private readonly ICurrencyRepositoty _currencyRepositoty;

        public CurrencyService(ILogger<CurrencyService> logger, ICurrencyRepositoty currencyRepositoty)
        {
            _logger = logger;
            _currencyRepositoty = currencyRepositoty;
        }

        public async Task Create(CurrencyModel model)
        {
            _currencyRepositoty.Create(new Entities.TbCurrency() { Currency = model.Currency, CurrencyName = model.CurrencyName });
        }

        public async Task Delete(string currency)
        {
            _currencyRepositoty.Delete(currency);
        }

        public async Task Update(CurrencyModel model)
        {
            _currencyRepositoty.Update(new Entities.TbCurrency() { Currency = model.Currency, CurrencyName = model.CurrencyName });
        }
    }
}