using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace XibBuilder
{
    /// <summary>
    /// Display resize handles to resize the control
    /// </summary>
    public class DesignerControlResizeChrome : Control
    {
        static DesignerControlResizeChrome()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(DesignerControlResizeChrome), new FrameworkPropertyMetadata(typeof(DesignerControlResizeChrome)));
        }
    }
}
