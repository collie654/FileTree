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

            // create the view models from the data
            // Creates a collection of data (ObservableCollection) that gives notifications when the list changes out of the children being passed in. 
            // selects each drive from the List and creates a new DirectoryItemViewModel out of it, filling in its fullPath and Type
            // **** THIS IS WHAT FolderView IS BOUND TO ****
            // and hierarchially speaking, what the Setter (IsExpanded), HierarchialDataTemplate (Children), and the Image (Type) are bound to. 
            this.Items = new ObservableCollection<DirectoryItemViewModel>
                (children.Select(drive => new DirectoryItemViewModel(drive.FullPath, DirectoryItemType.Drive)));   
        }
        #endregion
    }
}
