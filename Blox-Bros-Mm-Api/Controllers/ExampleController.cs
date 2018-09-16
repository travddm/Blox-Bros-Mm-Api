using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Blox_Bros_Mm_Api.Controllers
{
    /// <summary>
    /// Example endpoints
    /// </summary>
    [Route("example")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        #region Public Methods

        /// <summary>
        /// Gets an array of example values
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }
        
        /// <summary>
        /// Gets a specific example value by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        #endregion
    }
}
