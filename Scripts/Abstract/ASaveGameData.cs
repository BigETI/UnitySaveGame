using System;
using UnityEngine;
using UnitySaveGame.Data;

/// <summary>
/// Unity save game namespace
/// </summary>
namespace UnitySaveGame
{
    /// <summary>
    /// Save game data interface
    /// </summary>
    [Serializable]
    public abstract class ASaveGameData
    {
        /// <summary>
        /// Last save date and time
        /// </summary>
        [SerializeField]
        private DateTimeData lastSaveDateTime;

        /// <summary>
        /// Last save date and time
        /// </summary>
        public DateTime LastSaveDateTime
        {
            get
            {
                if (lastSaveDateTime == null)
                {
                    lastSaveDateTime = new DateTimeData(DateTime.Now);
                }
                return lastSaveDateTime.DateTime;
            }
            set => lastSaveDateTime = new DateTimeData(value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="saveGameData">Save game data</param>
        public ASaveGameData(ASaveGameData saveGameData)
        {
            if (saveGameData != null)
            {
                LastSaveDateTime = saveGameData.LastSaveDateTime;
            }
        }

        /// <summary>
        /// Update last save date and time
        /// </summary>
        public void UpdateLastSaveDateTime()
        {
            lastSaveDateTime = new DateTimeData(DateTime.Now);
        }
    }
}
