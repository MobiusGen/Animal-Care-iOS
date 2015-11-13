using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace AnimalCare {
	
	partial class MainMenuTableController : UITableViewController {

		//static public string[] MenuOptions = { "Pets", "Vets", "Calendar", "Gallery", "Settings" };
		//static public NSString CellIdentifier = new NSString("TableCell"); 

		string[] menuOptions;
		NSString CellIdentifier;

		public MainMenuTableController (IntPtr handle) : base (handle) {
			menuOptions = new string[]{ "Pets", "Vets", "Calendar", "Gallery", "Settings" };
			CellIdentifier = new NSString ("TableCell");
			//TableView.RegisterClassForCellReuse (typeof(UITableViewCell), CellIdentifier);
			TableView.Source = new MainMenuTableSource(this);
		}// public MainMenuTableController

		// The table source
		class MainMenuTableSource : UITableViewSource {

			MainMenuTableController controller;

			public MainMenuTableSource (MainMenuTableController controller) {
				this.controller = controller;
			}

			public override nint RowsInSection (UITableView tableView, nint section) {
				return 3;
				//return controller.menuOptions.Length;
			}// RowsInSection

			public override nint NumberOfSections(UITableView tableView) {
				return 1;
			}// NumberOfSections

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath) {
				//UITableViewCell cell = tableView.DequeueReusableCell(MainMenuTableController.CellIdentifier);
				UITableViewCell newCell = new UITableViewCell(UITableViewCellStyle.Default, "stuff");
				newCell.TextLabel.Text = "Hello!";
				return newCell;
				UITableViewCell cell = null;
				int row = indexPath.Row;
				if (cell == null) {
					cell = new UITableViewCell (UITableViewCellStyle.Default, controller.CellIdentifier);
				}
				cell.TextLabel.Text = controller.menuOptions [row];
				return cell;
			}// GetCell

		}// class MainMenuTableSource
	}// class MainMenuTableController
}// namespace AnimalCare
