using System.Windows;

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

            // setting the data context to the DirectoryStructureViewModel();
            // the data context allows elements to inherit information from parent elements 
            this.DataContext = new DirectoryStructureViewModel();
        }
        #endregion
    }
}
