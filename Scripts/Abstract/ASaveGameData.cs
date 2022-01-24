using System;
using UnityEngine;

/// <summary>
/// Unity save game data namespace
/// </summary>
namespace UnitySaveGame.Data
{
    /// <summary>
    /// An abstract class that describes save game data
    /// </summary>
    [Serializable]
    public abstract class ASaveGameData : ISaveGameData
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
                lastSaveDateTime ??= new DateTimeData(DateTime.Now);
                return lastSaveDateTime.DateTime;
            }
            set => lastSaveDateTime = new DateTimeData(value);
        }

        /// <summary>
        /// Constructs save game data
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
        /// Updates last save date and time
        /// </summary>
        public void UpdateLastSaveDateTime() => lastSaveDateTime = new DateTimeData(DateTime.Now);
    }
}
