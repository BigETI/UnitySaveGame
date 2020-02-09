using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity save game namespace
/// </summary>
namespace UnitySaveGame
{
    /// <summary>
    /// Save games class
    /// </summary>
    public static class SaveGames
    {
        /// <summary>
        /// Save game path
        /// </summary>
        private static string saveGamePath;

        /// <summary>
        /// Save games
        /// </summary>
        private static Dictionary<string, ISaveGame> saveGames = new Dictionary<string, ISaveGame>();

        /// <summary>
        /// Default save game path
        /// </summary>
        public static string DefaultSaveGamePath
        {
            get
            {
                if (saveGamePath == null)
                {
                    saveGamePath = Application.persistentDataPath + "/save-game.json";
                }
                return saveGamePath;
            }
        }

        /// <summary>
        /// Get save game
        /// </summary>
        /// <typeparam name="T">Save game data type</typeparam>
        /// <returns>Save game if successful, otherwise "null"</returns>
        public static SaveGame<T> Get<T>() where T : ASaveGameData
        {
            SaveGame<T> ret;
            string key = typeof(T).FullName;
            if (saveGames.ContainsKey(key))
            {
                ret = (SaveGame<T>)(saveGames[key]);
            }
            else
            {
                ret = new SaveGame<T>();
                saveGames.Add(key, ret);
            }
            return ret;
        }
    }
}
