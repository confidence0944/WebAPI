using WebAPI.Model;

namespace WebAPI.Service
{
    public interface IRateService
    {
        Task<RateModel> GetRate();
    }
}
