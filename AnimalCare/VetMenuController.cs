using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace AnimalCare
{
	partial class VetMenuController : UITableViewController
	{
		protected string cellIdentifier = "VetMenuCell";
		public Vet_Database vets;

		public VetMenuController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad() {
			base.ViewDidLoad ();
			UIBarButtonItem newVet = new UIBarButtonItem (UIBarButtonSystemItem.Add, createNewVet);
			this.NavigationItem.Title = "Vets";
			this.NavigationItem.RightBarButtonItem = newVet;
			string[] vetMenu = vets.getNames ();
			this.TableView.RegisterClassForCellReuse (typeof(UITableViewCell), cellIdentifier);
			this.TableView.Source = new VetMenuTableSource (this, vetMenu, cellIdentifier);
		}// ViewDidLoad

		public void createNewVet (Object sender, EventArgs e) {
			
		}

		public class VetMenuTableSource : UITableViewSource {
			private string[] menuItems;
			protected string cellID;
			private VetMenuController controller;

			public VetMenuTableSource (VetMenuController controller, string[] items, string cellID) {
				menuItems = items;
				this.controller = controller;
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

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath) {
				controller.TableView.DeselectRow (indexPath, true);
				VetViewController vetController = controller.Storyboard.InstantiateViewController ("VetProfile") as VetViewController;
				string name = GetCell (tableView, indexPath).TextLabel.Text;
				Vet newVet = controller.vets.GetVet (name);
				vetController.vet = newVet;
				controller.NavigationController.PushViewController (vetController, true);
			}

		}// class VetMenuTableSource

	}// class VetMenuController
}// namespace AnimalCare
