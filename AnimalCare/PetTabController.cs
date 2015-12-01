using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace AnimalCare
{
	partial class PetTabController : UITabBarController
	{
		public string name { get; set; }

		public PetTabController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad () {
			base.ViewDidLoad ();
		}
	}
}
