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
				productCell.checkMarkImage.Image = UIImage.FromBundle ("delcheck.png"); 
			} else
				productCell.checkMarkImage.Image = null;
			
			productCell.productNameTxtLabel.Text = theProduct.ProductName;

			return productCell;
		}

		public override void ItemHighlighted (UICollectionView collectionView, NSIndexPath indexPath)
		{
			var cell = collectionView.CellForItem (indexPath);
			cell.ContentView.BackgroundColor = UIColor.Yellow;
		}

		public override void ItemUnhighlighted (UICollectionView collectionView, NSIndexPath indexPath)
		{
			var cell = collectionView.CellForItem (indexPath);
			cell.ContentView.BackgroundColor = UIColor.White;
		}

		public override void ItemDeselected(UICollectionView collectionView, NSIndexPath indexPath){
			var cell = collectionView.CellForItem (indexPath) as ProductListCell;
			cell.checkMarkImage.Image = null;
			cell.Selected = false;

		}

		public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath){
			var cell = collectionView.CellForItem (indexPath) as ProductListCell;
			cell.checkMarkImage.Image = UIImage.FromBundle ("delcheck.png");
			cell.Selected = true;
		}

		private void InitialViewSetup(){

			SectionInset = new UIEdgeInsets (50.0, 20.0, 50.0, 20.0);
		}
	}
}
