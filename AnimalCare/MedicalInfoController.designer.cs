// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace AnimalCare
{
	[Register ("MedicalInfoController")]
	partial class MedicalInfoController
	{
		[Outlet]
		UIKit.UIStackView allergiesStack { get; set; }

		[Outlet]
		UIKit.UIStackView medConditionsStack { get; set; }

		[Outlet]
		UIKit.UIStackView otherInfoStack { get; set; }

		[Outlet]
		UIKit.UIStackView vetStack { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (allergiesStack != null) {
				allergiesStack.Dispose ();
				allergiesStack = null;
			}

			if (medConditionsStack != null) {
				medConditionsStack.Dispose ();
				medConditionsStack = null;
			}

			if (otherInfoStack != null) {
				otherInfoStack.Dispose ();
				otherInfoStack = null;
			}

			if (vetStack != null) {
				vetStack.Dispose ();
				vetStack = null;
			}
		}
	}
}
