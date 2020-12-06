using System;
using System.Collections.Generic;
using System.IO;

namespace MusicPlayer.Droid.Helpers
{
    public static class AndroidStorageHelper
    {
        public static string[] GetDirectories()
        {
            var directoriesList = new List<string>();

            directoriesList.Add(Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryMusic));
            directoriesList.Add(Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDcim));
            directoriesList.Add(Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDocuments));
            directoriesList.Add(Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads));

            return directoriesList.ToArray();
        }

        public static string[] GetAllFilesFromDirectories(string[] directoriesPaths)
        {
            var filesArray = new List<string>();
            foreach (var path in directoriesPaths)
            {
                var t = GetFilesFromDirectory(path);
                filesArray.AddRange(t);
            }
            return filesArray.ToArray();
        }

        public static string[] GetFilesFromDirectory(string path)
        {
            try
            {
                var t = Directory.GetFiles(path);
                return t;
            }
            catch (Exception ex)
            {
                return Array.Empty<string>();
            }
        }
    }
}
