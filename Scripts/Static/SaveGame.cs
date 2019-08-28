using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

/// <summary>
/// Unity save game namespace
/// </summary>
namespace UnitySaveGame
{
    /// <summary>
    /// Save game class
    /// </summary>
    public static class SaveGame
    {
        /// <summary>
        /// Save game path
        /// </summary>
        private static string saveGamePath;

        /// <summary>
        /// Save game data
        /// </summary>
        private static ASaveGameData saveGameData;

        /// <summary>
        /// Backup save game data
        /// </summary>
        private static ASaveGameData backupSaveGameData;

        /// <summary>
        /// Save game data type
        /// </summary>
        private static Type saveGameDataType;

        /// <summary>
        /// Save game data assembly
        /// </summary>
        private static Assembly saveGameDataAssembly;

        /// <summary>
        /// Save game path
        /// </summary>
        public static string SaveGamePath
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
        /// Save game data
        /// </summary>
        public static ASaveGameData SaveGameData
        {
            get
            {
                if ((saveGameData == null) && (SaveGameDataType != null))
                {
                    Load();
                    if (saveGameData == null)
                    {
                        try
                        {
                            saveGameData = (ASaveGameData)(Activator.CreateInstance(SaveGameDataType));
                            Save();
                        }
                        catch (Exception e)
                        {
                            Debug.LogError(e);
                        }
                    }
                }
                return saveGameData;
            }
        }

        /// <summary>
        /// Get save game data
        /// </summary>
        /// <typeparam name="T">Save game type</typeparam>
        /// <returns>Save game if successful, otherwise "null"</returns>
        public static T GetData<T>() where T : ASaveGameData => SaveGameData as T;

        /// <summary>
        /// Backup save game data
        /// </summary>
        public static ASaveGameData BackupSaveGameData
        {
            get
            {
                if ((backupSaveGameData == null) && (SaveGameDataType != null))
                {
                    try
                    {
                        backupSaveGameData = (ASaveGameData)(Activator.CreateInstance(SaveGameDataType, SaveGameData));
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                    }
                }
                return backupSaveGameData;
            }
        }

        /// <summary>
        /// Save game data type
        /// </summary>
        private static Type SaveGameDataType
        {
            get
            {
                if (saveGameDataAssembly == null)
                {
                    try
                    {
                        List<Type> types = new List<Type>();
                        saveGameDataAssembly = Assembly.GetExecutingAssembly();
                        if (saveGameDataAssembly != null)
                        {
                            foreach (Type type in saveGameDataAssembly.GetTypes())
                            {
                                if (type != null)
                                {
                                    if (type.IsClass && (!(type.IsAbstract)) && (!(type.IsInterface)) && typeof(ASaveGameData).IsAssignableFrom(type))
                                    {
                                        types.Add(type);
                                    }
                                }
                            }
                        }
                        if (types.Count <= 0)
                        {
                            Debug.LogError("Please implement a class inherited from \"" + typeof(ASaveGameData).FullName + "\"");
                        }
                        else if (types.Count > 1)
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append("There are too many classes inherited from \"");
                            sb.Append(typeof(ASaveGameData).FullName);
                            sb.AppendLine("\":");
                            foreach (Type type in types)
                            {
                                sb.Append("\t\"");
                                sb.Append(type.FullName);
                                sb.AppendLine("\"");
                            }
                            Debug.LogError(sb.ToString());
                        }
                        else
                        {
                            saveGameDataType = types[0];
                        }
                        types.Clear();
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                    }
                }
                return saveGameDataType;
            }
        }

        /// <summary>
        /// Load save game
        /// </summary>
        /// <returns>"true" if successful, otherwise "false"</returns>
        public static bool Load()
        {
            return Load(SaveGamePath);
        }

        /// <summary>
        /// Load save game
        /// </summary>
        /// <param name="path">Save game path</param>
        /// <returns>"true" if successful, otherwise "false"</returns>
        public static bool Load(string path)
        {
            bool ret = false;
            if ((path != null) && (SaveGameDataType != null))
            {
                try
                {
                    if (File.Exists(path))
                    {
                        using (StreamReader reader = new StreamReader(File.Open(path, FileMode.Open)))
                        {
                            saveGameData = (ASaveGameData)(JsonUtility.FromJson(reader.ReadToEnd(), SaveGameDataType));
                            backupSaveGameData = (ASaveGameData)(Activator.CreateInstance(SaveGameDataType, saveGameData));
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
        public static bool Save()
        {
            return Save(SaveGamePath);
        }

        /// <summary>
        /// Save game
        /// </summary>
        /// <param name="path">Save game path</param>
        /// <returns>"true" if successful, otherwise "false"</returns>
        public static bool Save(string path)
        {
            bool ret = false;
            if ((saveGameData != null) && (path != null) && (SaveGameDataType != null))
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
                        saveGameData.UpdateLastSaveDateTime();
                        writer.Write(JsonUtility.ToJson(saveGameData));
                        backupSaveGameData = (ASaveGameData)(Activator.CreateInstance(SaveGameDataType, saveGameData));
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
        public static void RestoreSaveGameDataFromBackupSaveGameData()
        {
            if (backupSaveGameData != null)
            {
                saveGameData = (ASaveGameData)(Activator.CreateInstance(SaveGameDataType, backupSaveGameData));
            }
        }
    }
}
