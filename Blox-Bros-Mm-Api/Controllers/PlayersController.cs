using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Blox_Bros_Mm_Api.Attributes;
using Blox_Bros_Mm_Api.Models;

namespace Blox_Bros_Mm_Api.Controllers
{
    /// <summary>
    /// Endpoints for viewing and modifying player queue information
    /// </summary>
    [Route("players")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        #region Public Methods

        /// <summary>
        /// Gets an array of all player userIds
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetPlayerUserIdList()
        {
            return BadRequest(new NotImplementedException());
        }
        
        /// <summary>
        /// Gets information about a specific player from the player queue
        /// </summary>
        /// <returns></returns>
        [HttpGet("{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetPlayer()
        {
            return BadRequest(new NotImplementedException());
        }

        /// <summary>
        /// Posts information about a specific player to the player queue, and starts the queue if it's not already started
        /// </summary>
        /// <returns></returns>
        [ApiAuthorize]
        [HttpPost("{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult PostPlayer()
        {
            return BadRequest(new NotImplementedException());
        }

        /// <summary>
        /// Deletes a player from the player queue
        /// </summary>
        /// <returns></returns>
        [ApiAuthorize]
        [HttpDelete("{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult DeletePlayer()
        {
            return BadRequest(new NotImplementedException());
        }

        #endregion
    }
}
