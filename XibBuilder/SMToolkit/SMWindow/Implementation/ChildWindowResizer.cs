using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Runtime.InteropServices;
using System.Windows;
using System.Threading;

namespace SM.Toolkit
{
    public class ChildWindowResizer
    {
        private SMWindow target = null;

        private bool resizeRight = false;
        private bool resizeLeft = false;
        private bool resizeUp = false;
        private bool resizeDown = false;

        private Dictionary<UIElement, short> leftElements = new Dictionary<UIElement, short>();
        private Dictionary<UIElement, short> rightElements = new Dictionary<UIElement, short>();
        private Dictionary<UIElement, short> upElements = new Dictionary<UIElement, short>();
        private Dictionary<UIElement, short> downElements = new Dictionary<UIElement, short>();

        //private PointAPI resizePoint = new PointAPI();
        private Point resizePoint = new Point();
        private Size resizeSize = new Size();
        private Point resizeWindowPoint = new Point();

        private delegate void RefreshDelegate();

        public ChildWindowResizer(SMWindow target)
        {
            this.target = target;

            if (target == null)
            {
                throw new Exception("Invalid Window handle");
            }
        }

        #region add resize components
        private void connectMouseHandlers(UIElement element)
        {
            if (element == null)
                return;

            element.MouseLeftButtonDown += new MouseButtonEventHandler(element_MouseLeftButtonDown);
            element.MouseLeftButtonUp += new MouseButtonEventHandler(element_MouseLeftButtonUp);
            element.MouseEnter += new MouseEventHandler(element_MouseEnter);
            element.MouseLeave += new MouseEventHandler(setArrowCursor);
        }

        public void addResizerRight(UIElement element)
        {
            if (element == null)
                return;

            connectMouseHandlers(element);
            rightElements.Add(element, 0);
        }

        public void addResizerLeft(UIElement element)
        {
            if (element == null)
                return;

            connectMouseHandlers(element);
            leftElements.Add(element, 0);
        }

        public void addResizerUp(UIElement element)
        {
            if (element == null)
                return;

            connectMouseHandlers(element);
            upElements.Add(element, 0);
        }

        public void addResizerDown(UIElement element)
        {
            if (element == null)
                return;

            connectMouseHandlers(element);
            downElements.Add(element, 0);
        }

        public void addResizerRightDown(UIElement element)
        {
            if (element == null)
                return;

            connectMouseHandlers(element);
            rightElements.Add(element, 0);
            downElements.Add(element, 0);
        }

        public void addResizerLeftDown(UIElement element)
        {
            if (element == null)
                return;

            connectMouseHandlers(element);
            leftElements.Add(element, 0);
            downElements.Add(element, 0);
        }

        public void addResizerRightUp(UIElement element)
        {
            if (element == null)
                return;

            connectMouseHandlers(element);
            rightElements.Add(element, 0);
            upElements.Add(element, 0);
        }

        public void addResizerLeftUp(UIElement element)
        {
            if (element == null)
                return;

            connectMouseHandlers(element);
            leftElements.Add(element, 0);
            upElements.Add(element, 0);
        }
        #endregion

        #region resize handlers
        private void element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            (sender as UIElement).CaptureMouse();
            resizePoint = Mouse.GetPosition(target);            
            resizeSize = new Size(target.Width, target.Height);
            resizeWindowPoint = new Point(target.Left, target.Top);            

            #region updateResizeDirection
            UIElement sourceSender = (UIElement)sender;
            if (leftElements.ContainsKey(sourceSender))
            {
                resizeLeft = true;
            }
            if (rightElements.ContainsKey(sourceSender))
            {
                resizeRight = true;
            }
            if (upElements.ContainsKey(sourceSender))
            {
                resizeUp = true;
            }
            if (downElements.ContainsKey(sourceSender))
            {
                resizeDown = true;
            }
            #endregion

            Thread t = new Thread(new ThreadStart(updateSizeLoop));
            t.Name = "Mouse Position Poll Thread";
            t.Start();
        }

        void element_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            (sender as UIElement).ReleaseMouseCapture();
        }

        private void updateSizeLoop()
        {
            try
            {
                while (resizeDown || resizeLeft || resizeRight || resizeUp)
                {
                    target.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Render, new RefreshDelegate(updateSize));
                    target.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Render, new RefreshDelegate(updateMouseDown));                    
                    Thread.Sleep(10);
                }

                target.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Render, new RefreshDelegate(setArrowCursor));                
            }
            catch (Exception)
            {
            }
        }

        #region updates
        private void updateSize()
        {
            Point p = Mouse.GetPosition(target);
            
            if (resizeRight)
            {
                target.Width = Math.Max(0, this.resizeSize.Width - (resizePoint.X - p.X));                
            }

            if (resizeDown)
            {
                target.Height = Math.Max(0, resizeSize.Height - (resizePoint.Y - p.Y));                
            }

            if (resizeLeft)
            {
                target.Width = Math.Max(0, resizeSize.Width + (resizePoint.X - p.X));
                target.Left = Math.Max(0, resizeWindowPoint.X - (resizePoint.X - p.X));               
            }

            if (resizeUp)
            {
                target.Height = Math.Max(0, resizeSize.Height + (resizePoint.Y - p.Y));
                target.Top = Math.Max(0, resizeWindowPoint.Y - (resizePoint.Y - p.Y));                
            }
        }

        private void updateMouseDown()
        {
            if (Mouse.LeftButton == MouseButtonState.Released)
            {
                resizeRight = false;
                resizeLeft = false;
                resizeUp = false;
                resizeDown = false;
            }
        }
        #endregion
        #endregion

        #region cursor updates
        private void element_MouseEnter(object sender, MouseEventArgs e)
        {
            bool resizeRight = false;
            bool resizeLeft = false;
            bool resizeUp = false;
            bool resizeDown = false;

            var window = (SMWindow)((FrameworkElement)sender).TemplatedParent;

            UIElement sourceSender = (UIElement)sender;

            if (leftElements.ContainsKey(sourceSender))
            {
                resizeLeft = true;
            }
            if (rightElements.ContainsKey(sourceSender))
            {
                resizeRight = true;
            }
            if (upElements.ContainsKey(sourceSender))
            {
                resizeUp = true;
            }
            if (downElements.ContainsKey(sourceSender))
            {
                resizeDown = true;
            }

            if ((resizeLeft && resizeDown) || (resizeRight && resizeUp))
            {
                setNESWCursor(sender, e);
            }
            else if ((resizeRight && resizeDown) || (resizeLeft && resizeUp))
            {
                setNWSECursor(sender, e);
            }
            else if (resizeLeft || resizeRight)
            {
                setWECursor(sender, e);
            }
            else if (resizeUp || resizeDown)
            {
                setNSCursor(sender, e);
            }
        }

        private void setWECursor(object sender, MouseEventArgs e)
        {
            target.Cursor = Cursors.SizeWE;            
        }

        private void setNSCursor(object sender, MouseEventArgs e)
        {
            target.Cursor = Cursors.SizeNS;           
        }

        private void setNESWCursor(object sender, MouseEventArgs e)
        {
            target.Cursor = Cursors.SizeNESW;
            
        }

        private void setNWSECursor(object sender, MouseEventArgs e)
        {
            target.Cursor = Cursors.SizeNWSE;            
        }

        private void setArrowCursor(object sender, MouseEventArgs e)
        {
            if (!resizeDown && !resizeLeft && !resizeRight && !resizeUp)
            {
                target.Cursor = Cursors.Arrow;                
            }
        }

        private void setArrowCursor()
        {
            target.Cursor = Cursors.Arrow;            
        }
        #endregion
    }
}
