using System;

/// <summary>
/// Unity save game namespace
/// </summary>
namespace UnitySaveGame
{
    /// <summary>
    /// An interface that represents save game data
    /// </summary>
    public interface ISaveGameData
    {
        /// <summary>
        /// Last save date and time
        /// </summary>
        DateTime LastSaveDateTime { get; set; }

        /// <summary>
        /// Updates last save date and time
        /// </summary>
        void UpdateLastSaveDateTime();
    }
}
