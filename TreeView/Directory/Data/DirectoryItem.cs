/// <summary>
///  Information about a directory item such as a drive, file , or folder
/// </summary>
namespace TreeView
{
    public class DirectoryItem
    {
        /// <summary>
        /// The type of this item
        /// </summary>
        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// the absolute path to this item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// the name of this directory item
        /// </summary>
        public string Name { get { return this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFileFolderName(this.FullPath); } }
    }
}
