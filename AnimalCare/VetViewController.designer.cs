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
	[Register ("VetViewController")]
	partial class VetViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton callCellButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton callOfficeButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton directionsButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextView notesTextView { get; set; }

		[Action ("CallCellButtonPressed:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void CallCellButtonPressed (UIButton sender);

		[Action ("CallOfficeButtonPressed:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void CallOfficeButtonPressed (UIButton sender);

		[Action ("DirectionsToOfficeButtonPressed:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void DirectionsToOfficeButtonPressed (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (callCellButton != null) {
				callCellButton.Dispose ();
				callCellButton = null;
			}
			if (callOfficeButton != null) {
				callOfficeButton.Dispose ();
				callOfficeButton = null;
			}
			if (directionsButton != null) {
				directionsButton.Dispose ();
				directionsButton = null;
			}
			if (notesTextView != null) {
				notesTextView.Dispose ();
				notesTextView = null;
			}
		}
	}
}
