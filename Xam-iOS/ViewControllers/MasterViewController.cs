using System;
using System.Drawing;
using System.Collections.Generic;
using Foundation;
using UIKit;
using PortableCode.Services;
using PortableCode.Models;
using System.Threading.Tasks;

namespace Xam_iOS
{
    public partial class MasterViewController : UITableViewController
    {
        DataSource dataSource;
//        static bool isLogged;
        public MasterViewController(IntPtr handle)
            : base(handle)
        {
            Title = NSBundle.MainBundle.LocalizedString("Master", "Master");
            // Custom initialization
        }

        void AddNewItem(object sender, EventArgs args)
        {
			/*
            dataSource.Objects.Insert(0, DateTime.Now);

            using (var indexPath = NSIndexPath.FromRowSection(0, 0))
                TableView.InsertRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Automatic);
                */
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
            var addButton = new UIBarButtonItem(UIBarButtonSystemItem.Add, AddNewItem);
            NavigationItem.RightBarButtonItem = addButton;

            TableView.Source = dataSource = new DataSource(this);

        }

        public async override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
			if (null == UserWebservice.Instance.CurrentUser)
 			{//            if(!isLogged)
				this.NavigationController.PerformSegue ("ShowLoginViewControllerIdentifier", this);
				//               isLogged = true;
			} else {
				await GetAllUsersForCurrentTerritory ();
				TableView.ReloadData ();
			}
        }


        class DataSource : UITableViewSource
        {
            static readonly NSString CellIdentifier = new NSString("CustomerCell");
			public List<Customer> Objects { get; set; }
            readonly MasterViewController controller;

            public DataSource(MasterViewController controller)
            {
                this.controller = controller;
            }
			/*
            public IList<object> Objects
            {
                get { return objects; }
            }
			*/
            // Customize the number of sections in the table view.
            public override nint NumberOfSections(UITableView tableView)
            {
                return 1;
            }

            public override nint RowsInSection(UITableView tableview, nint section)
            {
				return null == Objects ? 0 : Objects.Count;
            }

            // Customize the appearance of table view cells.
            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var cell = (UITableViewCell)tableView.DequeueReusableCell(CellIdentifier, indexPath);
				Customer theCustomer = Objects [indexPath.Row];
				cell.TextLabel.Text = theCustomer.CustomerName;

                return cell;
            }

            public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
            {
                // Return false if you do not want the specified item to be editable.
                return true;
            }

            public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
            {
                if (editingStyle == UITableViewCellEditingStyle.Delete)
                {
                    // Delete the row from the data source.
                    Objects.RemoveAt(indexPath.Row);
                    controller.TableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
                }
                else if (editingStyle == UITableViewCellEditingStyle.Insert)
                {
                    // Create a new instance of the appropriate class, insert it into the array, and add a new row to the table view.
                }
            }
            /*
            // Override to support rearranging the table view.
            public override void MoveRow (UITableView tableView, NSIndexPath sourceIndexPath, NSIndexPath destinationIndexPath)
            {
            }
            */
            /*
            // Override to support conditional rearranging of the table view.
            public override bool CanMoveRow (UITableView tableView, NSIndexPath indexPath)
            {
                // Return false if you do not want the item to be re-orderable.
                return true;
            }
            */
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "showDetail")
            {
                var indexPath = TableView.IndexPathForSelectedRow;
                var item = dataSource.Objects[indexPath.Row];

               // ((DetailViewController)segue.DestinationViewController).SetDetailItem(item);
            }
            else if (segue.Identifier == "ShowLoginViewControllerIdentifier")
            {
				UserWebservice.Instance.InvalidateCurrentUser();
				dataSource.Objects.RemoveRange (0, dataSource.Objects.Count);
				TableView.ReloadData ();
            }
        }

		private async Task GetAllUsersForCurrentTerritory(){
			User theUser = UserWebservice.Instance.CurrentUser;
			List<Customer> allCustomers = await UserWebservice.Instance.GetCustomersInTerritoryAsync (theUser.TerritoryId);
			dataSource.Objects = allCustomers;
		}
    }
}
