using System.Collections.Generic;
using UnityEngine;
using UnitySaveGame.Data;

/// <summary>
/// Unity save game namespace
/// </summary>
namespace UnitySaveGame
{
    /// <summary>
    /// A class that contains functions for managing save games
    /// </summary>
    public static class SaveGames
    {
        /// <summary>
        /// Save games
        /// </summary>
        private static readonly Dictionary<string, IBaseSaveGame> saveGames = new Dictionary<string, IBaseSaveGame>();

        /// <summary>
        /// Save game path
        /// </summary>
        private static string saveGamePath;

        /// <summary>
        /// Default save game path
        /// </summary>
        public static string DefaultSaveGamePath => saveGamePath ??= $"{ Application.persistentDataPath }/save-game.json";

        /// <summary>
        /// Gets save game
        /// </summary>
        /// <typeparam name="T">Save game data type</typeparam>
        /// <returns>Save game if successful, otherwise "null"</returns>
        public static SaveGame<T> Get<T>() where T : ASaveGameData
        {
            SaveGame<T> ret;
            string key = typeof(T).FullName;
            if (saveGames.TryGetValue(key, out IBaseSaveGame save_game))
            {
                ret = (SaveGame<T>)save_game;
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
