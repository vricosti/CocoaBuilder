// IMPORTANT: Read the license included with this code archive.
using System;
using System.Drawing;
using System.Drawing.Design;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Data;

namespace Hosting
{
	public class frmMain : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PropertyGrid propertyGrid;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel pnlViewHost;
		private System.Windows.Forms.Splitter splitter2;
		private ToolboxService lstToolbox;
		private System.Windows.Forms.Label lblSelectedComponent;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem mnuDelete;
		private System.ComponentModel.Container components = null;

		public frmMain()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			Initialize();
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.propertyGrid = new System.Windows.Forms.PropertyGrid();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lstToolbox = new Hosting.ToolboxService();
			this.splitter2 = new System.Windows.Forms.Splitter();
			this.lblSelectedComponent = new System.Windows.Forms.Label();
			this.pnlViewHost = new System.Windows.Forms.Panel();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.mnuDelete = new System.Windows.Forms.MenuItem();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// propertyGrid
			// 
			this.propertyGrid.CommandsVisibleIfAvailable = true;
			this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.propertyGrid.LargeButtons = false;
			this.propertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.propertyGrid.Location = new System.Drawing.Point(0, 203);
			this.propertyGrid.Name = "propertyGrid";
			this.propertyGrid.Size = new System.Drawing.Size(224, 312);
			this.propertyGrid.TabIndex = 0;
			this.propertyGrid.Text = "propertyGrid1";
			this.propertyGrid.ViewBackColor = System.Drawing.SystemColors.Window;
			this.propertyGrid.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// splitter1
			// 
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
			this.splitter1.Location = new System.Drawing.Point(596, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(4, 539);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			// 
			// panel1
			// 
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.lstToolbox,
																				 this.splitter2,
																				 this.propertyGrid,
																				 this.lblSelectedComponent});
			this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel1.Location = new System.Drawing.Point(600, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(224, 539);
			this.panel1.TabIndex = 2;
			// 
			// lstToolbox
			// 
			this.lstToolbox.BackColor = System.Drawing.SystemColors.Control;
			this.lstToolbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstToolbox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstToolbox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lstToolbox.IntegralHeight = false;
			this.lstToolbox.ItemHeight = 16;
			this.lstToolbox.Name = "lstToolbox";
			this.lstToolbox.SelectedCategory = null;
			this.lstToolbox.Size = new System.Drawing.Size(224, 199);
			this.lstToolbox.Sorted = true;
			this.lstToolbox.TabIndex = 2;
			// 
			// splitter2
			// 
			this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitter2.Location = new System.Drawing.Point(0, 199);
			this.splitter2.Name = "splitter2";
			this.splitter2.Size = new System.Drawing.Size(224, 4);
			this.splitter2.TabIndex = 1;
			this.splitter2.TabStop = false;
			// 
			// lblSelectedComponent
			// 
			this.lblSelectedComponent.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.lblSelectedComponent.Location = new System.Drawing.Point(0, 515);
			this.lblSelectedComponent.Name = "lblSelectedComponent";
			this.lblSelectedComponent.Size = new System.Drawing.Size(224, 24);
			this.lblSelectedComponent.TabIndex = 3;
			this.lblSelectedComponent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pnlViewHost
			// 
			this.pnlViewHost.BackColor = System.Drawing.SystemColors.Window;
			this.pnlViewHost.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlViewHost.Name = "pnlViewHost";
			this.pnlViewHost.Size = new System.Drawing.Size(596, 539);
			this.pnlViewHost.TabIndex = 3;
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.mnuDelete});
			this.menuItem1.Text = "&Edit";
			// 
			// mnuDelete
			// 
			this.mnuDelete.Index = 0;
			this.mnuDelete.Shortcut = System.Windows.Forms.Shortcut.Del;
			this.mnuDelete.Text = "&Delete";
			this.mnuDelete.Click += new System.EventHandler(this.mnuDelete_Click);
			// 
			// frmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(824, 539);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.pnlViewHost,
																		  this.splitter1,
																		  this.panel1});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Menu = this.mainMenu1;
			this.Name = "frmMain";
			this.Text = "Designer Hosting Sample";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		[STAThread]
		static void Main() 
		{
			Application.Run(new frmMain());
		}

		private ServiceContainer serviceContainer = null;
		private MenuCommandService menuService = null;

		private void Initialize()
		{
			IDesignerHost host;
			Form form;
			IRootDesigner rootDesigner;
			Control view;

			// Initialise service container and designer host
			serviceContainer = new ServiceContainer();
			serviceContainer.AddService(typeof(INameCreationService), new NameCreationService());
			serviceContainer.AddService(typeof(IUIService), new UIService(this));
			host = new DesignerHost(serviceContainer);

			// Add toolbox service
			serviceContainer.AddService(typeof(IToolboxService), lstToolbox);
			lstToolbox.designPanel = pnlViewHost;
			PopulateToolbox(lstToolbox);

			// Add menu command service
			menuService = new MenuCommandService();
			serviceContainer.AddService(typeof(IMenuCommandService), menuService);

			// Start the designer host off with a Form to design
			form = (Form)host.CreateComponent(typeof(Form));
			form.TopLevel = false;
			form.Text = "Form1";

			// Get the root designer for the form and add its design view to this form
			rootDesigner = (IRootDesigner)host.GetDesigner(form);
			view = (Control)rootDesigner.GetView(ViewTechnology.WindowsForms);
			view.Dock = DockStyle.Fill;
			pnlViewHost.Controls.Add(view);

			// Subscribe to the selectionchanged event and activate the designer
			ISelectionService s = (ISelectionService)serviceContainer.GetService(typeof(ISelectionService));
			s.SelectionChanged += new EventHandler(OnSelectionChanged);
			host.Activate();
		}

		private void PopulateToolbox(IToolboxService toolbox)
		{
			toolbox.AddToolboxItem(new ToolboxItem(typeof(Button)));
			toolbox.AddToolboxItem(new ToolboxItem(typeof(ListView)));
			toolbox.AddToolboxItem(new ToolboxItem(typeof(TreeView)));
			toolbox.AddToolboxItem(new ToolboxItem(typeof(TextBox)));
			toolbox.AddToolboxItem(new ToolboxItem(typeof(Label)));
			toolbox.AddToolboxItem(new ToolboxItem(typeof(TabControl)));
			toolbox.AddToolboxItem(new ToolboxItem(typeof(OpenFileDialog)));
			toolbox.AddToolboxItem(new ToolboxItem(typeof(CheckBox)));
			toolbox.AddToolboxItem(new ToolboxItem(typeof(ComboBox)));
			toolbox.AddToolboxItem(new ToolboxItem(typeof(GroupBox)));
			toolbox.AddToolboxItem(new ToolboxItem(typeof(ImageList)));
			toolbox.AddToolboxItem(new ToolboxItem(typeof(Panel)));
			toolbox.AddToolboxItem(new ToolboxItem(typeof(ProgressBar)));
			toolbox.AddToolboxItem(new ToolboxItem(typeof(ToolBar)));
			toolbox.AddToolboxItem(new ToolboxItem(typeof(ToolTip)));
			toolbox.AddToolboxItem(new ToolboxItem(typeof(StatusBar)));
		}

		private void OnSelectionChanged(object sender, System.EventArgs e)
		{
			ISelectionService s = (ISelectionService)serviceContainer.GetService(typeof(ISelectionService));

			object[] selection;
			if (s.SelectionCount == 0)
				propertyGrid.SelectedObject = null;
			else
			{
				selection = new object[s.SelectionCount];
				s.GetSelectedComponents().CopyTo(selection, 0);
				propertyGrid.SelectedObjects = selection;
			}

			if (s.PrimarySelection == null)
				lblSelectedComponent.Text = "";
			else
			{
				IComponent component = (IComponent)s.PrimarySelection;
				lblSelectedComponent.Text = component.Site.Name + " (" + component.GetType().Name + ")";
			}
		}

		private void mnuDelete_Click(object sender, System.EventArgs e)
		{
			menuService.GlobalInvoke(StandardCommands.Delete);
		}

	}
}
