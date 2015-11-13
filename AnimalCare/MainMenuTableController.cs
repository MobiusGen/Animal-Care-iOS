using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace AnimalCare {
	
	partial class MainMenuTableController : UITableViewController {

		static public string[] MenuOptions = { "Pets", "Vets", "Calendar", "Gallery", "Settings" };
		static public NSString CellIdentifier = new NSString("TableCell"); 

		public MainMenuTableController (IntPtr handle) : base (handle) {
			TableView.RegisterClassForCellReuse (typeof(UITableViewCell), CellIdentifier);
			TableView.Source = new MainMenuTableSource();
		}// public MainMenuTableController

		// The table source
		class MainMenuTableSource : UITableViewSource {

			public override nint RowsInSection (UITableView tableView, nint section) {
				return MenuOptions.Length;
			}// RowsInSection

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath) {
				var cell = tableView.DequeueReusableCell(MainMenuTableController.CellIdentifier);

				int row = indexPath.Row;
				cell.TextLabel.Text = MainMenuTableController.MenuOptions [row];
				return cell;
			}

		}// class MainMenuTableSource
	}// class MainMenuTableController
}// namespace AnimalCare
