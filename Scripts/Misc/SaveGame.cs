using System;
using System.IO;
using UnityEngine;

/// <summary>
/// Unity save game namespace
/// </summary>
namespace UnitySaveGame
{
    /// <summary>
    /// Save game class
    /// </summary>
    /// <typeparam name="T">Save game data type</typeparam>
    public class SaveGame<T> : ISaveGame where T : ASaveGameData
    {
        /// <summary>
        /// Save game data
        /// </summary>
        private T data;

        /// <summary>
        /// Save game backup data
        /// </summary>
        private T backupData;

        /// <summary>
        /// Save game data
        /// </summary>
        public T Data
        {
            get
            {
                if (data == null)
                {
                    Load();
                    if (data == null)
                    {
                        try
                        {
                            data = (T)(Activator.CreateInstance(typeof(T)));
                            Save();
                        }
                        catch
                        {
                            Debug.LogWarning("It is recommended to implement a public default constructor for \"" + typeof(T).Name + "\".");
                            try
                            {
                                data = (T)(Activator.CreateInstance(typeof(T), new object[] { null }));
                                Save();
                            }
                            catch (Exception e)
                            {
                                Debug.LogError(e);
                            }
                        }
                    }
                }
                return data;
            }
        }

        /// <summary>
        /// Backup save game data
        /// </summary>
        public T BackupData
        {
            get
            {
                if (backupData == null)
                {
                    try
                    {
                        backupData = (T)(Activator.CreateInstance(typeof(T), Data));
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                    }
                }
                return backupData;
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        internal SaveGame()
        {
            // ...
        }

        /// <summary>
        /// Load save game
        /// </summary>
        /// <returns>"true" if successful, otherwise "false"</returns>
        public bool Load() => Load(SaveGames.DefaultSaveGamePath);

        /// <summary>
        /// Load save game
        /// </summary>
        /// <param name="path">Save game path</param>
        /// <returns>"true" if successful, otherwise "false"</returns>
        public bool Load(string path)
        {
            bool ret = false;
            if (path != null)
            {
                try
                {
                    if (File.Exists(path))
                    {
                        using (StreamReader reader = new StreamReader(File.Open(path, FileMode.Open)))
                        {
                            data = (T)(JsonUtility.FromJson(reader.ReadToEnd(), typeof(T)));
                            backupData = (T)(Activator.CreateInstance(typeof(T), data));
                            ret = true;
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
            return ret;
        }

        /// <summary>
        /// Save game
        /// </summary>
        /// <returns>"true" if successful, otherwise "false"</returns>
        public bool Save() => Save(SaveGames.DefaultSaveGamePath);

        /// <summary>
        /// Save game
        /// </summary>
        /// <param name="path">Save game path</param>
        /// <returns>"true" if successful, otherwise "false"</returns>
        public bool Save(string path)
        {
            bool ret = false;
            if ((data != null) && (path != null))
            {
                string backup_path = path + ".backup";
                try
                {
                    if (File.Exists(path))
                    {
                        File.Copy(path, backup_path, true);
                        File.Delete(path);
                    }
                    using (StreamWriter writer = new StreamWriter(File.Open(path, FileMode.Create)))
                    {
                        data.UpdateLastSaveDateTime();
                        writer.Write(JsonUtility.ToJson(data));
                        backupData = (T)(Activator.CreateInstance(typeof(T), data));
                        ret = true;
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                    try
                    {
                        if (File.Exists(backup_path))
                        {
                            File.Copy(backup_path, path, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError(ex.Message);
                    }
                }
                try
                {
                    if (File.Exists(backup_path))
                    {
                        File.Delete(backup_path);
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex.Message);
                }
            }
            return ret;
        }

        /// <summary>
        /// Restore save game data from backup save game data
        /// </summary>
        public void RestoreSaveGameDataFromBackupSaveGameData()
        {
            if (backupData != null)
            {
                data = (T)(Activator.CreateInstance(typeof(T), backupData));
            }
        }
    }
}
