using DStutz.Data.Cruders;

using System.Collections;

namespace DStutz.Apps.Controllers.API
{
    public class APIResponse200 : APIResponse
    {
        public APIResponse200(
            string? internalMessage = null,
            string? externalMessage = null)
            : base(200,
                  "OK",
                  internalMessage,
                  externalMessage)
        { }

        public APIResponse200(
            object? data)
            : this("containing 1 entity")
        {
            Count = 1;
            Data = data;
        }

        public APIResponse200(
            ICollection data)
            : this($"containing {data.Count} entities")
        {
            Count = data.Count;
            Data = data;
        }

        public APIResponse200(
            string method,
            ICruderDAO cruder,
            params object[] parameters)
            : this(
                  $"{method} {cruder.Name} with primary key(s) {string.Join(", ", parameters)}")
        { }
    }

    public class APIResponse201 : APIResponse
    {
        public APIResponse201(
            string? internalMessage = null,
            string? externalMessage = null)
            : base(201,
                  "Created",
                  internalMessage,
                  externalMessage)
        { }

        public APIResponse201(
            object? data)
            : this("1 entity")
        {
            Count = 1;
            Data = data;
        }
    }

    public class APIResponse204 : APIResponse
    {
        public APIResponse204(
            string? internalMessage = null,
            string? externalMessage = null)
            : base(204,
                  "No content",
                  internalMessage,
                  externalMessage)
        { }
    }
}
