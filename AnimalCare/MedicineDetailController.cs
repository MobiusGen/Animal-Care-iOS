// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;

namespace AnimalCare
{
	public partial class MedicineDetailController : UITableViewController
	{
		private const string cellIdentifier = "MedicineDetailCell";
		public Medication medication { get; set; }

		public MedicineDetailController (IntPtr handle) : base (handle)
		{
		}// MedicineDetailController constructor

		public override void ViewDidLoad () {
			base.ViewDidLoad ();
			UIBarButtonItem editButton = new UIBarButtonItem (UIBarButtonSystemItem.Edit, EditMedication);
			this.NavigationItem.Title = medication.name;
			this.NavigationItem.RightBarButtonItem = editButton;
			TableView.RegisterClassForCellReuse (typeof(UITableViewCell), cellIdentifier);
			TableView.Source = new MedicineDetailTableSource (this, cellIdentifier);
		}

		public void EditMedication (Object sender, EventArgs e) {
			
		}

		public class MedicineDetailTableSource : UITableViewSource {
			private MedicineDetailController controller;
			private string cellID;

			public MedicineDetailTableSource (MedicineDetailController controller, string cellID) {
				this.cellID = cellID;
				this.controller = controller;
			}// MedicineDetailTableSource constructor

			public override void AccessoryButtonTapped (UITableView tableView, NSIndexPath indexPath) {
				Console.WriteLine ("Accessory Button Tapped");
			}

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath) {
				UITableViewCell cell = tableView.DequeueReusableCell (cellID) ?? new UITableViewCell (UITableViewCellStyle.Default, cellID);
				cell.TextLabel.Text = "";
				cell.AccessoryView = null;
				Medication med = controller.medication;
				switch (indexPath.Section) {
				case 0:
					if (indexPath.Row == 0) {
						cell.TextLabel.Text = "Take " + med.frequencyNumber + " " + 
							med.medType + " " + med.frequencyPeriod + " at 9:00 AM";
					} else {
						cell.TextLabel.Text = "Notify me";
						cell.AccessoryView = new UISwitch ();
					}
					break;
				case 1:
					if (indexPath.Row == 0) {
						cell.TextLabel.Text = "20 days left";
					} else {
						cell.TextLabel.Text = "Notify at 5 days";
						cell.AccessoryView = new UISwitch ();
					}
					break;
				case 2:
					if (indexPath.Row == 0) {
						cell.TextLabel.Text = med.pharmacyAddress;
						cell.TextLabel.Lines = 4;//controller.medication.getPharmacyAddressLines ();
					} else {
						cell.TextLabel.Text = "Get Directions";
					}
					break;
				default:
					break;
				}
				return cell;
			}// GetCell

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath) {
				if (indexPath.Section == 2 && indexPath.Row == 0) {
					UILabel testLabel = new UILabel ();
					testLabel.Text = "Doggy Pharma\n1234 Woof Ave.\nArlington, TX 12345";
					testLabel.Lines = 4;//controller.medication.getPharmacyAddressLines ();
					if (testLabel.IntrinsicContentSize.Height > tableView.RowHeight)
						return testLabel.IntrinsicContentSize.Height;
				}
				return tableView.RowHeight;
			}// GetHeightForRow

			public override nint NumberOfSections (UITableView tableView) {
				return 3;
			}// NumberOfSections

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath) {
				if (indexPath.Section == 2 && indexPath.Row == 1) {
					var url = new NSUrl("http://maps.apple.com/?daddr=" + "Doggy+Pharma+1234+Woof+Ave.+Arlington,+TX+12345" + "&dirflg=d");
					UIApplication.SharedApplication.OpenUrl(url);
				}
			}

			public override nint RowsInSection (UITableView tableview, nint section) {
				return 2;
			}// RowsInSection

			public override bool ShouldHighlightRow (UITableView tableView, NSIndexPath rowIndexPath) {
				return true;
			}

			public override string TitleForHeader (UITableView tableView, nint section) {
				switch (section) {
				case 0:
					return "Instructions";
				case 1:
					return "Prescription";
				case 2:
					return "Pharmacy";
				default:
					return "";
				}
			}// TitleForHeader

		}// class MedicineDetailTableSource
	}// class MedicineDetailController
}// namespace AnimalCare
