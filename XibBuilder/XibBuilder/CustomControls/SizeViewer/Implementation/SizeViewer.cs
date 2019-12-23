using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace XibBuilder
{
    /// <summary>
    /// Display current Width and Height while resizing the adorned control
    /// </summary>
    public class SizeViewer : Control
    {
        static SizeViewer()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(SizeViewer), new FrameworkPropertyMetadata(typeof(SizeViewer)));
        }
    }

    public class DoubleFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double d = (double)value;
            return Math.Round(d);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
