using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace AnimalCare
{
	partial class PetTabController : UITabBarController
	{
		public Pet pet { get; set; }
		public Vet_Database vets { get; set; }

		public PetTabController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad () {
			base.ViewDidLoad ();
			this.NavigationItem.Title = pet.name;
		}
	}
}
