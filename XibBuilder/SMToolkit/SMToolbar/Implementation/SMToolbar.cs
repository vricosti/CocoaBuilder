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
    ///     <MyNamespace:SMToolbar/>
    ///
    /// </summary>
    
    [TemplatePart(Name = "PART_Root", Type = typeof(Border))]
    public class SMToolbar : Control
    {
        #region Private Members

        private Border _borderRoot;
        private ContentControl _content;
        private ScrollViewer _scroller;

        #endregion //Private Members

        static SMToolbar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SMToolbar), new FrameworkPropertyMetadata(typeof(SMToolbar)));
        }

        #region Properties

        #region Content

        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(object), typeof(SMToolbar), new UIPropertyMetadata(null));
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

        #endregion //Content

        #region DefaultItems

        public static readonly DependencyProperty DefaultItemsProperty = DependencyProperty.Register("DefaultItems", typeof(List<SMToolbarItem>), typeof(SMToolbar));
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

        public static readonly DependencyProperty AllowedItemsProperty = DependencyProperty.Register("AllowedItems", typeof(List<SMToolbarItem>), typeof(SMToolbar));
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

        #region ShowOverflow

        private static readonly DependencyProperty ShowOverflowProperty = DependencyProperty.Register("ShowOverflow", typeof(bool), typeof(SMToolbar), new UIPropertyMetadata(false));
        private bool ShowOverflow
        {
            get
            {
                return (bool)GetValue(ShowOverflowProperty);
            }
            set
            {
                SetValue(ShowOverflowProperty, value);
            }
        }

        #endregion //ShowOverflow

        #region State

        public static readonly DependencyProperty StateProperty = DependencyProperty.Register("State", typeof(SMToolbarState), typeof(SMToolbar), new UIPropertyMetadata(SMToolbarState.Normal));
        public SMToolbarState State
        {
            get
            {
                return (SMToolbarState)GetValue(StateProperty);
            }
            set
            {
                SetValue(StateProperty, value);
            }
        }

        #endregion //State

        #endregion //Properties

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            
            _borderRoot = GetTemplateChild("PART_Root") as Border; 
            if (_borderRoot != null)
            {
                _borderRoot.AddHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(ToolBarMouseLeftButtonDown));
            }

            _content = GetTemplateChild("PART_Content") as ContentControl;
            _scroller = GetTemplateChild("PART_Scroll") as ScrollViewer;

            this.SizeChanged += new SizeChangedEventHandler(SMToolbar_SizeChanged);
        }

        void SMToolbar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this._scroller != null)
            {
                double wid = _scroller.ViewportWidth;
                if (_scroller.ExtentWidth > _scroller.ViewportWidth)
                    this.ShowOverflow = true;
                else
                    this.ShowOverflow = false;
            }
        }
        
        void ToolBarMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            ResourceDictionary rd = this.Template.Resources;

            if (this.State == SMToolbarState.Normal)
            {
                _borderRoot.Background = this.TryFindResource("ToolbarHighlightBrush") as LinearGradientBrush; // _toolbarPopup.IsOpen = true;
                this.State = SMToolbarState.Highlight;
            }
            else if (this.State == SMToolbarState.Highlight)
            {
                _borderRoot.Background = Brushes.Transparent;
                Adorners.SetIsVisible(this, true);
                this.State = SMToolbarState.Edit;
            }            
        }

        public void AddItemsToToolbar()
        {
            Grid toolbar = new Grid();
            toolbar.Name = "gridToolbar";
            toolbar.Margin = new Thickness(10, 0, 10, 0);

            for (int i = 0; i < DefaultItems.Count; i++)
            {
                SMToolbarItem ib = DefaultItems[i];
                if (ib != null)
                {
                    bool isFlexibleSpace = ib.ItemIdentifier == "NSToolbarFlexibleSpaceItem";
                    bool isSpace = ib.ItemIdentifier == "NSToolbarSpaceItem";
                    bool isSeparator = ib.ItemIdentifier == "NSToolbarSeparatorItem";

                    ColumnDefinition gridColTBItem = new ColumnDefinition();

                    if (isFlexibleSpace)
                        gridColTBItem.Width = new GridLength(1, GridUnitType.Star);
                    else if (isSpace)
                        gridColTBItem.Width = new GridLength(32);
                    else if (isSeparator)
                        gridColTBItem.Width = new GridLength(8);
                    else
                        gridColTBItem.Width = new GridLength(1, GridUnitType.Auto);


                    toolbar.ColumnDefinitions.Add(gridColTBItem);


                    if (!isFlexibleSpace && !isSpace && !isSeparator)
                    {
                        SMToolbarItem _imgButton = new SMToolbarItem();
                        _imgButton.NormalImage = ib.NormalImage;
                        _imgButton.Label = ib.Label;
                        _imgButton.PaletteLabel = ib.PaletteLabel;
                        _imgButton.ItemIdentifier = ib.ItemIdentifier;

                        Grid.SetColumn(_imgButton, i);
                        toolbar.Children.Add(_imgButton);
                    }
                }
            }

            this.Content = toolbar;

             
            if (this._scroller != null)
            {
                double wid = _scroller.ViewportWidth;
                if (_scroller.ExtentWidth > _scroller.ViewportWidth)
                    this.ShowOverflow = true;
                else
                    this.ShowOverflow = false;
            }
        }
    }
}
