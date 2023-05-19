using DStutz.Data.Cruders;

namespace DStutz.Apps.Controllers.API
{
    public class APIResponse400 : APIResponse
    {
        public APIResponse400(
            string? internalMessage = null,
            string? externalMessage = null)
            : base(400,
                  "Bad Request",
                  internalMessage,
                  externalMessage)
        { }

        public APIResponse400(
            ValidationException ex)
            : this(ex.Message)
        {
            Data = ex.Problems;
        }
    }

    public class APIResponse404 : APIResponse
    {
        public APIResponse404(
            string? internalMessage = null,
            string? externalMessage = null)
            : base(404,
                  "Not Found",
                  internalMessage,
                  externalMessage)
        { }
    }
}
