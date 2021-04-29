using System.ComponentModel;
using System.Threading.Tasks;

namespace TreeView
{
    /// <summary>
    /// a base view model that fires property changed events as needed
    /// </summary>

    class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// the event is fired when any child property chnages its value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => {};
    }
}
