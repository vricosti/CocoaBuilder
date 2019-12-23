using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using Smartmobili.Cocoa;
using System.Collections;
using System.IO;
using SM.Toolkit;

namespace XibBuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string toolbarSpaceItemImage = "IBNSFixedSpace.tiff";
        string toolbarFlexiSpaceItemImage = "IBNSFlexibleSpace.tiff";
        string toolbarSeparatorItemImage = "IBNSToolbarSeparator.png";
        string toolbarPrintItemImage = "NSToolbarPrint.tiff";
        string toolbarFontsItemImage = "NSToolbarShowFonts.tiff";
        string toolbarColorsItemImage = "NSToolbarShowColors.tiff";
        string xibPath = string.Empty;
        public static RoutedCommand Exit = new RoutedCommand();
        SMMenu _menu;

        public MainWindow()
        {
            InitializeComponent();
            this.CommandBindings.Add(new CommandBinding(MainWindow.Exit, Exit_Executed));
        }
        
        #region Events

        /// <summary>
        /// Opens a new XIB
        /// </summary>        
        private void miOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();                
                dlg.DefaultExt = ".xib"; // Default file extension
                dlg.Filter = "Xib documents (.xib)|*.xib"; // Filter files by extension 

                // Show open file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process open file dialog box results
                if (result == true)
                {
                    //// open document 
                    xibPath = dlg.FileName;
                    //ParseXib(xibPath);
#if true
                    IBDocument ibDoc = (IBDocument)IBDocument.alloc().init();
                    if (ibDoc.readFromURL((NSURL)NSURL.fileURLWithPath(xibPath), "", null))
                    {
                        //NSCustomObject custObj = (NSCustomObject)NSCustomObject.alloc().init();
                        ParseXib(ibDoc.RootObjects);
                    }
#else
                    IDEDocumentController
#endif

                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// Remove the menu 
        /// </summary>        
        private void menu_CloseClick(object sender, RoutedEventArgs e)
        {
            mainGrid.Children.Remove(sender as UIElement);
        }

        /// <summary>
        /// Exit command to close the app
        /// </summary>        
        private void Exit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                Application.Current.Shutdown();
            }
            catch (Exception exc)
            {
                System.Windows.MessageBox.Show(exc.Message);
            }
        }

        #endregion

        #region Helper methods

        private void ParseXib(NSMutableArray array)
        {
            XibParser xibParser = new XibParser(xibPath);
            var ibArchive = xibParser.Deserialize();

            foreach (var nsObj in array)
            {
                if (nsObj is NSWindowTemplate)
                {
                    if (nsObj != null)
                    {
                        NSWindowTemplate nsWindow = (NSWindowTemplate)nsObj;

                        if (nsWindow.ClassName.Value == "NSWindow")
                        {
                            SMWindow window = CreateWindow(xibPath, nsWindow);
                            canvas.AddItem(window, nsWindow.WindowRect.Size.Height + window.ToolbarHeight + window.FrameHeight, nsWindow.WindowRect.Size.Width, true);
                        }
                        else if (nsWindow.ClassName.Value == "NSPanel")
                        {
                            SMPanel panel = CreatePanel(nsWindow);
                            canvas.AddItem(panel, nsWindow.WindowRect.Size.Height, nsWindow.WindowRect.Size.Width, true);
                        }
                    }
                }
                else if (nsObj is NSMenu)
                {
                    _menu = CreateMenu(xibPath, (NSMenu)nsObj);
                    Grid.SetRow(_menu, 1);
                    mainGrid.Children.Add(_menu);
                    _menu.CloseClick += new RoutedEventHandler(menu_CloseClick);
                }
            }
        }

        private SMMenu CreateMenu(string xibPath, NSMenu nsMenu)
        {
            SMMenu menu = new SMMenu();
            menu.Width = canvas.ActualWidth;
            menu.Height = 25;

            foreach (NSMenuItem nsMenuItem in nsMenu.MenuItems)
            {
                SMMenuItem mi = new SMMenuItem();
                mi.Header = nsMenuItem.Title;
                menu.Items.Add(mi);
                CreateMenuItems(mi, nsMenuItem);
            }

            return menu;
        }

        private void CreateMenuItems(SMMenuItem mi, NSMenuItem nsMenuItem)
        {
            if (nsMenuItem.SubMenu != null)
            {
                foreach (NSMenuItem nsSubMI in nsMenuItem.SubMenu.MenuItems)
                {
                    if (nsSubMI.IsSeparator)
                    {
                        mi.Items.Add(new Separator());
                    }
                    else
                    {
                        SMMenuItem subMI = new SMMenuItem();
                        subMI.Header = nsSubMI.Title;
                        mi.Items.Add(subMI);
                        CreateMenuItems(subMI, nsSubMI);
                    }
                }
            }
        }

        private SMWindow CreateWindow(string xibPath, NSWindowTemplate nsWindow)
        {
            SMWindow wpfWindow = new SMWindow();
            wpfWindow.Show();
            wpfWindow.Caption = nsWindow.Title;            
            wpfWindow.ShowToolBar = true;
            wpfWindow.Width = nsWindow.WindowRect.Size.Width;

            // THe height of the window depends on the toolbar
            wpfWindow.FrameHeight = 22;

            NSToolbar nsToolbar = nsWindow.ViewClass as NSToolbar;

            #region Set Window Toolbar

            if (nsToolbar != null  &&  nsToolbar.IBDefaultItems != null  &&  nsToolbar.IBDefaultItems.Count > 0)
            {
                wpfWindow.ToolbarHeight = 56;
            }
            else
            {
                wpfWindow.ToolbarHeight = 19;
            }

            wpfWindow.Height = nsWindow.WindowRect.Size.Height + wpfWindow.ToolbarHeight + wpfWindow.FrameHeight;

            SMToolbar _toolbar = new SMToolbar();
            wpfWindow.Toolbar = _toolbar;

            if (nsToolbar != null  &&  nsToolbar.IBDefaultItems != null  &&  nsToolbar.IBDefaultItems.Count > 0)
            {
                // Add toolbar default items
                wpfWindow.Toolbar.DefaultItems = FillWindowToolbar(nsToolbar.IBDefaultItems);

                // Add toolbar allowed items
                wpfWindow.Toolbar.AllowedItems = FillWindowToolbar(nsToolbar.IBAllowedItems);
                
                // Refresh window toolbar
                wpfWindow.Toolbar.AddItemsToToolbar();
            }

            #endregion

            #region Set Window Content

            if (nsWindow.View != null)
            {
                DesignerCanvas rootCanvas = new DesignerCanvas();
                rootCanvas.Background = Brushes.Transparent;
                wpfWindow.Content = rootCanvas;

                NSView nsView = nsWindow.View as NSView;
                foreach (NSObject subView in nsView.SubViews)
                {
                    if (subView != null)
                    {
                        GenerateWindowContent(rootCanvas, subView);
                    }
                }
            }

            #endregion

            return wpfWindow;
        }

        /// <summary>
        /// Creates a SMToolbarItem object
        /// </summary>
        private SMToolbarItem CreateToolBarItem(NSToolbarItem paramTBI, string xibPath)
        {
            Uri _uri;

            switch (paramTBI.ItemIdentifier.Value)
            {
                case "NSToolbarSpaceItem":
                    _uri = new Uri("/XibBuilder;component/Images/" + toolbarSpaceItemImage, UriKind.Relative);
                    break;
                case "NSToolbarFlexibleSpaceItem":
                    _uri = new Uri("/XibBuilder;component/Images/" + toolbarFlexiSpaceItemImage, UriKind.Relative);
                    break;
                case "NSToolbarSeparatorItem":
                    _uri = new Uri("/XibBuilder;component/Images/" + toolbarSeparatorItemImage, UriKind.Relative);
                    break;
                case "NSToolbarShowColorsItem":
                    _uri = new Uri("/XibBuilder;component/Images/" + toolbarColorsItemImage, UriKind.Relative);
                    break;
                case "NSToolbarPrintItem":
                    _uri = new Uri("/XibBuilder;component/Images/" + toolbarPrintItemImage, UriKind.Relative);
                    break;
                case "NSToolbarShowFontsItem":
                    _uri = new Uri("/XibBuilder;component/Images/" + toolbarFontsItemImage, UriKind.Relative);
                    break;
                default:
                    _uri = new Uri(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(xibPath), paramTBI.Image.ResourceName + ".png"), UriKind.Absolute);
                    break;
            }

            SMToolbarItem _smTBI = new SMToolbarItem();

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = _uri;
            bi.EndInit();

            _smTBI.NormalImage = bi;
            _smTBI.ItemIdentifier = paramTBI.ItemIdentifier.Value;
            _smTBI.Label = paramTBI.Label;
            _smTBI.PaletteLabel = paramTBI.PaletteLabel;

            return _smTBI;
        }

        /// <summary>
        /// Creates a SMPanel object
        /// </summary>        
        private SMPanel CreatePanel(NSWindowTemplate paramWindowTemplate)
        {
            SMPanel panel = new SMPanel();
            panel.Caption = paramWindowTemplate.Title;

            return panel;
        }

        /// <summary>
        /// Generates window content and add WPF controls corresponding to provided NS controls
        /// </summary>
        /// <param name="rootCanvas">WPF container control to which other WPF controls are added</param>
        /// <param name="nsObject">NS object to be converted to WPF control</param>
        private void GenerateWindowContent(Canvas rootCanvas, NSObject nsObject)
        {
            DesignerControl dc;
            switch (nsObject.GetType().Name)
            {
                case "NSTextField":
                    NSTextField nsTextField = nsObject as NSTextField;
                    if (nsTextField.Cell.Editable)
                    {
                        SMTextField smTF = new SMTextField();
                        smTF.IsReadOnly = !nsTextField.Cell.Editable;
                        dc = new DesignerControl();
                        SetSizeAndLocation(nsTextField, dc);
                        dc.Content = smTF;
                        //SetSizeAndLocation(nsTextField, tb);
                        //rootCanvas.Children.Add(tb);
                        rootCanvas.Children.Add(dc);
                    }
                    else
                    {
                        SMTextField smTF = new SMTextField();
                        smTF.IsReadOnly = !nsTextField.Cell.Editable;
                        dc = new DesignerControl();
                        SetSizeAndLocation(nsTextField, dc);
                        dc.Content = smTF;
                        //SetSizeAndLocation(nsTextField, tb);
                        //rootCanvas.Children.Add(tb);
                        rootCanvas.Children.Add(dc);

                        smTF.TextAlignment = MapAlignment(nsTextField.Cell.Alignment);
                        smTF.Text = nsTextField.Cell.StringValue.Value;  
                    } 

                    break;

                case "NSButton":
                    NSButton nsButton = nsObject as NSButton;

                    dc = new DesignerControl();

                    SMButton btn = new SMButton();
                    SetSizeAndLocation(nsButton, dc);
                    //rootCanvas.Children.Add(btn);
                    dc.Content = btn;
                    rootCanvas.Children.Add(dc);

                    btn.Content = nsButton.Title.Value;
                    btn.IsBordered = nsButton.Bordered;          
                    btn.StyleType = nsButton.BezelStyle.ToString();
                    
                    break;

                case "NSPopUpButton":
                    NSPopUpButton nsPopupButton = nsObject as NSPopUpButton;
                    
                    SMPopupButton smPopupBtn = new SMPopupButton();

                    dc = new DesignerControl();

                    //SMButton btn = new SMButton();
                    SetSizeAndLocation(nsPopupButton, dc);
                    ////rootCanvas.Children.Add(btn);
                    dc.Content = smPopupBtn;
                    rootCanvas.Children.Add(dc);

                    break;

                case "NSCustomView":
                    NSCustomView nsCustomView = nsObject as NSCustomView;

                    //Canvas windowContentCanvas = new Canvas();
                    DesignerCanvas windowContentCanvas = new DesignerCanvas();
                    SetSizeAndLocation(nsCustomView, windowContentCanvas);
                    rootCanvas.Children.Add(windowContentCanvas);

                    Rectangle rectBorder = new Rectangle();
                    rectBorder.StrokeThickness = 1;
                    rectBorder.Stroke = Brushes.LightGray;
                    rectBorder.Height = nsCustomView.Frame.Size.Height;
                    rectBorder.Width = nsCustomView.Frame.Size.Width;
                    rectBorder.IsHitTestVisible = false;
                    //SetSizeAndLocation(nsCustomView, rectBorder);
                    windowContentCanvas.Children.Add(rectBorder);
                    

                    if (nsCustomView.SubViews != null)
                    {
                        foreach (NSObject nsObj in nsCustomView.SubViews)
                        {
                            if (nsObj != null)
                            {
                                GenerateWindowContent(windowContentCanvas, nsObj);
                            }
                        }
                    }
                    break;
            }
        }
                
        /// <summary>
        /// Set Height, Width and Left, Top location of WPF control corresponding to given NSControl
        /// </summary>        
        private void SetSizeAndLocation(NSView nsControl, FrameworkElement wpfControl)
        {
            double offsetTop;

            if (nsControl.Superview != null)
            {
                offsetTop = CalculateNewOffsetY(nsControl.Frame.Origin.Y + nsControl.Frame.Size.Height, (nsControl.Superview as NSView).Frame.Size.Height);
            }
            else
            {
                offsetTop = CalculateNewOffsetY(nsControl.Frame.Origin.Y + nsControl.Frame.Size.Height, nsControl.Frame.Size.Height);
            }

            wpfControl.Height = nsControl.Frame.Size.Height;
            wpfControl.Width = nsControl.Frame.Size.Width;
            Canvas.SetLeft(wpfControl, nsControl.Frame.Origin.X);
            Canvas.SetTop(wpfControl, offsetTop);
        }

        /// <summary>
        /// Calculates new offset by taking upper left corner as (0,0)
        /// Y-offset from in xib is taking lower left corner as (0,0) 
        /// </summary>
        /// <param name="offsetY">Current Y-offset</param>
        /// <param name="height">Height of the control</param>
        /// <returns>New Y-offset</returns>
        private double CalculateNewOffsetY(double offsetY, double height)
        {
            return height - offsetY;
        }

        private List<SMToolbarItem> FillWindowToolbar(NSArray nsArray)
        {
            List<SMToolbarItem> _listDefaultItems = new List<SMToolbarItem>();

            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Horizontal;

            foreach (NSToolbarItem tbi in nsArray)
            {
                if (tbi != null)
                {
                    if (tbi.Image != null)
                    {
                        SMToolbarItem smTBI = CreateToolBarItem(tbi, xibPath);
                        _listDefaultItems.Add(smTBI);
                    }
                    else
                    {
                        SMToolbarItem smTBI = CreateToolBarItem(tbi, xibPath);
                        _listDefaultItems.Add(smTBI);
                    }
                }
            }

            return _listDefaultItems;
        }

        private System.Windows.TextAlignment MapAlignment(NSTextAlignment nsTextAlign)
        {
            switch (nsTextAlign.ToString())
            {
                case "NSCenterTextAlignment":
                    return TextAlignment.Center;
                case "NSLeftTextAlignment":
                    return TextAlignment.Left;
                case "NSRightTextAlignment":
                    return TextAlignment.Right;
                case "NSJustifiedTextAlignment":
                    return TextAlignment.Justify;
                case "NSNaturalTextAlignment":
                    return TextAlignment.Left;
            }

            return TextAlignment.Left;
        }

        #endregion
    }
}