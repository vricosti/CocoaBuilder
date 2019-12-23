using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace System.Windows
{
    public static class Extensions
    {
        public static T GetLastDescendantOfType<T>(this DependencyObject start) where T : DependencyObject
        {
            return start.GetDescendantsOfType<T>().LastOrDefault();
        }
        public static T GetFirstDescendantOfType<T>(this DependencyObject start) where T : DependencyObject
        {
            return start.GetDescendantsOfType<T>().FirstOrDefault();
        }

        public static IEnumerable<T> GetDescendantsOfType<T>(this DependencyObject start) where T : DependencyObject
        {
            return start.GetDescendants().OfType<T>();
        }

        public static IEnumerable<DependencyObject> GetDescendants(this DependencyObject start)
        {
            var queue = new Queue<DependencyObject>();
            var count = VisualTreeHelper.GetChildrenCount(start);

            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(start, i);
                yield return child;
                queue.Enqueue(child);
            }

            while (queue.Count > 0)
            {
                var parent = queue.Dequeue();
                var count2 = VisualTreeHelper.GetChildrenCount(parent);

                for (int i = 0; i < count2; i++)
                {
                    var child = VisualTreeHelper.GetChild(parent, i);
                    yield return child;
                    queue.Enqueue(child);
                }
            }
        }

        public static T GetFirstAncestorOfType<T>(this DependencyObject start) where T : DependencyObject
        {
            return start.GetAncestorsOfType<T>().FirstOrDefault();
        }

        public static IEnumerable<T> GetAncestorsOfType<T>(this DependencyObject start) where T : DependencyObject
        {
            return start.GetAncestors().OfType<T>();
        }

        public static IEnumerable<DependencyObject> GetAncestors(this DependencyObject start)
        {
            var parent = VisualTreeHelper.GetParent(start);

            while (parent != null)
            {
                yield return parent;
                parent = VisualTreeHelper.GetParent(parent);
            }
        }


        //public static T FindChild<T>(this DependencyObject parent, string childName) where T : DependencyObject
        //{
        //    if (parent == null) return null;
        //    T foundChild = null;
        //    int childrenCount = VisualTreeHelper.GetChildrenCount(parent);

        //    for (int i = 0; i < childrenCount; i++)
        //    {
        //        var child = VisualTreeHelper.GetChild(parent, i);
        //        T childType = child as T;

        //        if (childType == null)
        //        {
        //            foundChild = FindChild<T>(child, childName);
        //            if (foundChild != null) break;
        //        }
        //        else if (!string.IsNullOrEmpty(childName))
        //        {
        //            var frameworkElement = child as FrameworkElement;
        //            if (frameworkElement != null && frameworkElement.Name == childName)
        //            {
        //                foundChild = (T)child;
        //                break;
        //            }

        //            foundChild = FindChild<T>(child, childName);
        //            if (foundChild != null) break;
        //        }
        //        else
        //        {
        //            foundChild = (T)child;
        //            break;
        //        }
        //    }

        //    return foundChild;
        //}


        public static T FindChild<T>(this DependencyObject depObj, string childName) where T : DependencyObject
        {
            // Confirm obj is valid. 
            if (depObj == null) return null;

            // success case
            if (depObj is T && ((FrameworkElement)depObj).Name == childName)
                return depObj as T;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);

                //DFS
                T obj = FindChild<T>(child, childName);

                if (obj != null)
                    return obj;
            }

            return null;
        }

        //public static T FindChild<T>(this DependencyObject parent, string childName) where T : DependencyObject
        ////public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        //{
        //    // Confirm parent and childName are valid. 
        //    if (parent == null) return null;

        //    T foundChild = null;

        //    int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
        //    for (int i = 0; i < childrenCount; i++)
        //    {
        //        var child = VisualTreeHelper.GetChild(parent, i);
        //        // If the child is not of the request child type child
        //        T childType = child as T;
        //        if (childType == null)
        //        {
        //            // recursively drill down the tree
        //            foundChild = FindChild<T>(child, childName);

        //            // If the child is found, break so we do not overwrite the found child. 
        //            if (foundChild != null) break;
        //        }
        //        else if (!string.IsNullOrEmpty(childName))
        //        {
        //            var frameworkElement = child as FrameworkElement;
        //            // If the child's name is set for search
        //            if (frameworkElement != null && frameworkElement.Name == childName)
        //            {
        //                // if the child's name is of the request name
        //                foundChild = (T)child;
        //                break;
        //            }
        //        }
        //        else
        //        {
        //            // child element found.
        //            foundChild = (T)child;
        //            break;
        //        }
        //    }

        //    return foundChild;
        //}


        //public static T FindChild<T>(this DependencyObject parent, string childName) where T : DependencyObject
        //{
        //    if (parent == null) return null;
        //    T foundChild = null;
        //    int childrenCount = VisualTreeHelper.GetChildrenCount(parent);

        //    for (int i = 0; i < childrenCount; i++)
        //    {
        //        var child = VisualTreeHelper.GetChild(parent, i);
        //        T childType = child as T;

        //        if (childType == null)
        //        {
        //            foundChild = FindChild<T>(child, childName);
        //            if (foundChild != null) break;
        //        }
        //        else if (!string.IsNullOrEmpty(childName))
        //        {
        //            var frameworkElement = child as FrameworkElement;
        //            if (frameworkElement != null && frameworkElement.Name == childName)
        //            {
        //                foundChild = (T)child;
        //                break;
        //            }

        //            foundChild = FindChild<T>(child, childName);
        //            if (foundChild != null) break;
        //        }
        //        else
        //        {
        //            foundChild = (T)child;
        //            break;
        //        }
        //    }

        //    return foundChild;
        //}


    }
}
