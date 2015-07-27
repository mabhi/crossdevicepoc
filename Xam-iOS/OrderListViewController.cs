using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Xam_iOS
{
	partial class OrderListViewController : UITableViewController
	{
		public OrderListViewController (IntPtr handle) : base (handle)
		{
		}

		void SaveOrder(object sender, EventArgs args)
		{
		}

		public override void ViewDidLoad(){
			base.ViewDidLoad ();
			var saveButton = new UIBarButtonItem(UIBarButtonSystemItem.Save, SaveOrder);
			NavigationItem.RightBarButtonItem = saveButton;

		}

	}
}
