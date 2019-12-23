using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml;
using System.Windows.Media;

namespace XibBuilder
{
    public partial class DesignerCanvas : Canvas
    {
        SelectionService selectionService;
        internal SelectionService SelectionService
        {
            get
            {
                if (selectionService == null)
                    selectionService = new SelectionService(this);

                return selectionService;
            }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Source == this)
            {
                SelectionService.ClearSelection();
                e.Handled = true;
            }
        }        

        protected override void OnDrop(DragEventArgs e)
        {
            //base.OnDrop(e);
            //DragObject dragObject = e.data.GetData(typeof(DragObject)) as DragObject;
            //if (dragObject != null && !String.IsNullOrEmpty(dragObject.Xaml))
            //{
            //    DesignerItem newItem = null;
            //    Object content = XamlReader.Load(XmlReader.create(new StringReader(dragObject.Xaml)));

            //    if (content != null)
            //    {
            //        newItem = new DesignerItem();
            //        newItem.Content = content;

            //        Point position = e.GetPosition(this);

            //        if (dragObject.DesiredSize.HasValue)
            //        {
            //            Size desiredSize = dragObject.DesiredSize.Value;
            //            newItem.Width = desiredSize.Width;
            //            newItem.Height = desiredSize.Height;

            //            DesignerCanvas.SetLeft(newItem, Math.Max(0, position.X - newItem.Width / 2));
            //            DesignerCanvas.SetTop(newItem, Math.Max(0, position.Y - newItem.Height / 2));
            //        }
            //        else
            //        {
            //            DesignerCanvas.SetLeft(newItem, Math.Max(0, position.X));
            //            DesignerCanvas.SetTop(newItem, Math.Max(0, position.Y));
            //        }

            //        Canvas.SetZIndex(newItem, this.Children.Count);
            //        this.Children.Add(newItem);                    
            //        SetConnectorDecoratorTemplate(newItem);

            //        //update selection
            //        this.SelectionService.SelectItem(newItem);
            //        newItem.Focus();
            //    }

            //    e.Handled = true;
            //}
        }

        #region MeasureOverride

        //protected override Size MeasureOverride(Size constraint)
        //{
        //    Size size = new Size();

        //    foreach (UIElement element in this.InternalChildren)
        //    {
        //        double left = Canvas.GetLeft(element);
        //        double top = Canvas.GetTop(element);
        //        left = double.IsNaN(left) ? 0 : left;
        //        top = double.IsNaN(top) ? 0 : top;

        //        //measure desired size for each child
        //        element.Measure(constraint);

        //        Size desiredSize = element.DesiredSize;
        //        if (!double.IsNaN(desiredSize.Width) && !double.IsNaN(desiredSize.Height))
        //        {
        //            size.Width = Math.Max(size.Width, left + desiredSize.Width);
        //            size.Height = Math.Max(size.Height, top + desiredSize.Height);
        //        }
        //    }

        //    return size;
        //}

        #endregion
        
        public void AddItem(FrameworkElement _item, double _height, double _width, bool showadorner)
        {
            DesignerItem _di = new DesignerItem();
            
            _di.Content = _item;
            _di.ShowAdorner = showadorner;

            Point position = Mouse.GetPosition(this);

            _di.Width = _width + 40; // + 40 because drag adorner around control is 20 pixel wide
            _di.Height = _height + 40;

            Canvas.SetZIndex(_di, this.Children.Count);
            this.Children.Add(_di);
        }
    }
}
