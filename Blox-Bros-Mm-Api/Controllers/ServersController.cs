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
    /// Endpoints for viewing and modifying server information
    /// </summary>
    [Route("servers")]
    [ApiController]
    public class ServersController : ControllerBase
    {
        #region Public Methods

        /// <summary>
        /// Gets an array of all server guids
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetServerGuidList()
        {
            List<string> guids = new List<string>();

            foreach(var server in Server.Servers)
            {
                guids.Add(server.Guid);
            }

            return Ok(guids);
        }

        /// <summary>
        /// Gets information about a specific server from the server list
        /// </summary>
        /// <returns></returns>
        [HttpGet("{guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetServer(string guid)
        {
            Server target = Server.Servers.Find(s => s.Guid == guid);

            if (target == null)
                return NotFound();

            return Ok(target);
        }

        /// <summary>
        /// Posts information about a specific server to the server list
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [ApiAuthorize]
        [HttpPost("{guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult PostServer(string guid, [FromBody] JObject data)
        {
            Server target = Server.Servers.Find(s => s.Guid == guid);
            
            int players = 0;
            string map = string.Empty;
            try
            {
                players = data.Value<int>("Players");
                map = data.Value<string>("Map");
            }
            catch (Exception)
            {
                return BadRequest();
            }

            if (target == null)
            {
                target = new Server(guid, players, map);
            }
            else
            {
                target.Map = map;
                target.Players = players;
            }

            return Ok();
        }

        /// <summary>
        /// Deletes a server from the server list
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult DeleteServer(string guid)
        {
            Server target = Server.Servers.Find(s => s.Guid == guid);

            if (target == null)
                return NotFound();
            else
                target.Delete();

            return Ok();
        }

        #endregion
    }
}
