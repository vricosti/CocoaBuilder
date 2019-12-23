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
    ///     <MyNamespace:WPFPopup/>
    ///
    /// </summary>


    [TemplatePart(Name = "PART_ButtonDone", Type = typeof(Button))]
    [TemplatePart(Name = "PART_DefaultToolbar", Type = typeof(ListBox))]
    [TemplatePart(Name = "PART_AllowedToolbar", Type = typeof(ListBox))]
    [TemplatePart(Name = "PART_PopupBorder", Type = typeof(Grid))]
    public class ToolbarEditor : Control
    {
        static ToolbarEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToolbarEditor), new FrameworkPropertyMetadata(typeof(ToolbarEditor)));
        }

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

        public SMToolbar AdornedElement
        {
            get
            {
                return Adorner == null ? null : Adorner.AdornedElement as SMToolbar;
            }
        }

        //protected override Size MeasureOverride(Size availableSize)
        //{
        //    var controlAdorner = Adorner as ControlAdorner;
        //    if (controlAdorner != null)
        //    {
        //        controlAdorner.Placeholder = this;
        //    }

        //    return new Size(controlAdorner.ActualWidth, controlAdorner.ActualHeight);

        //    //if (controlAdorner.ActualWidth != 0 && controlAdorner.ActualHeight != 0)
        //    //    return new Size(controlAdorner.ActualWidth, controlAdorner.ActualHeight);
        //    //else
        //    //    return new Size(0, 0);
        //}

        #region Private members

        private ListBox _lbAllowed;
        private ListBox _lbDefault;
        private Grid _gridPopup;

        enum DragOrigin
        {
            FromAllowed,
            FromDefault
        }

        DragOrigin _tbiDragFrom;

        #endregion

        #region Properties

        #region Internal Properties

        internal Button ButtonDone
        {
            get;
            private set;
        }

        internal ListBox AllowedItemList
        {
            get { return _lbAllowed; }
        }

        internal ListBox DefaultItemList
        {
            get { return _lbDefault; }
        }

        #endregion //Internal Properties

        #region Public Properties

        #region DefaultItems

        public static readonly DependencyProperty DefaultItemsProperty = DependencyProperty.Register("DefaultItems", typeof(List<SMToolbarItem>), typeof(ToolbarEditor));
        public List<SMToolbarItem> DefaultItems
        {
            get
            {
                return (List<SMToolbarItem>)GetValue(DefaultItemsProperty);
            }
            set
            {
                SetValue(DefaultItemsProperty, value);
            }
        }

        #endregion //DefaultItems

        #region AllowedItems

        public static readonly DependencyProperty AllowedItemsProperty = DependencyProperty.Register("AllowedItems", typeof(List<SMToolbarItem>), typeof(ToolbarEditor));
        public List<SMToolbarItem> AllowedItems
        {
            get
            {
                return (List<SMToolbarItem>)GetValue(AllowedItemsProperty);
            }
            set
            {
                SetValue(AllowedItemsProperty, value);
            }
        }

        #endregion //AllowedItems

        #endregion //Public Properties

        #endregion //Properties

        #region Constructors

        public ToolbarEditor()
        {

        }

        #endregion //Constructors

        #region Base Class Overrides

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            double hei = this.ActualHeight;

            SMToolbar cw = this.AdornedElement;
            this.AllowedItems = cw.AllowedItems;
            this.DefaultItems = cw.DefaultItems;

            if (ButtonDone != null)
                ButtonDone.Click -= (o, e) => UpdateToolBar();

            ButtonDone = GetTemplateChild("PART_ButtonDone") as Button;

            if (ButtonDone != null)
                ButtonDone.Click += (o, e) => UpdateToolBar();

            _lbAllowed = GetTemplateChild("PART_AllowedToolbar") as ListBox;
            _lbDefault = GetTemplateChild("PART_DefaultToolbar") as ListBox;

            Style itemContainerStyle = new Style(typeof(ListBoxItem));
            itemContainerStyle.Setters.Add(new Setter(ListBoxItem.BorderThicknessProperty, new Thickness(0)));
            itemContainerStyle.Setters.Add(new EventSetter(ListBoxItem.PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(Allowed_PreviewMouseLeftButtonDown)));
            itemContainerStyle.Setters.Add(new EventSetter(ListBoxItem.DropEvent, new DragEventHandler(AllowedItem_Drop)));
            _lbAllowed.ItemContainerStyle = itemContainerStyle;

            itemContainerStyle = new Style(typeof(ListBoxItem));
            itemContainerStyle.Setters.Add(new Setter(ListBoxItem.AllowDropProperty, true));
            itemContainerStyle.Setters.Add(new Setter(ListBoxItem.BorderThicknessProperty, new Thickness(0)));
            itemContainerStyle.Setters.Add(new EventSetter(ListBoxItem.PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(Default_PreviewMouseLeftButtonDown)));
            itemContainerStyle.Setters.Add(new EventSetter(ListBoxItem.DropEvent, new DragEventHandler(DefaultItem_Drop)));
            _lbDefault.ItemContainerStyle = itemContainerStyle;

            _lbDefault.Drop += new DragEventHandler(Default_Drop);
            _lbAllowed.Drop += new DragEventHandler(Allowed_Drop);

            _gridPopup = GetTemplateChild("PART_PopupBorder") as Grid;
            _gridPopup.Drop += new DragEventHandler(_popupBorder_Drop);            
        }

        #endregion //Base Class Overrides

        #region Drag and Drop events

        void _popupBorder_Drop(object sender, DragEventArgs e)
        {
            if (_tbiDragFrom == DragOrigin.FromAllowed)
            {
                SMToolbarItem droppedData = e.Data.GetData(typeof(SMToolbarItem)) as SMToolbarItem;

                DefaultItems.RemoveAll(item => item.ItemIdentifier == droppedData.ItemIdentifier);
                AllowedItems.Remove(droppedData);

                _lbDefault.ItemsSource = DefaultItems;
                _lbAllowed.Items.Refresh();
                _lbDefault.Items.Refresh();
                this.AdornedElement.AddItemsToToolbar();
            }

            if (_tbiDragFrom == DragOrigin.FromDefault)
            {
                SMToolbarItem droppedData = e.Data.GetData(typeof(SMToolbarItem)) as SMToolbarItem;                
                DefaultItems.Remove(droppedData);

                _lbDefault.ItemsSource = DefaultItems;
                _lbDefault.Items.Refresh();
                this.AdornedElement.AddItemsToToolbar();
            }
        }

        void Allowed_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBoxItem)
            {
                _tbiDragFrom = DragOrigin.FromAllowed;
                ListBoxItem draggedItem = sender as ListBoxItem;
                DragDrop.DoDragDrop(draggedItem, draggedItem.DataContext, DragDropEffects.Move);
                draggedItem.IsSelected = true;
            }
        }

        void Default_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBoxItem)
            {
                _tbiDragFrom = DragOrigin.FromDefault;
                ListBoxItem draggedItem = sender as ListBoxItem;
                DragDrop.DoDragDrop(draggedItem, draggedItem.DataContext, DragDropEffects.Move);
                draggedItem.IsSelected = true;
            }
        }

        void Default_Drop(object sender, DragEventArgs e)
        {
            if (_tbiDragFrom == DragOrigin.FromAllowed)
            {
                SMToolbarItem droppedData = e.Data.GetData(typeof(SMToolbarItem)) as SMToolbarItem;
                SMToolbarItem _imgButton = new SMToolbarItem();
                _imgButton.NormalImage = droppedData.NormalImage;
                _imgButton.ItemIdentifier = droppedData.ItemIdentifier;
                _imgButton.Label = droppedData.Label;          

                if (droppedData.ItemIdentifier == "NSToolbarFlexibleSpaceItem"
                    || droppedData.ItemIdentifier == "NSToolbarSpaceItem"
                || droppedData.ItemIdentifier == "NSToolbarSeparatorItem")
                {
                    _imgButton.Label = string.Empty;
                }

                //object ob = ((ListBox)(sender)).DataContext;

                DefaultItems.Add(_imgButton);

                _lbDefault.ItemsSource = DefaultItems; // 
                _lbDefault.Items.Refresh();
                this.AdornedElement.AddItemsToToolbar();
            }

            if (_tbiDragFrom == DragOrigin.FromDefault)
            {
                SMToolbarItem droppedData = e.Data.GetData(typeof(SMToolbarItem)) as SMToolbarItem;
                int removedIdx = _lbDefault.Items.IndexOf(droppedData);

                DefaultItems.Add(droppedData);
                DefaultItems.RemoveAt(removedIdx);

                _lbDefault.ItemsSource = DefaultItems;
                _lbDefault.Items.Refresh();
                this.AdornedElement.AddItemsToToolbar();
            }
            e.Handled = true;
        }

        void DefaultItem_Drop(object sender, DragEventArgs e)
        {
            if (_tbiDragFrom == DragOrigin.FromAllowed)
            {
                SMToolbarItem droppedData = e.Data.GetData(typeof(SMToolbarItem)) as SMToolbarItem;
                SMToolbarItem _imgButton = new SMToolbarItem();
                _imgButton.NormalImage = droppedData.NormalImage;
                _imgButton.Label = droppedData.Label;
                _imgButton.ItemIdentifier = droppedData.ItemIdentifier;     

                if (droppedData.ItemIdentifier == "NSToolbarFlexibleSpaceItem"
                    || droppedData.ItemIdentifier == "NSToolbarSpaceItem"
                || droppedData.ItemIdentifier == "NSToolbarSeparatorItem")
                {
                    _imgButton.Label = string.Empty;
                }

                SMToolbarItem target = ((ListBoxItem)(sender)).DataContext as SMToolbarItem;
                int targetIdx = _lbDefault.Items.IndexOf(target);

                ListBoxItem lbi = (ListBoxItem)(sender);
                Point p = e.GetPosition(lbi);
                if (p.X > (lbi.ActualWidth / 2))
                    targetIdx += 1;

                DefaultItems.Insert(targetIdx, _imgButton);
            }

            if (_tbiDragFrom == DragOrigin.FromDefault)
            {
                SMToolbarItem droppedData = e.Data.GetData(typeof(SMToolbarItem)) as SMToolbarItem;
                SMToolbarItem target = ((ListBoxItem)(sender)).DataContext as SMToolbarItem;
                int targetIdx = _lbDefault.Items.IndexOf(target);
                int removedIdx = _lbDefault.Items.IndexOf(droppedData);

                ListBoxItem lbi = (ListBoxItem)(sender);
                Point p = e.GetPosition(lbi);
                if (p.X > (lbi.ActualWidth / 2))
                    targetIdx += 1;

                if (removedIdx < targetIdx)
                {
                    DefaultItems.Insert(targetIdx, droppedData);
                    DefaultItems.RemoveAt(removedIdx);
                }
                else
                {
                    int remIdx = removedIdx + 1;
                    if (DefaultItems.Count + 1 > remIdx)
                    {
                        DefaultItems.Insert(targetIdx, droppedData);
                        DefaultItems.RemoveAt(remIdx);
                    }
                }
            }

            _lbDefault.ItemsSource = DefaultItems;
            _lbDefault.Items.Refresh();
            this.AdornedElement.AddItemsToToolbar();

            e.Handled = true;
        }

        void AllowedItem_Drop(object sender, DragEventArgs e)
        {
            if (_tbiDragFrom == DragOrigin.FromAllowed)
            {
                SMToolbarItem droppedData = e.Data.GetData(typeof(SMToolbarItem)) as SMToolbarItem;
                SMToolbarItem target = ((ListBoxItem)(sender)).DataContext as SMToolbarItem;
                int targetIdx = _lbAllowed.Items.IndexOf(target);
                int removedIdx = _lbAllowed.Items.IndexOf(droppedData);

                ListBoxItem lbi = (ListBoxItem)(sender);
                Point p = e.GetPosition(lbi);
                if (p.X > (lbi.ActualWidth / 2))
                    targetIdx += 1;

                if (removedIdx < targetIdx)
                {
                    AllowedItems.Insert(targetIdx, droppedData);
                    AllowedItems.RemoveAt(removedIdx);
                }
                else
                {
                    int remIdx = removedIdx + 1;
                    if (AllowedItems.Count + 1 > remIdx)
                    {
                        AllowedItems.Insert(targetIdx, droppedData);
                        AllowedItems.RemoveAt(remIdx);
                    }
                }
                _lbAllowed.ItemsSource = AllowedItems;
                _lbAllowed.Items.Refresh();
            }

            e.Handled = true;
        }

        void Allowed_Drop(object sender, DragEventArgs e)
        {
            if (_tbiDragFrom == DragOrigin.FromAllowed)
            {
                SMToolbarItem droppedData = e.Data.GetData(typeof(SMToolbarItem)) as SMToolbarItem;
                int removedIdx = _lbAllowed.Items.IndexOf(droppedData);

                AllowedItems.Add(droppedData);
                AllowedItems.RemoveAt(removedIdx);

                _lbAllowed.ItemsSource = AllowedItems;
                _lbAllowed.Items.Refresh();
            }
            e.Handled = true;
        }

        #endregion

        void UpdateToolBar()
        {
            this.AdornedElement.State = SMToolbarState.Normal;
            Adorners.SetIsVisible(this.AdornedElement, false);
        }
    }
}
