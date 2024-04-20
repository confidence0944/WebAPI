using System.Text;

namespace WebAPI
{
    public class ApiLoggingMiddleware
    {
        private readonly ILogger<ApiLoggingMiddleware> _logger;

        private readonly RequestDelegate _next;

        public ApiLoggingMiddleware(ILogger<ApiLoggingMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = await FormatRequest(context.Request);

            var originalBodyStream = context.Response.Body;

            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            var response = await FormatResponse(context.Response);

            if (context.Request.Path.HasValue && !context.Request.Path.ToString().StartsWith("/swagger"))
            {
                _logger.LogInformation("Request: \n{Request}", request);
                _logger.LogInformation("Response: \n{Response}", response);
            }

            await responseBody.CopyToAsync(originalBodyStream);
        }

        /// <summary>
        /// 格式化請求
        /// </summary>
        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableBuffering();
            var headers = FormatHeaders(request.Headers);
            var body = await new StreamReader(request.Body, Encoding.UTF8).ReadToEndAsync();
            request.Body.Position = 0;
            var ip = request.HttpContext.Connection.RemoteIpAddress?.ToString();

            return $"Method: {request.Method}\n" +
                   $"URL: {request.Scheme}://{request.Host}{request.Path}{request.QueryString}\n" +
                   $"Headers: \n{headers}" +
                   $"Body: {body}\n" +
                   $"IP: {ip}";
        }

        /// <summary>
        /// 格式化回應
        /// </summary>
        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var headers = FormatHeaders(response.Headers);
            var body = await new StreamReader(response.Body, Encoding.UTF8).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return $"Status code: {response.StatusCode}\n" +
                   $"Headers: \n{headers}" +
                   $"Body: {body}";
        }

        /// <summary>
        /// 格式化標頭
        /// </summary>
        private string FormatHeaders(IHeaderDictionary headers)
        {
            var formattedHeaders = new StringBuilder();
            foreach (var (key, value) in headers)
            {
                formattedHeaders.AppendLine($"\t{key}: {string.Join(",", value)}");
            }

            return formattedHeaders.ToString();
        }
    }
}