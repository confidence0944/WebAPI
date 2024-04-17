using WebAPI.Model;

namespace WebAPI.Service
{
    public interface ICurrencyService
    {
        Task Create(CurrencyModel model);

        Task Update(CurrencyModel model);

        Task Delete(string currency);
    }
}
