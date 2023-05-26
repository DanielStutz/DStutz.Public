using DStutz.Data.Cruders;
using DStutz.System.Exceptions;

using Microsoft.AspNetCore.Mvc;

namespace DStutz.Apps.Controllers.API
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
        public IActionResult GetIdentity()
        {
            Log("GetIdentity()");

            return Ok(new APIResponse200(Identity));
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
        #endregion

        #region Methods - Create / POST
        /***********************************************************/
        protected async Task<ActionResult> PostAsync<P>(
            ICruderBLO<P> cruder,
            P entity)
        {
            try
            {
                Log("Post", cruder, "entity");

                return Ok(new APIResponse201(
                    await cruder.Create(entity)));
            }
            catch (ValidationException ex)
            {
                return BadRequest(new APIResponse400(ex));
            }
            catch (Exception ex)
            {
                return Error500(ex);
            }
        }
        #endregion

        #region Methods - Read / GET
        /***********************************************************/
        protected async Task<ActionResult> GetAllAsync<P>(
            ICruderBLO<P> cruder,
            int includeType)
        {
            try
            {
                Log("Get", cruder);

                return Ok(new APIResponse200(
                    await cruder.ReadAll(includeType)));
            }
            catch (Exception ex)
            {
                return Error500(ex);
            }
        }

        protected async Task<ActionResult> GetAsync<P>(
            ICruderBLO<P> cruder,
            int includeType,
            params object[] primaryKeys)
        {
            try
            {
                Log("Get", cruder, primaryKeys);

                return Ok(new APIResponse200(
                    await cruder.ReadOrThrow(primaryKeys, includeType)));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new APIResponse404(ex.Message));
            }
            catch (Exception ex)
            {
                return Error500(ex);
            }
        }
        #endregion

        #region Methods - Update / PUT
        /***********************************************************/
        protected async Task<ActionResult> DeleteCreateAsync<P>(
            ICruderBLO<P> cruder,
            P entity,
            params object[] primaryKeys)
        {
            try
            {
                Log("DeleteCreate", cruder, "entity");

                return Ok(new APIResponse200(
                    await cruder.DeleteCreate(primaryKeys, entity)));
            }
            catch (ValidationException ex)
            {
                return BadRequest(new APIResponse400(ex));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new APIResponse404(ex.Message));
            }
            catch (Exception ex)
            {
                return Error500(ex);
            }
        }

        protected async Task<ActionResult> PutAsync<P>(
            ICruderBLO<P> cruder,
            P entity)
        {
            try
            {
                Log("Put", cruder, "entity");

                return Ok(new APIResponse200(
                    await cruder.Update(entity)));
            }
            catch (ValidationException ex)
            {
                return BadRequest(new APIResponse400(ex));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new APIResponse404(ex.Message));
            }
            catch (Exception ex)
            {
                return Error500(ex);
            }
        }
        #endregion

        #region Methods - Delete / DELETE
        /***********************************************************/
        protected async Task<ActionResult> DeleteAsync<P>(
            ICruderBLO<P> cruder,
            params object[] primaryKeys)
        {
            try
            {
                Log("Delete", cruder, primaryKeys);

                var number = await
                    cruder.Delete(primaryKeys);

                return Ok(new APIResponse200(
                    "Deleted", cruder, primaryKeys));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new APIResponse404(ex.Message));
            }
            catch (Exception ex)
            {
                return Error500(ex);
            }
        }
        #endregion

        #region Methods - Send error 500
        /***********************************************************/
        private BadRequestObjectResult Error500(
            Exception ex)
        {
            Console.WriteLine(ex.ToString());

            // TODO Find another method ?!
            return BadRequest(new APIResponse500(ex.Message));
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        protected void Log(
            string method,
            ICruderDAO cruder,
            params object[] parameters)
        {
            Logger.LogInformation(
                "Method: {0}{1}({2})",
                method,
                cruder.Name,
                string.Join(", ", parameters));
        }

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
