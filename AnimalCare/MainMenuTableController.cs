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
		Pet[] pets;

		public MainMenuTableController (IntPtr handle) : base (handle) {
		}// public MainMenuTableController

		public override void ViewDidLoad() {
			base.ViewDidLoad ();
			menuOptions = new string[]{ "Pets", "Vets", "Calendar", "Gallery", "Settings" };
			pets = new Pet[3];
			pets [0] = new Pet ("Cookie");
			pets [1] = new Pet ("Spot");
			pets [2] = new Pet ("Fluffy");
			CellIdentifier = new NSString ("TableCell");
			TableView.RegisterClassForCellReuse (typeof(UITableViewCell), CellIdentifier);
			TableView.Source = new MainMenuTableSource (this);
		}// ViewDidLoad

		// The table source
		class MainMenuTableSource : UITableViewSource {

			MainMenuTableController controller;

			public MainMenuTableSource (MainMenuTableController controller) {
				this.controller = controller;
			}

			public override nint RowsInSection (UITableView tableView, nint section) {
				return controller.menuOptions.Length;
			}// RowsInSection

			public override nint NumberOfSections(UITableView tableView) {
				return 1;
			}// NumberOfSections

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath) {
				UITableViewCell cell = tableView.DequeueReusableCell(controller.CellIdentifier);
				int row = indexPath.Row;
				if (cell == null) {
					cell = new UITableViewCell (UITableViewCellStyle.Default, controller.CellIdentifier);
				}
				cell.TextLabel.Text = controller.menuOptions [row];
				return cell;
			}// GetCell

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath) {
				controller.TableView.DeselectRow(indexPath, true);
				//UIAlertController okAlerController = UIAlertController.Create ("Row Selected", controller.menuOptions [indexPath.Row], UIAlertControllerStyle.Alert);
				//okAlerController.AddAction (UIAlertAction.Create ("OK", UIAlertActionStyle.Default, null));
				//controller.PresentViewController (okAlerController, true, null);
				if (indexPath.Row == 0) { // "PETS"
					// Launches a new instance of PetMenuController
					PetMenuController petMenu = controller.Storyboard.InstantiateViewController ("PetMenu") as PetMenuController;
					controller.NavigationController.PushViewController (petMenu, true);
				} else if (indexPath.Row == 1) { //"VETS"
					// Launches a new instance of VetMenuController
					VetMenuController vetMenu = controller.Storyboard.InstantiateViewController("VetMenu") as VetMenuController;
					controller.NavigationController.PushViewController (vetMenu, true);
				} else if (indexPath.Row == 2) { //"CALENDAR"

				} else if (indexPath.Row == 3) { //"GALLERY"

				} else if (indexPath.Row == 4) { //"SETTINGS"
					SettingsController settingsMenu = controller.Storyboard.InstantiateViewController("SettingsController") as SettingsController;
					controller.NavigationController.PushViewController (settingsMenu, true);
				}
			}

		}// class MainMenuTableSource

	}// class MainMenuTableController
}// namespace AnimalCare
