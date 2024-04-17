using Microsoft.AspNetCore.Mvc;
using WebAPI.Service;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RateController : ApiBaseController
    {
        private readonly IRateService _rateService;

        public RateController(IRateService rateService)
        {
            _rateService = rateService ?? throw new ArgumentNullException(nameof(rateService));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _rateService.GetRate();
            return CustomOKResult(result);
        }
    }
}