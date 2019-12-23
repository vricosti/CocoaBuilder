using System;
using System.Collections.Generic;
using System.Linq;

namespace XibBuilder
{
    internal class SelectionService
    {
        private DesignerCanvas designerCanvas;

        private List<ISelectable> currentSelection;
        internal List<ISelectable> CurrentSelection
        {
            get
            {
                if (currentSelection == null)
                    currentSelection = new List<ISelectable>();

                return currentSelection;
            }
        }

        public SelectionService(DesignerCanvas canvas)
        {
            this.designerCanvas = canvas;
        }

        internal void SelectItem(ISelectable item)
        {
            this.ClearSelection();
            this.AddToSelection(item);
        }

        internal void AddToSelection(ISelectable item)
        {
            item.IsSelected = true;
            CurrentSelection.Add(item);
        }

        internal void RemoveFromSelection(ISelectable item)
        {
            item.IsSelected = false;
            CurrentSelection.Remove(item);
        }

        internal void ClearSelection()
        {
            CurrentSelection.ForEach(item => item.IsSelected = false);
            CurrentSelection.Clear();
        }

        internal void SelectAll()
        {
            ClearSelection();
            CurrentSelection.AddRange(designerCanvas.Children.OfType<ISelectable>());
            CurrentSelection.ForEach(item => item.IsSelected = true);
        }    
    }
}
