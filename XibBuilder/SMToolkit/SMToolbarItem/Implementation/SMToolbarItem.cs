using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SM.Toolkit
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:SM.Toolkit"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:SM.Toolkit;assembly=SM.Toolkit"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:MyImageButton/>
    ///
    /// </summary>
    public class SMToolbarItem : Control
    {
        static SMToolbarItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SMToolbarItem), new FrameworkPropertyMetadata(typeof(SMToolbarItem)));
        }

        #region properties

        public ImageSource NormalImage
        {
            get { return (ImageSource)GetValue(NormalImageProperty); }
            set { SetValue(NormalImageProperty, value); }
        }

        public ImageSource DisabledImage
        {
            get { return (ImageSource)GetValue(DisabledImageProperty); }
            set { SetValue(DisabledImageProperty, value); }
        }

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public string PaletteLabel
        {
            get { return (string)GetValue(PaletteLabelProperty); }
            set { SetValue(PaletteLabelProperty, value); }
        }

        public bool ShowCaption
        {
            get { return (bool)GetValue(ShowCaptionProperty); }
            set { SetValue(ShowCaptionProperty, value); }
        }

        #endregion

        public string ItemIdentifier { get; set; }

        #region dependency properties

        public static readonly DependencyProperty DisabledImageProperty =
            DependencyProperty.Register(
                "DisabledImage", typeof(ImageSource), typeof(SMToolbarItem));

        public static readonly DependencyProperty NormalImageProperty =
          DependencyProperty.Register(
              "NormalImage", typeof(ImageSource), typeof(SMToolbarItem));       

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register(
                "Label", typeof(string), typeof(SMToolbarItem));

        public static readonly DependencyProperty PaletteLabelProperty =
            DependencyProperty.Register(
                "PaletteLabel", typeof(string), typeof(SMToolbarItem));

        public static readonly DependencyProperty ShowCaptionProperty =
            DependencyProperty.Register(
                "ShowCaption", typeof(bool), typeof(SMToolbarItem), new PropertyMetadata(true));

        #endregion
    }
}
