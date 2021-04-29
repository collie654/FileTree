using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TreeView
{
    /// <summary>
    /// The type of directory item
    /// </summary>
    public enum DirectoryItemType
    {
        /// <summary>
        /// a logical drive
        /// </summary>
        Drive,
        /// <summary>
        /// a physical file
        /// </summary>
        File,
        /// <summary>
        /// a folder
        /// </summary>
        Folder
    }
}
