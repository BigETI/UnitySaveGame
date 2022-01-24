/// <summary>
/// Unity save game namespace
/// </summary>
namespace UnitySaveGame
{
    /// <summary>
    /// An interface that represents a save game
    /// </summary>
    public interface ISaveGame<T> : IBaseSaveGame where T : ISaveGameData
    {
        /// <summary>
        /// Save game data
        /// </summary>
        T Data { get; }

        /// <summary>
        /// Backup save game data
        /// </summary>
        T BackupData { get; }
    }
}
