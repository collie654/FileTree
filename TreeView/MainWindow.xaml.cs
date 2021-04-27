using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace TreeView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region On Loaded

        /// <summary>
        /// When the application first opens
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // get every lofical drive on the machine
            foreach (var drive in Directory.GetLogicalDrives())
            {
                // create a new item for it
                var item = new TreeViewItem()
                {
                    // set the header
                    Header = drive,
                    // and the full path
                    Tag = drive
                };

                // add a dummy item
                item.Items.Add(null);

                // listen out for item being expanded
                item.Expanded += Folder_Expanded; 

                // add the item to the tree view
                FolderView.Items.Add(item);
            }
        }
        #endregion

        #region Folder Expanded
        /// <summary>
        /// When a folder is expanded, find the sub folders/files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            #region Initial Checks
            var item = (TreeViewItem)sender;

            // if the item only contains the dummy data
            if (item.Items.Count != 1 || item.Items[0] != null)
                return;

            // clear dummy data
            item.Items.Clear();

            var fullPath = (string)item.Tag;
            #endregion

            #region Get Folders
            // create a blank list for directories
            var directories = new List<String>();

            // try and get directories from the folder
            // ignoring any issues doing so
            try
            {

                var dirs = Directory.GetDirectories(fullPath);

                if (dirs.Length > 0)
                    directories.AddRange(dirs);
            }
            catch { }

            // for each directory
            directories.ForEach(directoryPath =>
            {
                // create directory item
                var subItem = new TreeViewItem()
                {
                    // set header as folder name
                    Header = GetFileFolderName(directoryPath),
                    
                    // and tag as full path
                    Tag = directoryPath
                };

                // add dummy item for the expansion tab
                subItem.Items.Add(null);

                // handle expanding
                subItem.Expanded += Folder_Expanded;

                // add this item to the parent
                item.Items.Add(subItem);
            });

            #endregion

            #region Get Files
            // create a blank list for directories
            var files = new List<String>();

            // try and get directories from the folder
            // ignoring any issues doing so
            try
            {

                var fs = Directory.GetFiles(fullPath);

                if (fs.Length > 0)
                    files.AddRange(fs);
            }
            catch { }

            // for each directory
            directories.ForEach(filePath =>
            {
                // create file item
                var subItem = new TreeViewItem()
                {
                    // set header as file name
                    Header = GetFileFolderName(filePath),

                    // and tag as full path
                    Tag = filePath
                };

                // add this item to the parent
                item.Items.Add(subItem);
            });
            #endregion
        }

        #endregion

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
