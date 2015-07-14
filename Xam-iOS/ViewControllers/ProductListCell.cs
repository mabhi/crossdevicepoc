
using System;

using Foundation;
using UIKit;

namespace Xam_iOS
{
	public class ProductListCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("ProductListCell");

		public ProductListCell () : base (UITableViewCellStyle.Value1, Key)
		{
			// TODO: add subviews to the ContentView, set various colors, etc.
			TextLabel.Text = "TextLabel";
		}
	}
}

