using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace AnimalCare
{
	partial class PetMenuController : UITableViewController
	{
		public PetMenuController (IntPtr handle) : base (handle) {
		}// PetMenuController constructor

		public override void ViewDidLoad() {
			base.ViewDidLoad ();
			string[] petMenu = new string[] { "Cookie", "Spot", "Fluffy" };
			this.TableView.Source = new PetMenuTableSource (petMenu);
		}

		public class PetMenuTableSource : UITableViewSource {
			protected string[] menuItems;
			protected string cellIdentifier = "PetMenuCell";

			public PetMenuTableSource (string[] items) {
				menuItems = items;
			}// PetMenuTableSource constructor

			public override nint RowsInSection (UITableView tableview, nint section) {
				return menuItems.Length;
			}// RowsInSection

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath) {
				//request a recycled cell to save memory
				UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);

				//if there are no cells to reuse, create a new one
				if (cell == null) {
					cell = new UITableViewCell (UITableViewCellStyle.Default, cellIdentifier);
				}// if
				cell.TextLabel.Text = menuItems[indexPath.Row];
				return cell;
			}// GetCell

		}// class PetMenuTableSource

	}// class PetMenuController
}
