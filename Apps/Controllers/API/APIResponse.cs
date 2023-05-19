namespace DStutz.Apps.Controllers.API
{
    // See https://developer.mozilla.org/en-US/docs/Web/HTTP/Status
    public abstract class APIResponse
    {
        public string Version { get; set; } = "1.0";
        public int Status { get; set; }
        public string StatusText { get; set; }
        public bool Success { get; set; }
        public string? UserMessage { get; set; }
        public int? Count { get; set; }
        public object? Data { get; set; }

        public APIResponse(
            int status,
            string statusText,
            string? internalMessage = null,
            string? externalMessage = null)
        {
            Status = status;
            StatusText = statusText;

            if (internalMessage != null)
                StatusText += $" ({internalMessage})";

            UserMessage = externalMessage;

            if (status >= 500)
                Success = false; // Server error responses
            else if (status >= 400)
                Success = false; // Client error responses
            else if (status >= 300)
                Success = true;  // Redirection messages
            else if (status >= 200)
                Success = true;  // Successful responses
            else if (status >= 100)
                Success = true;  // Informational responses
            else
                Success = false;
        }
    }
}
