using System;
using System.Windows.Documents;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace SM.Toolkit
{
    public class ControlAdorner : Adorner
    {
        private Control _child;
        //public AdornedPlaceholder Placeholder { get; set; }
        public ToolbarEditor Placeholder { get; set; }

        public ControlAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return 1;
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index != 0) throw new ArgumentOutOfRangeException();
            return _child;
        }

        public Control Child
        {
            get { return _child; }
            set
            {
                if (_child != null)
                {
                    RemoveVisualChild(_child);
                }
                _child = value;
                if (_child != null)
                {
                    AddVisualChild(_child);
                }
            }
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Button btn = GetTemplateChild("PART_ButtonDone") as Button;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            _child.Measure(constraint);
            return _child.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {            
            _child.Arrange(new Rect(new Point(0, 0), finalSize));
            UpdateLocation();
            return new Size(_child.ActualWidth, _child.ActualHeight);
        }

        private void UpdateLocation()
        {
            double xOffset = (((FrameworkElement)AdornedElement).ActualWidth - _child.ActualWidth) / 2;
            this.Margin = new Thickness(xOffset, ((FrameworkElement)AdornedElement).ActualHeight, 0, 0);

            if (Placeholder == null) return;

            Transform t = (Transform)TransformToDescendant(Placeholder);
            if (t == Transform.Identity) return;
            var oldTransfor = RenderTransform;
            if (oldTransfor == null || oldTransfor == Transform.Identity)
            {
                RenderTransform = t;
            }
            else
            {
                TransformGroup g = new TransformGroup();
                g.Children.Add(oldTransfor);
                g.Children.Add(t);
                RenderTransform =
                    new MatrixTransform(g.Value);
            }
        }
    }
}