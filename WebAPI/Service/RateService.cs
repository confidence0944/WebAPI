using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using WebAPI.Interface;
using WebAPI.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAPI.Service
{
    public class RateService : IRateService
    {
        private readonly ILogger<RateService> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ICurrencyRepositoty _currencyRepositoty;

        public RateService(ILogger<RateService> logger, ICurrencyRepositoty currencyRepositoty, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _currencyRepositoty = currencyRepositoty;
        }

        public async Task<RateModel> GetRate()
        {
            var result = new RateModel();

            var currencyName = _currencyRepositoty.GetAll();

            using (var client = _clientFactory.CreateClient("CoindeskHttpClient"))
            {
                var response = await client.SendAsync(new HttpRequestMessage());
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CoindeskApiModel>(content);
                    result.UpdateTime = data.time.updatedISO;

                    foreach (var item in data.bpi)
                    {
                        result.RateList.Add(new RateModel.RateData()
                        {
                            Currency = item.Key,
                            CurrencyChineseName = currencyName.FirstOrDefault(x => x.Currency == item.Key)?.CurrencyName.Trim(),
                            Rate = item.Value.rate_float
                        });
                    }
                }
            }

            //throw new Exception("測試 GlobleExceptionHandler");
            result.RateList = result.RateList.OrderBy(x => x.Currency).ToList();
            return result;
        }
    }
}