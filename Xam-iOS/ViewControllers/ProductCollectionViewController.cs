using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Collections.Generic;
using PortableCode.Models;
using Xam_iOS.ViewController.Constants;
using PortableCode.Services;

namespace Xam_iOS
{
	 partial class ProductCollectionViewController : UICollectionViewController
	{
		private UIEdgeInsets SectionInset;
		public List<Product> Products ;
		public Customer TargetCustomer{get; set;}

		public ProductCollectionViewController (IntPtr handle) : base (handle)
		{
			 InitialViewSetup ();
		}

		public ProductCollectionViewController(UICollectionViewLayout layout) : base (layout){
			 InitialViewSetup ();
		}
			
		public async override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
			IList<Product> allProducts = await UserWebservice.Instance.GetProductsForCountryAsync(TargetCustomer.OrgUnitEntity.ParentId);
			Products.InsertRange(0, allProducts);
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
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
			var productCell = (ProductListCell)collectionView.DequeueReusableCell (CellIdentifierConstants.ProductListCellIdentifier, indexPath);

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

			SectionInset = new UIEdgeInsets (50, 20, 50, 20);
			Products = new List<Product>();
		}
	}
}
