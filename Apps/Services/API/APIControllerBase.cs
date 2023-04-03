using Microsoft.AspNetCore.Mvc;

namespace DStutz.Apps.Services.API
{
    public interface IAPIControllerBase
    {
        public IAppContext Context { get; }
        public ILogger Logger { get; }
    }

    public abstract class APIControllerBase
        : ControllerBase, IAPIControllerBase
    {
        #region Properties
        /***********************************************************/
        public IAppContext Context { get; }
        public ILogger Logger { get; }
        private APIIdentity Identity { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        protected APIControllerBase(
            IAppContext context,
            string version)
        {
            Context = context;
            Logger = Context.CreateLogger(this);

            Identity = new APIIdentity()
            {
                Name = GetType().Name,
                Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                Version = version,
            };
        }
        #endregion

        #region Methods - Identity
        /***********************************************************/
        // GET: api/[controller]/Identity
        [HttpGet("Identity")]
        public APIResponse200<APIIdentity> GetIdentity()
        {
            Log("GetIdentity()");

            return new APIResponse200<APIIdentity>(Identity);
        }
        #endregion

        #region Methods - Preflight
        /***********************************************************/
        // OPTIONS: api/[controller]
        [HttpOptions]
        public IActionResult PreflightRoute()
        {
            Log("PreflightRoute()");

            return NoContent();
        }

        // OPTIONS: api/[controller]/5
        [HttpOptions("{id}")]
        public IActionResult PreflightRoute(
            long id)
        {
            Log($"PreflightRoute({id})");

            return NoContent();
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        protected void Log(
            string method,
            IFormCollection? collection = null)
        {
            Logger.LogInformation(
                "Method: {0}",
                method);

            Log(collection);
        }

        protected void Log(
            string method,
            int id,
            IFormCollection? collection = null)
        {
            Logger.LogInformation(
                "Method: {0} with id {1}",
                method,
                id);

            Log(collection);
        }

        private void Log(
            IFormCollection? collection = null)
        {
            if (collection != null)
                foreach (var item in collection)
                    Logger.LogInformation(
                        "--> {0,-20} {1}",
                        item.Key,
                        item.Value);
        }
        #endregion
    }
}
