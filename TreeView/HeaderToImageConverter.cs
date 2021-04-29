using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace TreeView
{
    /// <summary>
    /// converts a full path to a specific image type of a drive, folder, or folder
    /// </summary>
    [ValueConversion(typeof(DirectoryItemType), typeof(BitmapImage))]
    public class HeaderToImageConverter : IValueConverter
    {
        // creating a new instance of this converter
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();

        /// <summary>
        /// Converts which image we will use based on the value of DirectoryItemType
        /// </summary>
        /// <param name="value"> The type of directory </param>
        /// <param name="targetType"></param>
        /// <param name="parameter"> null </param>
        /// <param name="culture"> US </param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            // by defualt we assume it's a file
            var image = "Images/file.png";
            
            switch ((DirectoryItemType)value)
            {
                // if the value is Drive
                case DirectoryItemType.Drive:
                    image = "Images/drive.png";
                    break;
                // if the value is Folder
                case DirectoryItemType.Folder:
                    image = "Images/folder-closed.png";
                    break;

            }      
            
            // if we're accessing as a URI, need this part of the string to access.
            return new BitmapImage(new Uri($"pack://application:,,,/{image}"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
