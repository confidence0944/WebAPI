using Microsoft.AspNetCore.Mvc;
using WebAPI.Model;
using WebAPI.Service;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ApiBaseController
    {
        private ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {         
            return CustomOKResult(await _currencyService.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CurrencyModel currency)
        {
            await _currencyService.Create(currency);
            return CustomOKResult();
        }

        [HttpPatch]
        public async Task<IActionResult> Edit(CurrencyModel category)
        {
            await _currencyService.Update(category);
            return CustomOKResult();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string currency)
        {
            if (string.IsNullOrEmpty(currency))
                throw new ArgumentNullException(nameof(currency));

            await _currencyService.Delete(currency);
            return CustomOKResult();
        }
    }
}
