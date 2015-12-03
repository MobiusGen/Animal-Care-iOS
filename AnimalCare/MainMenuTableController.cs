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
		Pets_Database pets;
		Vet_Database vets;

		public MainMenuTableController (IntPtr handle) : base (handle) {
		}// public MainMenuTableController

		public override void ViewDidLoad() {
			base.ViewDidLoad ();
			menuOptions = new string[]{ "Pets", "Vets", "Calendar", "Gallery", "Settings" };
			pets = new Pets_Database ();
			pets.addNewPet (new Pet ("Mr. Cuddles", profilePicture: UIImage.FromBundle ("mrCuddles"), breed: "Bulldog",
				birthday: NSDate.FromTimeIntervalSinceNow (-162795200), weight: 25, bodyColor: "Brown",
				eyeColor: "Black", height: 60, length: 85, idBrand: "Dog Tagz inc", idNumber: "12345",
				notes: "Pretty cool I guess.", allergies: new string[] { "Peanut butter", "Dogs", "Cats", "Humans" },
				medicalConditions: new string[] { "Acute adorableness" }, vetNames: new string[] { "Dr. Strangelove" }));
			pets.addNewPet (new Pet ("Spot"));
			pets.addNewPet (new Pet ("Fluffy"));
			vets = new Vet_Database ();
			vets.addNewVet (new Vet("Dr. Strangelove", "5555555555", "07", "1231234567",
				"1801 Crazy Ln\nArlington, TX, 22202", "A bit odd. Tends to ramble."));
			vets.addNewVet (new Vet("Dr. Bob"));
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
					petMenu.pets = controller.pets;
					petMenu.vets = controller.vets;
					controller.NavigationController.PushViewController (petMenu, true);
				} else if (indexPath.Row == 1) { //"VETS"
					// Launches a new instance of VetMenuController
					VetMenuController vetMenu = controller.Storyboard.InstantiateViewController("VetMenu") as VetMenuController;
					vetMenu.vets = controller.vets;
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
