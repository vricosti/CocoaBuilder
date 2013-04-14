// IMPORTANT: Read the license included with this code archive.
using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace Hosting
{
	public class SelectionService : ISelectionService
	{
		IDesignerHost host = null;
		ArrayList selectedComponents = null;
		
		public event EventHandler SelectionChanging;
		public event EventHandler SelectionChanged;
		
		public SelectionService(IDesignerHost host)
		{
			this.host = host;

			selectedComponents = new ArrayList();

			// Subscribe to the componentremoved event
			IComponentChangeService c = (IComponentChangeService)host.GetService(typeof(IComponentChangeService));
			c.ComponentRemoved += new ComponentEventHandler(OnComponentRemoved);
		}
		
		public ICollection GetSelectedComponents() 
		{
			return selectedComponents.ToArray();
		}

		internal void OnSelectionChanging(EventArgs e)
		{
			// Fire the SelectionChanging event if anything is bound to it
			if (SelectionChanging != null) 
				SelectionChanging(this, e);
		}
		
		internal void OnSelectionChanged(EventArgs e)
		{
			// Fire the SelectionChanging event if anything is bound to it
			if (SelectionChanged != null) 
				SelectionChanged(this, e);
		}

		public object PrimarySelection 
		{
			get 
			{
				// If we have any selected components, return the first
				if (selectedComponents.Count > 0) 
					return selectedComponents[0];

				return null;
			}
		}
		
		public int SelectionCount 
		{
			get 
			{
				return selectedComponents.Count;
			}
		}
		
		public bool GetComponentSelected(object component) 
		{
			return selectedComponents.Contains(component);
		}
		
		public void SetSelectedComponents(ICollection components, SelectionTypes selectionType) 
		{
			bool control = false;
			bool shift = false;

			// Raise selectionchanging event
			if (SelectionChanging != null)
				SelectionChanging(this, EventArgs.Empty);

			// If we're being passed an empty collection
			if (components == null || components.Count == 0) 
				components = new object[1] { host.RootComponent };
			
			// If the selection type is Click, we want to know if shift or control is being held
			if ((selectionType & SelectionTypes.Click) == SelectionTypes.Click)
			{
				control = ((Control.ModifierKeys & Keys.Control) == Keys.Control);
				shift = ((Control.ModifierKeys & Keys.Shift)   == Keys.Shift);
			}

			if (selectionType == SelectionTypes.Replace)
			{
				// Simply replace our existing collection with the new one
				selectedComponents.Clear();
				foreach (object component in components)
				{
					if (component != null && !selectedComponents.Contains(component))
						selectedComponents.Add(component);
				}
			}
			else
			{
				// Clear selection if ctrl or shift isn't pressed
				if (!control && !shift && components.Count == 1)
				{
					foreach(object component in components)
					{
						if (!selectedComponents.Contains(component))
							selectedComponents.Clear();
					}
				}

				// Add or remove each component to or from the selection
				foreach (object component in components)
				{
					if (component != null)
					{
						if (control || shift)
						{
							if (selectedComponents.Contains(component))
								selectedComponents.Remove(component);
							else
								selectedComponents.Insert(0, component);
						}
						else
						{
							if (!selectedComponents.Contains(component))
								selectedComponents.Add(component);
							else
							{
								selectedComponents.Remove(component);
								selectedComponents.Insert(0, component);
							}
						}
					}
				}
			}

			// Raise the selectionchanged event
			if (SelectionChanged != null)
				SelectionChanged(this, EventArgs.Empty);
		}
		
		public void SetSelectedComponents(ICollection components) 
		{
			// Use the Replace selection type because this needs to replace anything already selected
			SetSelectedComponents(components, SelectionTypes.Replace);
		}
		
		internal void OnComponentRemoved(object sender, ComponentEventArgs e)
		{
			if (selectedComponents.Contains(e.Component)) 
			{
				// Raise the selectionchanging event
				OnSelectionChanging(EventArgs.Empty);

				// Remove this component from the selected components
				selectedComponents.Remove(e.Component);
				
				// Select root component if that leaves us with no selected components
				if (SelectionCount == 0) 
					selectedComponents.Add(host.RootComponent);

				// Raise the selectionchanged event
				OnSelectionChanged(EventArgs.Empty);
			}
		}
		
	}
}
