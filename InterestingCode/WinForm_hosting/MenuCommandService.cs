// IMPORTANT: Read the license included with this code archive.
using System;
using System.ComponentModel.Design;
using System.Collections;

namespace Hosting
{
	internal class MenuCommandService : IMenuCommandService
	{
		ArrayList menuCommands = null;

		public MenuCommandService()
		{
			menuCommands = new ArrayList();
		}

		public void AddCommand(System.ComponentModel.Design.MenuCommand command)
		{
			menuCommands.Add(command);
		}

		public void AddVerb(System.ComponentModel.Design.DesignerVerb verb)
		{
			// No implementation
		}

		public System.ComponentModel.Design.MenuCommand FindCommand(System.ComponentModel.Design.CommandID commandID)
		{
			return null;
		}

		public bool GlobalInvoke(System.ComponentModel.Design.CommandID commandID)
		{
			foreach(MenuCommand command in menuCommands)
			{
				if (command.CommandID == commandID)
				{
					command.Invoke();
					break;
				}
			}

			return false;
		}

		public void RemoveCommand(System.ComponentModel.Design.MenuCommand command)
		{
			menuCommands.Remove(command);
		}

		public void RemoveVerb(System.ComponentModel.Design.DesignerVerb verb)
		{
			// No implementation
		}

		public void ShowContextMenu(System.ComponentModel.Design.CommandID menuID, int x, int y)
		{
			// No implementation
		}

		public System.ComponentModel.Design.DesignerVerbCollection Verbs
		{
			get
			{
				return new DesignerVerbCollection();
			}
		}
	}
}
