using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Documents;
using System.Windows.Controls;

namespace Xceed.Wpf.Toolkit
{
    public class AdornedPlaceholder : FrameworkElement
    {
        public Adorner Adorner
        {
            get
            {
                Visual current = this;
                while (current != null && !(current is Adorner))
                {
                    current = (Visual)VisualTreeHelper.GetParent(current);
                }

                return (Adorner)current;
            }
        }

        public FrameworkElement AdornedElement
        {
            get
            {
                return Adorner == null ? null : Adorner.AdornedElement as FrameworkElement;
            }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var controlAdorner = Adorner as ControlAdorner;
            if (controlAdorner != null)
            {
                //controlAdorner.Placeholder = this;
            }

            if (controlAdorner.ActualWidth != 0 && controlAdorner.ActualHeight != 0)
                return new Size(controlAdorner.ActualWidth, controlAdorner.ActualHeight);
            else
                return new Size(0, 0);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Button btn = GetTemplateChild("PART_ButtonDone") as Button;
        }
    }
}
