using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Blox_Bros_Mm_Api.Attributes
{
    /// <summary>
    /// Requires the executing <see cref="Action"/>'s <see cref="ActionExecutingContext"/>.HttpContext.Request
    /// headers to include an X-Api-Key that matches the appsettings.json's "GlobalApiKey" property. Otherwise, it aborts the request and gives the context a <see cref="UnauthorizedResult"/>.
    /// </summary>
    public class ApiAuthorize : ActionFilterAttribute
    {
        #region Properties

        private static string GlobalApiKey;

        #endregion

        #region ActionFilterAttribute Overrides

        /// <summary>
        /// Custom <see cref="ActionFilterAttribute.OnActionExecuting(ActionExecutingContext)"/> implementation
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request != null)
            {
                var apiKey = context.HttpContext.Request.Headers["X-Api-Key"].First();

                if (apiKey != GlobalApiKey)
                    context.Result = new UnauthorizedResult();
                else
                    base.OnActionExecuting(context);
            }
            else
                base.OnActionExecuting(context);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets the <see cref="GlobalApiKey"/> for Api Authorization
        /// </summary>
        /// <param name="pGlobalApiKey"></param>
        public static void SetGlobalApiKey(string pGlobalApiKey)
        {
            GlobalApiKey = pGlobalApiKey;
        }

        #endregion

        #region Nested Classes

        /// <summary>
        /// <see cref="IOperationFilter"/> implementation used to add the X-Api-Key header parameter to any
        /// requests that are decorated with the <see cref="ApiAuthorize"/> attribute.
        /// </summary>
        public class AddSwaggerParameter : IOperationFilter
        {
            /// <summary>
            /// Custom <see cref="IOperationFilter.Apply(Operation, OperationFilterContext)"/> implementation.
            /// </summary>
            /// <param name="operation"></param>
            /// <param name="context"></param>
            public void Apply(Operation operation, OperationFilterContext context)
            {
                // Check if the method is decorated with any ApiAuthorize attributes
                if (context.MethodInfo.GetCustomAttributes(typeof(ApiAuthorize), false).Any())
                {
                    if (operation.Parameters == null)
                        operation.Parameters = new List<IParameter>();

                    operation.Parameters.Add(new NonBodyParameter
                    {
                        Name = "X-Api-Key",
                        In = "header",
                        Type = "string",
                        Required = true
                    });
                }
            }
        }

        #endregion
    }
}
