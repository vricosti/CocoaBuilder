// IMPORTANT: Read the license included with this code archive.
using System;
using System.Windows.Forms;
using System.Drawing.Design;

namespace Hosting
{
	internal class ToolboxService : ListBox, IToolboxService
	{
		internal Control designPanel = null;

		public ToolboxService()
		{
		}

		public void AddCreator(System.Drawing.Design.ToolboxItemCreatorCallback creator, string format, System.ComponentModel.Design.IDesignerHost host)
		{
			// No implementation
		}

		public void AddCreator(System.Drawing.Design.ToolboxItemCreatorCallback creator, string format)
		{
			// No implementation
		}

		public void AddLinkedToolboxItem(System.Drawing.Design.ToolboxItem toolboxItem, string category, System.ComponentModel.Design.IDesignerHost host)
		{
			// No implementation
		}

		public void AddLinkedToolboxItem(System.Drawing.Design.ToolboxItem toolboxItem, System.ComponentModel.Design.IDesignerHost host)
		{
			// No implementation
		}

		public void AddToolboxItem(System.Drawing.Design.ToolboxItem toolboxItem, string category)
		{
			AddToolboxItem(toolboxItem);
		}

		public void AddToolboxItem(System.Drawing.Design.ToolboxItem toolboxItem)
		{
			Items.Add(toolboxItem);
		}

		public System.Drawing.Design.ToolboxItem DeserializeToolboxItem(object serializedObject, System.ComponentModel.Design.IDesignerHost host)
		{
			return null;
		}

		public System.Drawing.Design.ToolboxItem DeserializeToolboxItem(object serializedObject)
		{
			return null;
		}

		public System.Drawing.Design.ToolboxItem GetSelectedToolboxItem(System.ComponentModel.Design.IDesignerHost host)
		{
			return GetSelectedToolboxItem();
		}

		public System.Drawing.Design.ToolboxItem GetSelectedToolboxItem()
		{
			if (base.SelectedIndex == -1)
				return null;
			else
				return (ToolboxItem)base.SelectedItem;
		}

		public System.Drawing.Design.ToolboxItemCollection GetToolboxItems(string category, System.ComponentModel.Design.IDesignerHost host)
		{
			return GetToolboxItems();
		}

		public System.Drawing.Design.ToolboxItemCollection GetToolboxItems(string category)
		{
			return GetToolboxItems();
		}

		public System.Drawing.Design.ToolboxItemCollection GetToolboxItems(System.ComponentModel.Design.IDesignerHost host)
		{
			return GetToolboxItems();
		}

		public System.Drawing.Design.ToolboxItemCollection GetToolboxItems()
		{
			ToolboxItem[] t = new ToolboxItem[Items.Count];
			Items.CopyTo(t, 0);
			
			return new ToolboxItemCollection(t);
		}

		public bool IsSupported(object serializedObject, System.Collections.ICollection filterAttributes)
		{
			return false;
		}

		public bool IsSupported(object serializedObject, System.ComponentModel.Design.IDesignerHost host)
		{
			return false;
		}

		public bool IsToolboxItem(object serializedObject, System.ComponentModel.Design.IDesignerHost host)
		{
			return false;
		}

		public bool IsToolboxItem(object serializedObject)
		{
			return false;
		}

//		public void Refresh()
//		{
//			base.Refresh();
//		}

		public void RemoveCreator(string format, System.ComponentModel.Design.IDesignerHost host)
		{
			// No implementation
		}

		public void RemoveCreator(string format)
		{
			// No implementation
		}

		public void RemoveToolboxItem(System.Drawing.Design.ToolboxItem toolboxItem, string category)
		{
			RemoveToolboxItem(toolboxItem);
		}

		public void RemoveToolboxItem(System.Drawing.Design.ToolboxItem toolboxItem)
		{
			Items.Remove(toolboxItem);
		}

		public void SelectedToolboxItemUsed()
		{
			base.SelectedIndex = -1;
		}

		public object SerializeToolboxItem(System.Drawing.Design.ToolboxItem toolboxItem)
		{
			return null;
		}

		public bool SetCursor()
		{
			if (base.SelectedIndex == -1)
				designPanel.Cursor = Cursors.Default;
			else
				designPanel.Cursor = Cursors.Cross;

			return true;
		}

		public void SetSelectedToolboxItem(System.Drawing.Design.ToolboxItem toolboxItem)
		{
			base.SelectedItem = toolboxItem;
		}

		public System.Drawing.Design.CategoryNameCollection CategoryNames
		{
			get
			{
				return null;
			}
		}

		public string SelectedCategory
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		private bool ShouldSerializeItems()
		{
			return false;
		}
	}
}
