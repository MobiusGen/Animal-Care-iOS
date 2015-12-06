using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace AnimalCare
{
	partial class SettingsController : UITableViewController
	{
		private const string cellIdentifier = "SettingsCell";

		public SettingsController (IntPtr handle) : base (handle) {
		}// SettingsController constructor

		public override void ViewDidLoad() {
			base.ViewDidLoad ();
			this.TableView.RegisterClassForCellReuse (typeof(UITableViewCell), cellIdentifier);
			this.TableView.Source = new SettingsTableSource (this, cellIdentifier);
		}// ViewDidLoad

		public class SettingsTableSource : UITableViewSource {
			protected const int TIME_PICKER_ROW = 3;
			protected const int DAY_PICKER_ROW = 2;
			protected const int PICKER_SECTION = 1;

			protected string cellID;
			protected UITableViewController controller;
			protected UIDatePicker timePicker;
			protected UIPickerView dayPicker;
			protected bool timePickerIsShowing, dayPickerIsShowing;
			protected NSDateFormatter dateFormatter;
			protected int dayPickerDay;
			protected string dayPickerUnit;

			public SettingsTableSource (UITableViewController controller, string cellID) {
				this.cellID = cellID;
				this.controller = controller;

				// Set up the NSDateFormatter
				this.dateFormatter = new NSDateFormatter();
				this.dateFormatter.DateStyle = NSDateFormatterStyle.None;
				this.dateFormatter.TimeStyle = NSDateFormatterStyle.Short;

				// Set up the UIDatePicker
				this.timePicker = new UIDatePicker();
				timePicker.Mode = UIDatePickerMode.Time;
				timePicker.Date = NSDate.Now;
				timePicker.Hidden = true;
				this.timePickerIsShowing = false;
				this.dayPickerDay = 1;
				this.dayPickerUnit = "Days";

				// Set up the UIPickerView
				this.dayPicker = new UIPickerView();
				this.dayPicker.DataSource = new DayPickerSource();
				this.dayPicker.Delegate = new DayPickerDelegate(this);
				this.dayPicker.Hidden = true;
				this.dayPickerIsShowing = false;
			}// SettingsTableSource constructor
				
			/// <summary>
			/// Toggles the time picker cell on or off.
			/// If any other picker cells are on, turns them off.
			/// </summary>
			/// <param name="tableView">The UITableView the cell is associated with.</param>
			private void toggleTimePickerCell(UITableView tableView) {
				// Toggle the timePicker cell
				this.timePicker.Hidden = this.timePickerIsShowing;
				this.timePickerIsShowing = !this.timePickerIsShowing;

				// Close any other picker cells
				if (this.dayPickerIsShowing)
					toggleDayPickerCell (tableView);

				// Reload the section
				NSIndexSet sections = new NSIndexSet (PICKER_SECTION);
				tableView.ReloadSections (sections, UITableViewRowAnimation.Fade);
			}// toggleTimePickerCell

			/// <summary>
			/// Toggles the day picker cell.
			/// If any other picker cells are on, turns them off.
			/// </summary>
			/// <param name="tableView">The UITableView the cell is associated with.</param>
			private void toggleDayPickerCell(UITableView tableView) {
				Console.WriteLine ("Toggling Day Picker Cell");
				// Toggle the dayPicker cell
				this.dayPicker.Hidden = this.dayPickerIsShowing;
				this.dayPickerIsShowing = !this.dayPickerIsShowing;

				// Close any other picker cells
				if (this.timePickerIsShowing)
					toggleTimePickerCell (tableView);

				// Reload the section
				NSIndexSet sections = new NSIndexSet (PICKER_SECTION);
				tableView.ReloadSections (sections, UITableViewRowAnimation.Fade);
			}// toggleDayPickerCell

			public override nint RowsInSection (UITableView tableView, nint section) {
				switch (section) {
				case 0:
					return 2;
				case 1: // PICKER_SECTION
					if (timePickerIsShowing || dayPickerIsShowing)
						return 4;
					else
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
					return "Section 1";
				case 1:
					return "Prescription Refills";
				default:
					return "Section 3";
				}//switch
			}// TitleForHeader

			public override bool ShouldHighlightRow (UITableView tableView, NSIndexPath indexPath) {
				if (indexPath.Section == PICKER_SECTION) { // PICKER_SECTION = 1
					if (indexPath.Row == 1) // DAY_PICKER_ROW = 2
						return true;
					else if (indexPath.Row == 2) // TIME_PICKER_ROW = 3
						return !dayPickerIsShowing;
					else if (indexPath.Row == 3)
						return dayPickerIsShowing;
				}
				return false;
			}// ShouldHighlightRow

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath) {
				tableView.DeselectRow (indexPath, true);
				if (indexPath.Section == PICKER_SECTION) {
					int row = indexPath.Row;
					if (dayPickerIsShowing) {
						if (row >= DAY_PICKER_ROW)
							row--;
					}// if
					if (row == DAY_PICKER_ROW - 1)
						toggleDayPickerCell (tableView);
					else if (row == TIME_PICKER_ROW - 1)
						toggleTimePickerCell (tableView);
				}//if
			}// RowSelected

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath) {
				nfloat rowHeight = tableView.RowHeight;
				if (this.timePickerIsShowing) {
					if (indexPath.Section == PICKER_SECTION && indexPath.Row == TIME_PICKER_ROW) {
						rowHeight = this.timePicker.Bounds.Height;
						Console.WriteLine ("Time picker height: " + rowHeight);
						//rowHeight = 216;
					}
				} else if (this.dayPickerIsShowing) {
					if (indexPath.Section == PICKER_SECTION && indexPath.Row == DAY_PICKER_ROW) {
						rowHeight = this.dayPicker.Bounds.Height;
						Console.WriteLine ("Day picker height: " + rowHeight);
					}
				}
				return rowHeight;
			}

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
					int row = indexPath.Row;
					if (row == 0) {
						cell.TextLabel.Text = "Refill Reminders";
						cell.TextLabel.Hidden = false;
						UISwitch refillSwitch = new UISwitch ();
						refillSwitch.On = true;
						cell.AccessoryView = refillSwitch;
						cell.Accessory = UITableViewCellAccessory.None;
					} else if (row == 1) {
						cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
						cell.TextLabel.Text = "Remind " + dayPickerDay + " " + dayPickerUnit + " before refills";
						cell.TextLabel.Hidden = false;
						cell.AccessoryView = null;
					} else if (row == 2) { // 2 = DAY_PICKER_ROW
						if (dayPickerIsShowing) {
							cell.Accessory = UITableViewCellAccessory.None;
							cell.AccessoryView = null;
							cell.TextLabel.Text = "";
							cell.TextLabel.Hidden = true;
							cell.Add (this.dayPicker);
						} else {
							cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
							NSDate date = this.timePicker.Date;
							cell.TextLabel.Text = "At " + this.dateFormatter.StringFor (date);
							cell.TextLabel.Hidden = false;
							cell.AccessoryView = null;
						}
					} else if (row == 3) { // 3 = TIME_PICKER_ROW
						if (dayPickerIsShowing) {
							cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
							NSDate date = this.timePicker.Date;
							cell.TextLabel.Text = "At " + this.dateFormatter.StringFor (date);
							cell.TextLabel.Hidden = false;
							cell.AccessoryView = null;
						} else {
							cell.Accessory = UITableViewCellAccessory.None;
							cell.AccessoryView = null;
							cell.TextLabel.Text = "";
							cell.TextLabel.Hidden = true;
							if (this.timePickerIsShowing)
								cell.Add (this.timePicker);
						}
					}
					break;
				case 2:
					if (indexPath.Row == 0) {
						cell.TextLabel.Text = "Metric Measurements";
						UISwitch metricSwitch = new UISwitch ();
						metricSwitch.On = false;
						cell.AccessoryView = metricSwitch;
						cell.Accessory = UITableViewCellAccessory.None;
					}
					break;
				default:
					break;
				}//switch
				return cell;
			}// GetCell

			public class DayPickerSource : UIPickerViewDataSource {
				public DayPickerSource () {
					
				}// DayPickerSource constructor

				public override nint GetComponentCount (UIPickerView pickerView) {
					return 2;
				} // GetComponentCount

				public override nint GetRowsInComponent (UIPickerView pickerView, nint component) {
					switch (component) {
					case 0:
						return 7;
					case 1:
						return 3;
					default:
						return 0;
					}	
				} // GetRowsInComponent
			}// DayPickerSource class

			public class DayPickerDelegate : UIPickerViewDelegate {
				private SettingsTableSource controller;

				public DayPickerDelegate (SettingsTableSource controller) {
					this.controller = controller;
				}// DayPickerDelegate constructor

//				public override nfloat GetComponentWidth (UIPickerView pickerView, nint component) {
//					throw new System.NotImplementedException ();
//				}// GetComponentWidth
//
//				public override nfloat GetRowHeight (UIPickerView pickerView, nint component) {
//					throw new System.NotImplementedException ();
//				}// GetRowHeight

				public override string GetTitle (UIPickerView pickerView, nint row, nint component) {
					if (component == 0) {
						return (row+1).ToString ();
					} else if (component == 1) {
						switch (row) {
						case 0:
							return "Days";
						case 1:
							return "Weeks";
						case 2:
							return "Months";
						default:
							return "";
						}
					} else {
						return "";
					}
				}// GetTitle

//				public override UIView GetView (UIPickerView pickerView, nint row, nint component, UIView view) {
//					throw new System.NotImplementedException ();
//				}// GetView
//
				public override void Selected (UIPickerView pickerView, nint row, nint component) {
					if (component == 0) {
						controller.dayPickerDay = int.Parse (this.GetTitle (pickerView, row, component));
					} else if (component == 1) {
						controller.dayPickerUnit = this.GetTitle (pickerView, row, component);
					}
					controller.controller.TableView.ReloadSections (new NSIndexSet(PICKER_SECTION), UITableViewRowAnimation.None);
				}// Selected

			}// DayPickerDelegate class

		}// class SettingsTableSource

	}// class SettingsController
}// namespace AnimalCare
