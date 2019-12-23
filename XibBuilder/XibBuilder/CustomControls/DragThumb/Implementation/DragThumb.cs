using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows;

namespace XibBuilder
{
    /// <summary>
    /// It provides the dragging of a control on canvas
    /// Shows a semi transparent border around control for dragging purpose
    /// </summary>
    public class DragThumb : Thumb
    {
        static DragThumb()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(DragThumb), new FrameworkPropertyMetadata(typeof(DragThumb)));
        }


        public DragThumb()
        {
            base.DragDelta += new DragDeltaEventHandler(DragThumb_DragDelta);
        }

        /// Bounded
        //void DragThumb_DragDelta(object sender, DragDeltaEventArgs e)
        //{
        //    DesignerItem designerItem = this.DataContext as DesignerItem;
        //    DesignerCanvas designer = VisualTreeHelper.GetParent(designerItem) as DesignerCanvas;
        //    if (designerItem != null && designer != null && designerItem.IsSelected)
        //    {
        //        double minLeft = double.MaxValue;
        //        double minTop = double.MaxValue;
        //        double maxRight = 0;
        //        double maxBottom = 0;

        //        // we only move DesignerItems
        //        var designerItems = designer.SelectionService.CurrentSelection.OfType<DesignerItem>();

        //        foreach (DesignerItem item in designerItems)
        //        {
        //            double left = Canvas.GetLeft(item);
        //            double top = Canvas.GetTop(item);

        //            minLeft = double.IsNaN(left) ? 0 : Math.Min(left, minLeft);
        //            minTop = double.IsNaN(top) ? 0 : Math.Min(top, minTop);

        //            maxRight = double.IsNaN(left) ? 0 : Math.Max(left + item.Width, maxRight);
        //            maxBottom = double.IsNaN(top) ? 0 : Math.Max(top + item.Height, maxBottom);
        //        }
                
        //        double deltaHorizontal = Math.Max(-minLeft, e.HorizontalChange);
        //        double deltaVertical = Math.Max(-minTop, e.VerticalChange);

        //        double d1 = maxRight;
        //        double d2 = maxBottom;

        //        if (maxRight + deltaHorizontal >= designer.ActualWidth)
        //            deltaHorizontal = 0;

        //        if (maxBottom + deltaVertical >= designer.ActualHeight)
        //            deltaVertical = 0;
                
        //        foreach (DesignerItem item in designerItems)
        //        {
        //            double left = Canvas.GetLeft(item);
        //            double top = Canvas.GetTop(item);

        //            if (double.IsNaN(left)) left = 0;
        //            if (double.IsNaN(top)) top = 0;
                                       
        //            Canvas.SetLeft(item, left + deltaHorizontal);
        //            Canvas.SetTop(item, top + deltaVertical);
        //        }

        //        designer.InvalidateMeasure();
        //        e.Handled = true;
        //    }
        //}


        void DragThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (this.DataContext is DesignerItem)
            {
                DesignerItem designerItem = this.DataContext as DesignerItem;

                DesignerCanvas designer = VisualTreeHelper.GetParent(designerItem) as DesignerCanvas;
                if (designerItem != null && designer != null && designerItem.IsSelected)
                {
                    // we only move DesignerItems
                    var designerItems = designer.SelectionService.CurrentSelection.OfType<DesignerItem>();

                    double deltaHorizontal = e.HorizontalChange;
                    double deltaVertical = e.VerticalChange;

                    foreach (DesignerItem item in designerItems)
                    {
                        double left = Canvas.GetLeft(item);
                        double top = Canvas.GetTop(item);

                        if (double.IsNaN(left)) left = 0;
                        if (double.IsNaN(top)) top = 0;

                        Canvas.SetLeft(item, left + deltaHorizontal);
                        Canvas.SetTop(item, top + deltaVertical);
                    }

                    designer.InvalidateMeasure();
                    e.Handled = true;
                }
            }

            if (this.DataContext is DesignerControl)
            {
                DesignerControl designerItem = this.DataContext as DesignerControl;

                DesignerCanvas designer = VisualTreeHelper.GetParent(designerItem) as DesignerCanvas;
                if (designerItem != null && designer != null && designerItem.IsSelected)
                {
                    // we only move DesignerItems
                    var designerItems = designer.SelectionService.CurrentSelection.OfType<DesignerControl>();

                    double deltaHorizontal = e.HorizontalChange;
                    double deltaVertical = e.VerticalChange;

                    foreach (DesignerControl item in designerItems)
                    {
                        double left = Canvas.GetLeft(item);
                        double top = Canvas.GetTop(item);

                        if (double.IsNaN(left)) left = 0;
                        if (double.IsNaN(top)) top = 0;

                        Canvas.SetLeft(item, left + deltaHorizontal);
                        Canvas.SetTop(item, top + deltaVertical);
                    }

                    designer.InvalidateMeasure();
                    e.Handled = true;
                }
            }
        }
    }
}
