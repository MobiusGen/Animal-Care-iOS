using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace AnimalCare
{
	partial class SettingsController : UITableViewController
	{
		protected string cellIdentifier = "SettingsCell";

		public SettingsController (IntPtr handle) : base (handle) {
		}// SettingsController constructor

		public override void ViewDidLoad() {
			base.ViewDidLoad ();

			this.TableView.Source = new SettingsTableSource (this, cellIdentifier);
		}// ViewDidLoad

		public class SettingsTableSource : UITableViewSource {
			protected string cellID;
			protected UITableViewController controller;

			public SettingsTableSource (UITableViewController controller, string cellID) {
				this.cellID = cellID;
				this.controller = controller;
			}// SettingsTableSource constructor

			public override nint RowsInSection (UITableView tableView, nint section) {
				switch (section) {
				case 0:
					return 2;
				case 1:
					return 3;
				case 2:
					return 1;
				default:
					return 0;
				}//switch
			}// RowsInSection

			public override nint NumberOfSections (UITableView tableView) {
				return 3;
			}// NumberOfSections

			public override string TitleForHeader (UITableView tableView, nint section) {
				switch (section) {
				case 0:
					return "";
				case 1:
					return "Prescription Refills";
				default:
					return " ";
				}//switch
			}// TitleForHeader

			public override bool ShouldHighlightRow (UITableView tableView, NSIndexPath indexPath) {
				return false;
			}// ShouldHighlightRow

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath) {
				if (indexPath.Section == 1) {
					if (indexPath.Row == 1) {
						//UIDatePicker refillTimePicker = new UIDatePicker ();
						//refillTimePicker.Mode = UIDatePickerMode.Time;
						//controller.PresentViewControllerAsync (refillTimePicker, true);
					}//if
				}//if
			}// RowSelected

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath) {
				//request a recycled cell to save memory
				UITableViewCell cell = tableView.DequeueReusableCell (cellID);

				//if there are no cells to reuse, create a new one
				if (cell == null) {
					cell = new UITableViewCell (UITableViewCellStyle.Default, cellID);
				}//if

				switch (indexPath.Section) {
				case 0:
					if (indexPath.Row == 0) {
						cell.TextLabel.Text = "Appointment Reminders";
						UISwitch appointmentSwitch = new UISwitch ();
						appointmentSwitch.On = true;
						cell.AccessoryView = appointmentSwitch;
					} else {// row == 1
						cell.TextLabel.Text = "Medication Reminders";
						UISwitch medicationSwitch = new UISwitch ();
						medicationSwitch.On = false;
						cell.AccessoryView = medicationSwitch;
					}
					break;
				case 1:
					if (indexPath.Row == 0) {
						cell.TextLabel.Text = "Refill Reminders";
						UISwitch refillSwitch = new UISwitch ();
						refillSwitch.On = true;
						cell.AccessoryView = refillSwitch;
					} else if (indexPath.Row == 1) {
						cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
						cell.TextLabel.Text = "Remind 5 days before refills";
					} else {// row == 2
						cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
						cell.TextLabel.Text = "At 6 PM";
					}
					break;
				default: //case 2
					cell.TextLabel.Text = "Metric Measurements";
					UISwitch metricSwitch = new UISwitch ();
					metricSwitch.On = false;
					cell.AccessoryView = metricSwitch;
					break;
				}//switch
				return cell;
			}// GetCell

		}// class SettingsTableSource

	}// class SettingsController
}// namespace AnimalCare
