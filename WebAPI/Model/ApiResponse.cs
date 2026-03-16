using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WebAPI.Model
{
    public class ApiResponse
    {
        public ApiResponse() : this(string.Empty, string.Empty, null)
        {
        }

        public ApiResponse(MobileBankError mberror, dynamic data)
        {
            Code = mberror.Code;
            Message = mberror.Description;
            Data = data;
            Time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
        }

        public ApiResponse(string code, string message, dynamic data)
        {
            Code = code;
            Message = message;
            Data = data;
            Time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
        }

        //回應代碼
        [JsonPropertyName("ReturnCode")]
        public string Code { get; set; }

        //回應訊息
        [JsonPropertyName("ReturnMessage")]
        public string Message { get; set; }

        //回應時間
        [JsonPropertyName("ReturnTime")]
        public string Time { get; set; }

        //回應資料
        [JsonPropertyName("Data")]
        public dynamic Data { get; set; }
    }
}