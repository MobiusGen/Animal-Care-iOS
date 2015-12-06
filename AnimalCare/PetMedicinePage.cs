using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Collections.Generic;

namespace AnimalCare
{
	partial class PetMedicinePage : UITableViewController
	{
		private PetTabController parentController;
		protected string cellIdentifier = "MedicineCell";

		public PetMedicinePage (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad () {
			base.ViewDidLoad ();
			parentController = this.ParentViewController as PetTabController;
			UIBarButtonItem addButton = new UIBarButtonItem (UIBarButtonSystemItem.Add, addNewMedication);
			parentController.NavigationItem.RightBarButtonItem = addButton;
			TableView.RegisterClassForCellReuse (typeof(UITableViewCell), cellIdentifier);
			TableView.Source = new MedicineTableDataSource(this, cellIdentifier);
			TableView.AllowsMultipleSelectionDuringEditing = false;

		}// ViewDidLoad

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender) {
			base.PrepareForSegue (segue, sender);
			var medDetail = segue.DestinationViewController as MedicineDetailController;
			int row = TableView.IndexPathForSelectedRow.Row;
			medDetail.medication = parentController.pet.medications [row];
		}

		public void addNewMedication(object sender, EventArgs e) {
			Console.WriteLine ("Add new medication");
		}

		public class MedicineTableDataSource : UITableViewSource {
			private PetMedicinePage controller;
			private string cellID;

			public MedicineTableDataSource (PetMedicinePage controller, string cellID) {
				this.controller = controller;
				this.cellID = cellID;
			}
				
			public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath) {
				if (editingStyle == UITableViewCellEditingStyle.Delete) {
					var medList = new List<Medication> (controller.parentController.pet.medications);
					medList.RemoveAt (indexPath.Row);
					controller.parentController.pet.medications = medList.ToArray ();
					tableView.ReloadData ();
				}
			}

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath) {
				UITableViewCell cell = tableView.DequeueReusableCell(cellID);

				if (cell == null) {
					cell = new UITableViewCell (UITableViewCellStyle.Default, cellID);
				}// if
				cell.TextLabel.Text = controller.parentController.pet.medications[indexPath.Row].name;
				cell.Accessory = UITableViewCellAccessory.DetailDisclosureButton;
				return cell;
			}// GetCell

			public override nint NumberOfSections (UITableView tableView) {
				return 1;
			}// NumberOfSections

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath) {
				controller.PerformSegue ("ShowDetailSegue", controller);
			}// RowSelected

			public override nint RowsInSection (UITableView tableview, nint section) {
				return controller.parentController.pet.medications.Length;
			}// RowsInSection

			public override string TitleForHeader (UITableView tableView, nint section) {
				return "Medications";
			}// TitleForHeader
		}

	}// class PetMedicinePage
}// namespace AnimalCare
