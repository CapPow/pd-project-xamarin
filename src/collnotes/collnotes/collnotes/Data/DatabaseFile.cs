﻿using System;
using System.IO;
using SQLite;
using Xamarin.Essentials;

namespace collnotes.Data
{
    /// <summary>
    /// Database file.
    /// </summary>
    public static class DatabaseFile
    {
        private static SQLiteConnection sqliteConnection = null;

        /// <summary>
        /// Sets the connection to the SQLite DB file.
        /// </summary>
        private static void SetConnection()
        {
            string filePath = CreateDBFilePath();
            sqliteConnection = new SQLiteConnection(filePath);
        }

        /// <summary>
        /// Gets the connection to the SQLite DB file.
        /// </summary>
        /// <returns>The connection.</returns>
        public static SQLiteConnection GetConnection()
        {
            if (sqliteConnection != null)
            {
                return sqliteConnection;
            }
            else
            {
                SetConnection();
                return sqliteConnection;
            }
        }

        // no args method
        // returns string with file path for the sqlite database
        // will be different on iOS vs Android
        private static string CreateDBFilePath()
        {
            var sqliteFilename = "collNotes_database.db3";
            string folderPath = "";
            if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                // we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
                // (they don't want non-user-generated data in Documents)
                folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Resources);
                // folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "..", "Library"); // Library folder instead
            }
            else
            {
                // Just use whatever directory SpecialFolder.Personal returns
                folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            }

            var path = Path.Combine(folderPath, sqliteFilename);
            return path;
        }
    }
}
