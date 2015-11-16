using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace AnimalCare
{
	partial class VetMenuController : UITableViewController
	{
		protected string cellIdentifier = "VetMenuCell";

		public VetMenuController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad() {
			base.ViewDidLoad ();
			string[] vetMenu = new string[] { "Dr. Strangelove", "Dr. Bob" };
			this.TableView.RegisterClassForCellReuse (typeof(UITableViewCell), cellIdentifier);
			this.TableView.Source = new VetMenuTableSource (vetMenu, cellIdentifier);
		}// ViewDidLoad

		public class VetMenuTableSource : UITableViewSource {
			protected string[] menuItems;
			protected string cellID;

			public VetMenuTableSource (string[] items, string cellID) {
				menuItems = items;
				this.cellID = cellID;
			}// VetMenuTableSource constructor

			public override nint RowsInSection (UITableView tableview, nint section) {
				return menuItems.Length;
			}// RowsInSection

			public override UITableViewCell GetCell (UITableView tableview, NSIndexPath indexPath) {
				//request a recycled cell to save memory
				UITableViewCell cell = tableview.DequeueReusableCell(cellID);

				//if there are no cells to reuse, create a new one
				if (cell == null) {
					cell = new UITableViewCell (UITableViewCellStyle.Default, cellID);
				}// if
				cell.TextLabel.Text = menuItems[indexPath.Row];
				return cell;
			}// GetCell

		}// class VetMenuTableSource

	}// class VetMenuController
}// namespace AnimalCare
