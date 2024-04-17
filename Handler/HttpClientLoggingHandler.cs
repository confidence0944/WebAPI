namespace WebAPI.Handler
{
    /// <summary>
    /// Http Client 日誌攔截器
    /// </summary>
    public class HttpClientLoggingHandler : DelegatingHandler
    {
        private readonly ILogger _logger;

        public HttpClientLoggingHandler(ILogger<HttpClientLoggingHandler> logger)
        {
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request != null && _logger.IsEnabled(LogLevel.Trace))
            {
                string requestString = request.Content == null ? "" : await request.Content.ReadAsStringAsync(cancellationToken);
                _logger.LogDebug($"Request url: {request.RequestUri} , method:{request.Method}, content:{requestString}");
            }

            var response = await base.SendAsync(request, cancellationToken);
            if (response != null && _logger.IsEnabled(LogLevel.Trace))
            {
                string respString = await response.Content.ReadAsStringAsync(cancellationToken);
                _logger.LogDebug($"Response url: {request.RequestUri} , content: {respString}");
            }

            return response ?? new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
        }
    }
}
