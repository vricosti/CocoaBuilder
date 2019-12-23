// IMPORTANT: Read the license included with this code archive.
using System;
using System.ComponentModel.Design;

namespace Hosting
{
	internal class MegaDesignerTransaction : DesignerTransaction
	{
		private DesignerHost host = null;

		protected override void OnCommit()
		{
			// Fire the transaction events on the designer host
			host.OnTransactionClosing(true);
			host.OnTransactionClosed(true);
		}

		protected override void OnCancel()
		{
			// Fire the transaction events on the designer host
			host.OnTransactionClosing(false);
			host.OnTransactionClosed(false);
		}

		public MegaDesignerTransaction(DesignerHost host) : base()
		{
			// Record a reference to the designer host, as we need this in order to tell it the transaction has been completed
			this.host = host;
		}

		public MegaDesignerTransaction(DesignerHost host, string description) : base(description)
		{
			// Record a reference to the designer host, as we need this in order to tell it the transaction has been completed
			this.host = host;
		}

	}
}
