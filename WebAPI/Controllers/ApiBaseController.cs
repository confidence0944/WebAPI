using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Net;
using WebAPI.Utilities;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    public class ApiBaseController : ControllerBase
    {
        protected IActionResult CustomResult(string code, string message, object data, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            ApiResponse apiResponse = new ApiResponse(code, message, data);
            return new JsonResult(apiResponse);
        }

        protected IActionResult CustomOKResult(object? data, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            ApiResponse apiResponse = data == null
                ? new ApiResponse(ReturnCode.NoData, null)
                : new ApiResponse(ReturnCode.Success, data);

            return new JsonResult(apiResponse, serializerSettings);
        }

        protected IActionResult CustomOKResult(HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            ApiResponse apiResponse = new ApiResponse(ReturnCode.Success, null);
            return new JsonResult(apiResponse, serializerSettings);
        }

        private static JsonSerializerSettings serializerSettings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
            Converters = { new JsonDateTimeConverter(), new StringEnumConverter() },
        };
    }
}
