using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Blox_Bros_Mm_Api.Models
{
    /// <summary>
    /// Represents a queued player
    /// </summary>
    public class Player
    {
        #region Properties

        /// <summary>
        /// The minimum number of players that can be matched together
        /// </summary>
        public static int MinMatchSize { get; set; }

        /// <summary>
        /// The maximum number of players that can be matched together
        /// </summary>
        public static int MaxMatchSize { get; set; }

        /// <summary>
        /// The maximum number of seconds a player can be queued before the queue will prioritize the queue
        /// </summary>
        public static int MaxQueueTime { get; set; }

        /// <summary>
        /// List of all <see cref="Player"/> objects
        /// </summary>
        public static readonly List<Player> Players = new List<Player>();

        /// <summary>
        /// The player's userId
        /// </summary>
        public long UserId { get; }

        /// <summary>
        /// The player's match <see cref="Server"/>, set when a match is found
        /// </summary>
        public Server Server { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates a new <see cref="Player"/> with the given parameters
        /// </summary>
        /// <param name="pUserId"></param>
        public Player(long pUserId)
        {
            UserId = pUserId;

            Players.Add(this);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Asynchronously searches for a match for the <see cref="Player"/>
        /// </summary>
        /// <returns></returns>
        public async Task<Player> FindMatch()
        {
            System.Diagnostics.Debug.WriteLine("Searching for match for " + UserId);

            var startTime = DateTime.Now;

            // Keep searching every second until Server is not null or Player is no longer queued
            while (this.Server == null && Players.Contains(this))
            {
                double elapsedTime = (DateTime.Now - startTime).TotalSeconds;
                
                // Lock threads on Player list, making this method thread-safe
                lock(Players)
                {
                    var possibleQueue = new List<Player>();

                    foreach (var player in Players)
                    {
                        if (player != this && player.Server == null)
                        {
                            possibleQueue.Add(player);
                        }

                        if (possibleQueue.Count == MaxMatchSize)
                            break;
                    }

                    possibleQueue.Add(this);

                    if (possibleQueue.Count == MaxMatchSize || (elapsedTime > MaxQueueTime && possibleQueue.Count >= MinMatchSize))
                    {
                        var reservedServer = new Server();

                        foreach (var player in possibleQueue)
                            player.Server = reservedServer;
                    }
                }

                await Task.Delay(1000);
            }

            if (this.Server != null)
                System.Diagnostics.Debug.WriteLine("Found match for " + UserId + " in " + (int)(DateTime.Now - startTime).TotalSeconds + " seconds");

            return this;
        }

        /// <summary>
        /// Disposes the <see cref="Player"/> and removes it from the <see cref="Player"/> list
        /// </summary>
        public void Delete()
        {
            Players.Remove(this);
        }

        #endregion
    }
}
