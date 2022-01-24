/// <summary>
/// Unity save game namespace
/// </summary>
namespace UnitySaveGame
{
    /// <summary>
    /// An interface that represents a base save game
    /// </summary>
    public interface IBaseSaveGame
    {
        /// <summary>
        /// Loads save game
        /// </summary>
        /// <returns>"true" if successful, otherwise "false"</returns>
        bool Load();

        /// <summary>
        /// Loads save game
        /// </summary>
        /// <param name="path">Save game path</param>
        /// <returns>"true" if successful, otherwise "false"</returns>
        bool Load(string path);

        /// <summary>
        /// Saves game
        /// </summary>
        /// <returns>"true" if successful, otherwise "false"</returns>
        bool Save();

        /// <summary>
        /// Saves game
        /// </summary>
        /// <param name="path">Save game path</param>
        /// <returns>"true" if successful, otherwise "false"</returns>
        bool Save(string path);

        /// <summary>
        /// Restores save game data from backup save game data
        /// </summary>
        void RestoreSaveGameDataFromBackupSaveGameData();
    }
}
