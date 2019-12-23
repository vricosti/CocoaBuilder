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
    public class SMButton : Button
    {
        static SMButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SMButton), new FrameworkPropertyMetadata(typeof(SMButton)));
        }      

        #region dependency properties

        #region StyleType

        public static readonly DependencyProperty StyleTypeProperty =
            DependencyProperty.Register(
                "StyleType", typeof(string), typeof(SMButton), new PropertyMetadata(string.Empty, new PropertyChangedCallback(StyleChanged)));

        public string StyleType
        {
            get { return (string)GetValue(StyleTypeProperty); }
            set { SetValue(StyleTypeProperty, value); }
        }

        /// <summary>
        /// Callback function called when TextFontSize dependency property is changed
        /// </summary>
        static void StyleChanged(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            SMButton button = property as SMButton;
            //Style st = Application.Current.TryFindResource(args.NewValue.ToString()) as Style; 
            //if (button.Style == null)
            //    return;

            Style st = button.TryFindResource(args.NewValue.ToString()) as Style;

            if (st != null)
                button.Style = st;            
        }

        #endregion

        #region IsBordered

        public static readonly DependencyProperty IsBorderedProperty =
            DependencyProperty.Register(
                "IsBordered", typeof(bool), typeof(SMButton), new PropertyMetadata(false));

        public bool IsBordered
        {
            get { return (bool)GetValue(IsBorderedProperty); }
            set { SetValue(IsBorderedProperty, value); }
        }

        #endregion
        
        #endregion
    }
}
