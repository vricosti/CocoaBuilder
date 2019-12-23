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
    ///     <MyNamespace:SMPanel/>
    ///
    /// </summary>
    
    public class SMPanel : Control
    {
        static SMPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SMPanel), new FrameworkPropertyMetadata(typeof(SMPanel)));
        }


        #region Caption

        public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register("Caption", typeof(object), typeof(SMPanel), new UIPropertyMetadata(String.Empty));
        public object Caption
        {
            get
            {
                return (object)GetValue(CaptionProperty);
            }
            set
            {
                SetValue(CaptionProperty, value);
            }
        }

        #endregion //Caption

        #region CaptionForeground

        public static readonly DependencyProperty CaptionForegroundProperty = DependencyProperty.Register("CaptionForeground", typeof(Brush), typeof(SMPanel), new UIPropertyMetadata(null));
        public Brush CaptionForeground
        {
            get
            {
                return (Brush)GetValue(CaptionForegroundProperty);
            }
            set
            {
                SetValue(CaptionForegroundProperty, value);
            }
        }

        #endregion //CaptionForeground


        #region Content

        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(object), typeof(SMPanel), new UIPropertyMetadata(null));
        public object Content
        {
            get
            {
                return (object)GetValue(ContentProperty);
            }
            set
            {
                SetValue(ContentProperty, value);
            }
        }

        #endregion //Caption
    }
}
