using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blox_Bros_Mm_Api.Models
{
    /// <summary>
    /// Represents a server
    /// </summary>
    public class Server
    {
        #region Properties

        /// <summary>
        /// List of all <see cref="Server"/> objects
        /// </summary>
        public static readonly List<Server> Servers = new List<Server>();

        /// <summary>
        /// The server's Guid
        /// </summary>
        public string Guid { get; }

        /// <summary>
        /// The number of players in the server
        /// </summary>
        public int Players { get; set; }

        /// <summary>
        /// The server's current map name
        /// </summary>
        public string Map { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor; instantiates a new <see cref="Server"/>
        /// </summary>
        public Server()
        {
            // Keep generating random Guids until a server with the Guid doesn't exist
            string pGuid;
            do
            {
                pGuid = System.Guid.NewGuid().ToString();
            } while (Exists(pGuid));

            Guid = pGuid;

            Servers.Add(this);
        }

        /// <summary>
        /// Instantiates a new <see cref="Server"/> with the given parameters
        /// </summary>
        /// <param name="pGuid">The server's Guid</param>
        /// <param name="pPlayers">The number of players in the server</param>
        /// <param name="pMap">The server's current map name</param>
        public Server(string pGuid, int pPlayers, string pMap)
        {
            if (Exists(pGuid))
                throw new ArgumentException("A Server object with the given Guid already exists", "pGuid");

            Guid = pGuid;
            Players = pPlayers;
            Map = pMap;

            Servers.Add(this);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Disposes the <see cref="Server"/> and removes it from the <see cref="Server"/> list
        /// </summary>
        public void Delete()
        {
            Servers.Remove(this);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Checks if a server with the given Guid exists
        /// </summary>
        /// <param name="pGuid"></param>
        /// <returns></returns>
        private bool Exists(string pGuid)
        {
            return Servers.Exists(s => s.Guid == pGuid);
        }

        #endregion
    }
}
