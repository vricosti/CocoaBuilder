using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace XibBuilder
{
    public class FrameworkElementExt
    {
        public static readonly DependencyProperty IsPressedProperty = DependencyProperty.RegisterAttached("IsPressed", typeof(bool),
        typeof(FrameworkElementExt), new PropertyMetadata(false));

        public static readonly DependencyProperty AttachIsPressedProperty = DependencyProperty.RegisterAttached("AttachIsPressed", typeof(bool), typeof(FrameworkElementExt), new PropertyMetadata(false, PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            FrameworkElement element = (FrameworkElement)depObj;
            if (element != null)
            {
                if ((bool)args.NewValue)
                {
                    element.MouseDown += new MouseButtonEventHandler(element_MouseDown);
                    element.MouseUp += new MouseButtonEventHandler(element_MouseUp);
                    element.MouseLeave += new MouseEventHandler(element_MouseLeave);
                }
                else
                {
                    element.MouseDown -= new MouseButtonEventHandler(element_MouseDown);
                    element.MouseUp -= new MouseButtonEventHandler(element_MouseUp);
                    element.MouseLeave -= new MouseEventHandler(element_MouseLeave);
                }
            }
        }

        static void element_MouseLeave(object sender, MouseEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;
            if (element != null)
            {
                element.SetValue(IsPressedProperty, false);
            }
        }
        static void element_MouseUp(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;
            if (element != null)
            {
                element.SetValue(IsPressedProperty, false);
            }
        }
        static void element_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;
            if (element != null)
            {
                element.SetValue(IsPressedProperty, true);
            }
        }
        public static bool GetIsPressed(UIElement element)
        {
            return (bool)element.GetValue(IsPressedProperty);
        }
        public static void SetIsPressed(UIElement element, bool val)
        {
            element.SetValue(IsPressedProperty, val);
        }
        public static bool GetAttachIsPressed(UIElement element)
        {
            return (bool)element.GetValue(AttachIsPressedProperty);
        }
        public static void SetAttachIsPressed(UIElement element, bool val)
        {
            element.SetValue(AttachIsPressedProperty, val);
        }
    }


    public static class MouseDownHelper
    {
        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached("IsEnabled",
        typeof(bool), typeof(MouseDownHelper), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnNotifyPropertyChanged)));

        public static void SetIsEnabled(UIElement element, bool value)
        {
            element.SetValue(IsEnabledProperty, value);
        }

        public static bool GetIsEnabled(UIElement element)
        {
            return (bool)element.GetValue(IsEnabledProperty);
        }

        private static void OnNotifyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as UIElement;
            if (element != null && e.NewValue != null)
            {
                if ((bool)e.NewValue)
                {
                    Register(element);
                }
                else
                {
                    UnRegister(element);
                }
            }
        }

        private static void Register(UIElement element)
        {
            element.MouseDown += element_MouseDown;
            element.MouseLeftButtonDown += element_MouseLeftButtonDown;
            element.MouseLeave += element_MouseLeave;
            element.MouseUp += element_MouseUp;
        }

        private static void UnRegister(UIElement element)
        {
            element.MouseDown -= element_MouseDown;
            element.MouseLeftButtonDown -= element_MouseLeftButtonDown;
            element.MouseLeave -= element_MouseLeave;
            element.MouseUp -= element_MouseUp;
        }

        private static void element_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var element = e.Source as UIElement;
            if (element != null)
            {
                SetIsMouseDown(element, true);
            }
        }

        private static void element_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var element = e.Source as UIElement;
            if (element != null)
            {
                SetIsMouseLeftButtonDown(element, true);
            }
        }

        private static void element_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var element = e.Source as UIElement;
            if (element != null)
            {
                SetIsMouseDown(element, false);
                SetIsMouseLeftButtonDown(element, false);
            }
        }

        private static void element_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var element = e.Source as UIElement;
            if (element != null)
            {
                SetIsMouseDown(element, false);
                SetIsMouseLeftButtonDown(element, false);
            }
        }

        internal static readonly DependencyPropertyKey IsMouseDownPropertyKey = DependencyProperty.RegisterAttachedReadOnly("IsMouseDown",
        typeof(bool), typeof(MouseDownHelper), new FrameworkPropertyMetadata(false));
        public static readonly DependencyProperty IsMouseDownProperty = IsMouseDownPropertyKey.DependencyProperty;

        internal static void SetIsMouseDown(UIElement element, bool value)
        {
            element.SetValue(IsMouseDownPropertyKey, value);
        }

        public static bool GetIsMouseDown(UIElement element)
        {
            return (bool)element.GetValue(IsMouseDownProperty);
        }

        internal static readonly DependencyPropertyKey IsMouseLeftButtonDownPropertyKey = DependencyProperty.RegisterAttachedReadOnly("IsMouseLeftButtonDown",
        typeof(bool), typeof(MouseDownHelper), new FrameworkPropertyMetadata(false));
        public static readonly DependencyProperty IsMouseLeftButtonDownProperty = IsMouseLeftButtonDownPropertyKey.DependencyProperty;

        internal static void SetIsMouseLeftButtonDown(UIElement element, bool value)
        {
            element.SetValue(IsMouseLeftButtonDownPropertyKey, value);
        }

        public static bool GetIsMouseLeftButtonDown(UIElement element)
        {
            return (bool)element.GetValue(IsMouseLeftButtonDownProperty);
        }
    }
}
