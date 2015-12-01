// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace AnimalCare
{
	[Register ("PetProfilePage")]
	partial class PetProfilePage
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel ageLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel birthdayLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel breedLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView profilePicImageView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel weightLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ageLabel != null) {
				ageLabel.Dispose ();
				ageLabel = null;
			}
			if (birthdayLabel != null) {
				birthdayLabel.Dispose ();
				birthdayLabel = null;
			}
			if (breedLabel != null) {
				breedLabel.Dispose ();
				breedLabel = null;
			}
			if (profilePicImageView != null) {
				profilePicImageView.Dispose ();
				profilePicImageView = null;
			}
			if (weightLabel != null) {
				weightLabel.Dispose ();
				weightLabel = null;
			}
		}
	}
}
