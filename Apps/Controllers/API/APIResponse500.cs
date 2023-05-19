namespace DStutz.Apps.Controllers.API
{
    public class APIResponse500 : APIResponse
    {
        public APIResponse500(
            string? internalMessage = null,
            string? externalMessage = null)
            : base(500,
                  "Internal Server Error",
                  internalMessage,
                  externalMessage)
        { }

        public APIResponse500(
            Exception ex)
            : this(ex.Message)
        { }
    }
}
