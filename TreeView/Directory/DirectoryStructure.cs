using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// Helper class to query information about directories
/// </summary>
namespace TreeView
{
    /// <summary>
    /// gets all logical drives on the computer
    /// </summary>
    public static class DirectoryStructure
    {

        /// <summary>
        ///  returns a list of full List of DirectoryItems
        /// </summary>
        /// <returns></returns>
        public static List<DirectoryItem> GetLogicalDrives()
        {
            // returns a list of all the drives. 
            return Directory.GetLogicalDrives().Select(drive => new DirectoryItem { FullPath = drive, Type = DirectoryItemType.Drive }).ToList();
        }

        /// <summary>
        /// gets the directories top level content
        /// </summary>
        /// <param name="fullPath">full path to the directory</param>
        /// <returns></returns>
        public static List<DirectoryItem> GetDirectoryContents(string fullPath)
        {
            // create empty list
            var items = new List<DirectoryItem>();

            #region Get Folders
           

            // try and get folders from the drive
            // ignoring any issues doing so
            try
            {
                // This is getting the full path of all folders directly inside the drive
                var dirs = Directory.GetDirectories(fullPath);

                // if there are any folders inside the drive
                if (dirs.Length > 0)
                    // add each folder to the items list as DirectoryItems
                    items.AddRange(dirs.Select(dir => new DirectoryItem { FullPath = dir, Type = DirectoryItemType.Folder }));
            }
            catch { }

            #endregion

            #region Get Files
            
            // try and get files from the drive
            // ignoring any issues doing so
            try
            {
                // gets any files withing the drive
                var fs = Directory.GetFiles(fullPath);

                // if there are files
                if (fs.Length > 0)
                    // add them to the list as DirectoryItems
                    items.AddRange(fs.Select(file => new DirectoryItem { FullPath = file, Type = DirectoryItemType.File }));
            }
            catch { }

            #endregion
            // returns the List<DirectoryItem> to DirectoryItemViewModel (var children)
            return items;
        }



        #region Helpers

        /// <summary>
        /// find the file or folder name from a full path
        /// </summary>
        /// <param name="path"> the full path</param>
        /// <returns></returns>
        public static string GetFileFolderName(string path)
        {

            // if we have no path, return empty
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            // make all slashes backslashes
            var normalizedPath = path.Replace('/', '\\');

            // find the last backslash in the path
            var lastIndex = normalizedPath.LastIndexOf('\\');


            // if we dont find a backslash, return the path itself
            if (lastIndex <= 0)
                return path;

            //return the name after the last backslash
            return path.Substring(lastIndex + 1);

        }
        #endregion
    }
}
