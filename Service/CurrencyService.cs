using WebAPI.Entities;
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
            _currencyRepositoty.Create(new TbCurrency() { Currency = model.Currency, CurrencyName = model.CurrencyName });
        }

        public async Task Delete(string currency)
        {
            _currencyRepositoty.Delete(currency);
        }

        public async Task<IEnumerable<CurrencyModel>> GetAll()
        {
            var data = _currencyRepositoty.GetAll();
            List<CurrencyModel> result = new List<CurrencyModel>();
            foreach (var item in data)
            {
                result.Add(new CurrencyModel() { Currency = item.Currency, CurrencyName = item.CurrencyName });
            }
            return result;
        }

        public async Task Update(CurrencyModel model)
        {
            _currencyRepositoty.Update(new TbCurrency() { Currency = model.Currency, CurrencyName = model.CurrencyName });
        }
    }
}