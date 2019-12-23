using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Documents;

namespace XibBuilder
{
    /// <summary>
    /// It provides the resizing of a control on canvas    
    /// </summary>
    public class ResizeThumb : Thumb
    {
        static ResizeThumb()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ResizeThumb), new FrameworkPropertyMetadata(typeof(ResizeThumb)));
        }

        private DesignerItem _designerItem;
        private DesignerCanvas _canvas;
        private Adorner _adorner;
        
        public ResizeThumb()
        {
            DragStarted += new DragStartedEventHandler(ResizeThumb_DragStarted);
            DragDelta += new DragDeltaEventHandler(ResizeThumb_DragDelta);            
            DragCompleted += new DragCompletedEventHandler(this.ResizeThumb_DragCompleted);
        }

        void ResizeThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            this._designerItem = this.DataContext as DesignerItem;

            if (this._designerItem != null)
            {
                this._canvas = VisualTreeHelper.GetParent(this._designerItem) as DesignerCanvas;

                if (this._canvas != null)
                {
                    AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this._canvas);
                    if (adornerLayer != null)
                    {
                        this._adorner = new SizeAdorner(this._designerItem);
                        adornerLayer.Add(this._adorner);
                    }
                }
            }     
        }

        void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (this._designerItem != null)
            {
                double deltaVertical, deltaHorizontal;

                //double minLeft, minTop, minDeltaHorizontal, minDeltaVertical;
                //CalculateDragLimits(this._designerItem, out minLeft, out minTop, out minDeltaHorizontal, out minDeltaVertical);

                switch (VerticalAlignment)
                {
                    case System.Windows.VerticalAlignment.Bottom:
                        deltaVertical = Math.Min(-e.VerticalChange, this._designerItem.ActualHeight - this._designerItem.MinHeight);                        

                        this._designerItem.Height -= deltaVertical;
                        (_designerItem.Content as FrameworkElement).Height -= deltaVertical;
                        break;
                    case System.Windows.VerticalAlignment.Top:
                        deltaVertical = Math.Min(e.VerticalChange, this._designerItem.ActualHeight - this._designerItem.MinHeight);                        

                        Canvas.SetTop(this._designerItem, Canvas.GetTop(this._designerItem) + deltaVertical);                        

                        this._designerItem.Height -= deltaVertical;
                        (_designerItem.Content as FrameworkElement).Height -= deltaVertical;
                        break;
                    default:
                        break;
                }

                switch (HorizontalAlignment)
                {
                    case System.Windows.HorizontalAlignment.Left:
                        deltaHorizontal = Math.Min(e.HorizontalChange, this._designerItem.ActualWidth - this._designerItem.MinWidth);
                                                
                        Canvas.SetLeft(this._designerItem, Canvas.GetLeft(this._designerItem) + deltaHorizontal);

                        _designerItem.Width -= deltaHorizontal;
                        (_designerItem.Content as FrameworkElement).Width -= deltaHorizontal;
                        break;
                    case System.Windows.HorizontalAlignment.Right:
                        deltaHorizontal = Math.Min(-e.HorizontalChange, this._designerItem.ActualWidth - this._designerItem.MinWidth);                        

                        _designerItem.Width -= deltaHorizontal;
                        (_designerItem.Content as FrameworkElement).Width -= deltaHorizontal;
                        break;
                    default:
                        break;
                }
            }


            e.Handled = true;
        }

        void ResizeThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (this._adorner != null)
            {
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this._canvas);
                if (adornerLayer != null)
                {
                    adornerLayer.Remove(this._adorner);
                }

                this._adorner = null;
            }
        }

        #region Helper methods

        void CalculateDragLimits(DesignerItem selectedItem, out double minLeft, out double minTop, out double minDeltaHorizontal, out double minDeltaVertical)
        {
            minLeft = double.MaxValue;
            minTop = double.MaxValue;
            minDeltaHorizontal = double.MaxValue;
            minDeltaVertical = double.MaxValue;

            // drag limits are set by these parameters: canvas top, canvas left, minHeight, minWidth
            // calculate min value for each parameter for each item
            double left = Canvas.GetLeft(selectedItem);
            double top = Canvas.GetTop(selectedItem);

            minLeft = double.IsNaN(left) ? 0 : Math.Min(left, minLeft);
            minTop = double.IsNaN(top) ? 0 : Math.Min(top, minTop);

            minDeltaVertical = Math.Min(minDeltaVertical, selectedItem.ActualHeight - selectedItem.MinHeight);
            minDeltaHorizontal = Math.Min(minDeltaHorizontal, selectedItem.ActualWidth - selectedItem.MinWidth);
        }

        #endregion
    }
}
