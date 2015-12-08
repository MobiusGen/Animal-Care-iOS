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
	[Register ("photoDetailController")]
	partial class photoDetailController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView image { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIToolbar toolbar { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (image != null) {
				image.Dispose ();
				image = null;
			}
			if (toolbar != null) {
				toolbar.Dispose ();
				toolbar = null;
			}
		}
	}
}
