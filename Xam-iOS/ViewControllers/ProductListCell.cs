
using System;

using Foundation;
using UIKit;
using CoreGraphics;
using Xam_iOS.ViewController.Constants;

namespace Xam_iOS
{
	//[Register ("ProductListCell")]
	public partial class ProductListCell : UICollectionViewCell
	{
		public static readonly NSString Key = new NSString (CellIdentifierConstants.ProductListCellIdentifier);

		UIImageView imageView;
		public ProductListCell(IntPtr handle) : base (handle){
			BackgroundView = new UIView{BackgroundColor = UIColor.Orange};
			SelectedBackgroundView = new UIView{BackgroundColor = UIColor.Green};

			ContentView.Layer.BorderColor = UIColor.LightGray.CGColor;
			ContentView.Layer.BorderWidth = 2.0f;
			ContentView.BackgroundColor = UIColor.White;
			ContentView.Transform = CGAffineTransform.MakeScale (0.8f, 0.8f);
		}

		[Export ("initWithFrame:")]
		public ProductListCell (CGRect frame) : base (frame)
		{
			BackgroundView = new UIView{BackgroundColor = UIColor.Orange};
			SelectedBackgroundView = new UIView{BackgroundColor = UIColor.Green};

			ContentView.Layer.BorderColor = UIColor.LightGray.CGColor;
			ContentView.Layer.BorderWidth = 2.0f;
			ContentView.BackgroundColor = UIColor.White;
			ContentView.Transform = CGAffineTransform.MakeScale (0.8f, 0.8f);
		}

		public UIImage Image {
			set {
				imageView.Image = value;
			}
		}

	}
}

