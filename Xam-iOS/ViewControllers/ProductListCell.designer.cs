// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Xam_iOS
{
	[Register ("ProductListCell")]
	partial class ProductListCell
	{
		[Outlet]
		public UIKit.UIImageView checkMarkImage { get; private set; }

		[Outlet]
		public UIKit.UILabel productNameTxtLabel { get; private set; }

		[Outlet]
		public UIKit.UILabel productPriceTxtLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (checkMarkImage != null) {
				checkMarkImage.Dispose ();
				checkMarkImage = null;
			}

			if (productPriceTxtLabel != null) {
				productPriceTxtLabel.Dispose ();
				productPriceTxtLabel = null;
			}

			if (productNameTxtLabel != null) {
				productNameTxtLabel.Dispose ();
				productNameTxtLabel = null;
			}
		}
	}
}
