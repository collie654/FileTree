using System.Collections.ObjectModel;
using System.Linq;

namespace TreeView
{
    /// <summary>
    ///  the view model for the applications main directory view
    /// </summary>
    class DirectoryStructureViewModel : BaseViewModel
    {
        #region Public Properties
        /// <summary>
        /// a list of all directories on the machine
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Items { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// default constructor
        /// </summary>
        public DirectoryStructureViewModel()
        {
            // gets a List<DirectoryItem> of all the drives
            var children = DirectoryStructure.GetLogicalDrives();

            

            /* turns the List of DirectoryItem into a List of DirectoryItemViewModel
             * This is what FolderView is using as its source for the drives. After this is finished running completely, 
             * the drives show up on the window. 
             */
            this.Items = new ObservableCollection<DirectoryItemViewModel>
                (children.Select(drive => new DirectoryItemViewModel(drive.FullPath, DirectoryItemType.Drive)));   
        }
        #endregion
    }
}
