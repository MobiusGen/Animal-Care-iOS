using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace AnimalCare
{
	partial class PetMenuController : UITableViewController
	{
		protected string cellIdentifier = "PetMenuCell";
		public Pets_Database pets { get; set; }
		public Vet_Database vets { get; set; }

		public PetMenuController (IntPtr handle) : base (handle) {
			pets = new Pets_Database ();
		}// PetMenuController constructor

		public override void ViewDidLoad() {
			base.ViewDidLoad ();
			string[] petMenu;	
			if (pets.getCount () == 0) {
				petMenu = new string[0];
			} else {
				petMenu = pets.getNames ();
			}
			this.TableView.Source = new PetMenuTableSource (this, petMenu, cellIdentifier);
		}// ViewDidLoad

		public class PetMenuTableSource : UITableViewSource {
			private PetMenuController controller;
			private string[] menuItems;
			private string cellID;

			public PetMenuTableSource (PetMenuController controller, string[] items, string cellID) {
				this.controller = controller;
				menuItems = items;
				this.cellID = cellID;
			}// PetMenuTableSource constructor

			public override nint RowsInSection (UITableView tableview, nint section) {
				return menuItems.Length;
			}// RowsInSection

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath) {
				//request a recycled cell to save memory
				UITableViewCell cell = tableView.DequeueReusableCell(cellID);

				//if there are no cells to reuse, create a new one
				if (cell == null) {
					cell = new UITableViewCell (UITableViewCellStyle.Default, cellID);
				}// if
				cell.TextLabel.Text = menuItems[indexPath.Row];
				return cell;
			}// GetCell

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath) {
				string name = GetCell (tableView, indexPath).TextLabel.Text;
				PetTabController petController = controller.Storyboard.InstantiateViewController ("PetMainPage") as PetTabController;
				Pet viewPet = controller.pets.GetPet (name);
				petController.pet = viewPet;
				controller.NavigationController.PushViewController (petController, true);


			}// RowSelected

		}// class PetMenuTableSource

	}// class PetMenuController
}
