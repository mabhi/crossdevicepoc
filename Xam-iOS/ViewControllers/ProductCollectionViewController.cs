using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Collections;
using PortableCode.Models;

namespace Xam_iOS
{
	partial class ProductCollectionViewController : UICollectionViewController
	{
		private UIEdgeInsets SectionInset;
		public IList<Product> Products ;
		public ProductCollectionViewController (IntPtr handle) : base (handle)
		{
			InitialViewSetup ();
		}

		public ProductCollectionViewController(UICollectionViewLayout layout) : base (layout){
			InitialViewSetup ();
		}
			
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
		}

		partial void CancelBBItem_Activated (UIBarButtonItem sender)
		{
			this.DismissViewController(true, null);
		}

		public override nint NumberOfSections (UICollectionView collectionView)
		{
			return 1;
		}

		public override nint GetItemsCount (UICollectionView collectionView, nint section)
		{
			return Products.Count;
		}

		public override UICollectionViewCell GetCell (UICollectionView collectionView, NSIndexPath indexPath)
		{
			var productCell = (ProductListCell)collectionView.DequeueReusableCell (ProductListCell, indexPath);

			Product theProduct = Products [indexPath.Row];

			if (productCell.Selected) {
				productCell.checkMarkImage.Image = UIImage.FromBundle ("placeholder.png"); 
			} else
				productCell.checkMarkImage.Image = null;
			
			productCell.productNameTxtLabel.Text = theProduct.ProductName;

			return productCell;
		}

		private void InitialViewSetup(){

			SectionInset = new UIEdgeInsets (50.0, 20.0, 50.0, 20.0);
		}
	}
}
