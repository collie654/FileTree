using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace TreeView
{
    /// <summary>
    /// converts a full path to a specific image type of a drive, folder, or folder
    /// </summary>
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // get the full path
            var path = (string)value;
            
            // if the path is null, ignore
            if (path == null)
                return null;

            // get the name of the item
            var name = MainWindow.GetFileFolderName(path);


            // by defualt we assume it's a file
            var image = "Images/file.png";

            // if the name is blank, we presume it's a drive as we cannot have a blank file or folder name
            if (string.IsNullOrEmpty(name))
                image = "Images/drive.png";
            // checks if it is a directory
            else if (new FileInfo(path).Attributes.HasFlag(FileAttributes.Directory))
                image = "Images/folder-closed.png";
                                            // if we're accessing as a URI, need this part of the string to access.
            return new BitmapImage(new Uri($"pack://application:,,,/{image}"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
