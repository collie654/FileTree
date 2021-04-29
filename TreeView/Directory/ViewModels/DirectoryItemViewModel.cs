using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace TreeView
{
    /// <summary>
    /// a view model for each directory item
    /// </summary>
    class DirectoryItemViewModel : BaseViewModel
    {
        #region Public Properties
        /// <summary>
        /// The type of this item
        /// *** THIS IS WHAT 
        /// </summary>
        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// the name of the image we're going to use
        /// Essentially is saying: ImageName = if Type is Drive = "drive" 
        ///                                    else if Type is File = "file" 
        ///                                    else if DirectoryItemViewModel IsExpanded = "folder-open" 
        ///                                    else "folder-closed"
        /// </summary>
        public string ImageName => Type == DirectoryItemType.Drive ? "drive" : (Type == DirectoryItemType.File ? "file" : (IsExpanded ? "folder-open" : "folder-closed"));

        /// <summary>
        /// the full path to the item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// the name of this directory item
        /// **** THIS IS WHAT THE TextBox IS BOUND TO / RECEIVING  ****
        /// Essentially says Name returns: if Directory type is Drive, return the full path (Ex. C:\\) else return DirectoryStructure.GetFileFolderName(this.FullPath) (Ex. Recycle Bin)
        /// </summary>
        public string Name { get { return this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFileFolderName(this.FullPath); } }

        /// <summary>
        /// a list of all children contained inside this item
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Children { get; set; }

        /// <summary>
        /// indicates if this item can be expanded
        /// </summary>
        public bool CanExpand { get { return this.Type != DirectoryItemType.File; } }

        /// <summary>
        /// indicates if the current item is expanded or not
        /// </summary>
        public bool IsExpanded
        {
            // Initially nothing is expanded. When you click on the expand arrow, the UI fires this event through the binding, asking for the children.
            get 
            {
                return this.Children?.Count(f => f != null) > 0;
            }
            set
            {
                // if the UI tells us to expand.. AKA if you clicked the expand button
                if (value == true)
                    // find all children
                    Expand();
                // of the UI tells us to close.. AKA you closed the expand button
                else
                    this.ClearChildren();
            }
        }
        #endregion

        #region Public Commands

        /// <summary>
        /// commands to expand this item
        /// </summary>
        public ICommand ExpandCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="fullPath"> full path of this item</param>
        /// <param name="type">the type of item</param>
        public DirectoryItemViewModel(string fullPath, DirectoryItemType type)
        {
            // create commands
            this.ExpandCommand = new RelayCommand(Expand);

            //set path and type
            this.FullPath = fullPath;
            this.Type = type;

            // setup the children as needed
            this.ClearChildren();
        }

        #endregion

        #region Helper Methods
        /// <summary>
        /// removes all children from the list, adding a dummy item to show the expand icon if required
        /// </summary>
        private void ClearChildren()
        {
            // 
            this.Children = new ObservableCollection<DirectoryItemViewModel>();

            //show the expand arrow if we are not in a file
            if (this.Type != DirectoryItemType.File)
                this.Children.Add(null);
        }
        #endregion


        /// <summary>
        /// expands this directory and finds all children
        /// </summary>
        private void Expand() 
        {
            // cannot expand file
            if (this.Type == DirectoryItemType.File)
                return;

            // returns a List<DirectoryItem> of all the children of the drive.
            var children = DirectoryStructure.GetDirectoryContents(this.FullPath);

            // This is creating an ObservableColletion (which sends notifications when it's changed) out of the List<DiscoveryItem> children (looping through the list),
            // which are first being called and run through DirectoryItemViewModel.   
            this.Children = new ObservableCollection<DirectoryItemViewModel>(children.Select(content => new DirectoryItemViewModel(content.FullPath, content.Type)));
        }
    }
}
