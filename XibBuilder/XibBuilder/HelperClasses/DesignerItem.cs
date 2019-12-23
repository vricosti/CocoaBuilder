using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace XibBuilder
{
    //These attributes identify the types of the named parts that are used for templating
    [TemplatePart(Name = "PART_DragThumb", Type = typeof(DragThumb))]
    [TemplatePart(Name = "PART_ResizeDecorator", Type = typeof(Control))]    
    [TemplatePart(Name = "PART_ContentPresenter", Type = typeof(ContentPresenter))]
    [TemplatePart(Name = "PART_CloseButton", Type = typeof(Button))]
    public class DesignerItem : ContentControl, ISelectable
    {
        Button btnClose;
 
        #region IsSelected Property

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
        public static readonly DependencyProperty IsSelectedProperty =
          DependencyProperty.Register("IsSelected",
                                       typeof(bool),
                                       typeof(DesignerItem),
                                       new FrameworkPropertyMetadata(false));

        #endregion

        #region ShowAdorner Property

        public bool ShowAdorner
        {
            get { return (bool)GetValue(ShowAdornerProperty); }
            set { SetValue(ShowAdornerProperty, value); }
        }
        public static readonly DependencyProperty ShowAdornerProperty =
          DependencyProperty.Register("ShowAdorner",
                                       typeof(bool),
                                       typeof(DesignerItem),
                                       new FrameworkPropertyMetadata(true));

        #endregion

        #region DragThumbTemplate Property

        // can be used to replace the default template for the DragThumb
        public static readonly DependencyProperty DragThumbTemplateProperty =
            DependencyProperty.RegisterAttached("DragThumbTemplate", typeof(ControlTemplate), typeof(DesignerItem));

        public static ControlTemplate GetDragThumbTemplate(UIElement element)
        {
            return (ControlTemplate)element.GetValue(DragThumbTemplateProperty);
        }

        public static void SetDragThumbTemplate(UIElement element, ControlTemplate value)
        {
            element.SetValue(DragThumbTemplateProperty, value);
        }

        #endregion

        static DesignerItem()
        {
            // set the key to reference the style for this control
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(
                typeof(DesignerItem), new FrameworkPropertyMetadata(typeof(DesignerItem)));
        }

        public DesignerItem()
        {
            TransformGroup tg = new TransformGroup();
            tg.Children.Add(new RotateTransform(0));
            tg.Children.Add(new TranslateTransform());
            this.RenderTransform = tg;
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);
            DesignerCanvas designer = VisualTreeHelper.GetParent(this) as DesignerCanvas;

            // update selection
            if (designer != null)
            {
                if ((Keyboard.Modifiers & (ModifierKeys.Shift | ModifierKeys.Control)) != ModifierKeys.None)
                    if (this.IsSelected)
                    {
                        designer.SelectionService.RemoveFromSelection(this);
                    }
                    else
                    {
                        designer.SelectionService.AddToSelection(this);
                    }
                else if (!this.IsSelected)
                {
                    designer.SelectionService.SelectItem(this);
                }
                Focus();
            }

            e.Handled = false;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (this.Template != null)
            {
                btnClose = GetTemplateChild("PART_CloseButton") as Button;
                btnClose.Click += new RoutedEventHandler(btnClose_Click);


                ContentPresenter contentPresenter =  this.Template.FindName("PART_ContentPresenter", this) as ContentPresenter;
                //if (contentPresenter != null)
                //{
                //    UIElement contentVisual = VisualTreeHelper.GetChild(contentPresenter, 0) as UIElement;
                //    if (contentVisual != null)
                //    {
                //        DragThumb thumb = this.Template.FindName("PART_DragThumb", this) as DragThumb;
                //        if (thumb != null)
                //        {
                //            ControlTemplate template =  DesignerItem.GetDragThumbTemplate(contentVisual) as ControlTemplate;
                //            if (template != null)
                //                thumb.Template = template;
                //        }
                //    }
                //}
            }
        }

        void btnClose_Click(object sender, RoutedEventArgs e)
        {
            DesignerCanvas _dc = this.Parent as DesignerCanvas;
            _dc.Children.Remove(this);
        }
    }
}
