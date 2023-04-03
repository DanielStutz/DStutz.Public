namespace DStutz.Apps.Services.API
{
    // See https://developer.mozilla.org/en-US/docs/Web/HTTP/Status
    public abstract class APIResponse
    {
        public string Version { get; set; } = "1.0";
        public int Status { get; set; }
        public string StatusText { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }

        public APIResponse(
            int status,
            string statusText,
            string? message = null)
        {
            Status = status;
            StatusText = statusText;
            Message = message;

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

    public class APIResponse200 : APIResponse
    {
        public APIResponse200()
            : base(200, "OK") { }
    }

    public class APIResponse201 : APIResponse
    {
        public APIResponse201()
            : base(201, "Created") { }
    }

    public class APIResponse400 : APIResponse
    {
        public APIResponse400()
            : base(400, "Bad Request") { }

        public APIResponse400(
            string message,
            string? messageUser = null)
            : base(400, "Bad Request (" + message + ")", messageUser) { }
    }

    public class APIResponse404 : APIResponse
    {
        public APIResponse404()
            : base(404, "Not Found") { }
    }
}
