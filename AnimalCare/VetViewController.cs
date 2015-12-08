using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace AnimalCare
{
	partial class VetViewController : UIViewController
	{
		public Vet vet { get; set; }

		public VetViewController (IntPtr handle) : base (handle)
		{
		}// VetViewController constructor

		public override void ViewDidLoad () {
			base.ViewDidLoad ();
			UIBarButtonItem editButton = new UIBarButtonItem (UIBarButtonSystemItem.Add, editVet);
			this.NavigationItem.Title = vet.name;
			this.NavigationItem.SetRightBarButtonItem (editButton, true);
			this.notesTextView.Text = vet.notes;
			this.callOfficeButton.Enabled = vet.hasOfficePhone;
			this.callCellButton.Enabled = vet.hasCellPhone;
			this.directionsButton.Enabled = vet.hasAddress;
		}// ViewDidLoad

		public void editVet (Object sender, EventArgs e) {
			
		}

		partial void CallOfficeButtonPressed (UIButton sender) {
			var url = new NSUrl("tel:" + vet.fullOfficePhone);
			UIApplication.SharedApplication.OpenUrl (url);
		}

		partial void CallCellButtonPressed (UIButton sender) {
			var url = new NSUrl("tel:" + vet.cellPhone);
			UIApplication.SharedApplication.OpenUrl (url);
		}

		partial void DirectionsToOfficeButtonPressed (UIButton sender) {
			var url = new NSUrl("http://maps.apple.com/?daddr=" + vet.getFormatedAddress () + "&dirflg=d");
			UIApplication.SharedApplication.OpenUrl(url);
		}
	}// class VetViewController
}// namespace AnimalCare
