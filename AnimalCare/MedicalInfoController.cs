using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace AnimalCare
{
	partial class MedicalInfoController : UIViewController
	{
		private PetTabController parentController;
		public MedicalInfoController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad () {
			base.ViewDidLoad ();
			parentController = this.ParentViewController as PetTabController;
			Pet pet = parentController.pet;

			allergiesStack.Opaque = true;
			for (int i = 0; i < pet.allergies.Length; i++) {
				UILabel allergyLabel = new UILabel ();
				allergyLabel.Text = "\t" + pet.allergies [i];
				allergyLabel.Opaque = true;
				allergyLabel.BackgroundColor = (i % 2 == 0) ? UIColor.GroupTableViewBackgroundColor : UIColor.FromHSB (0f, 0f, 0.85f);
				allergiesStack.AddArrangedSubview (allergyLabel);
			}

			medConditionsStack.Opaque = true;
			for (int i = 0; i < pet.medicalConditions.Length; i++) {
				UILabel medConditionLabel = new UILabel ();
				medConditionLabel.Text = "\t" + pet.medicalConditions [i];
				medConditionLabel.Opaque = true;
				medConditionLabel.BackgroundColor = (i % 2 == 0) ? UIColor.GroupTableViewBackgroundColor : UIColor.FromHSB (0f, 0f, 0.85f);
				medConditionsStack.AddArrangedSubview (medConditionLabel);
			}

			vetStack.Opaque = true;
			for (int i = 0; i < pet.vetNames.Length; i++) {
				UILabel vetLabel = new UILabel ();
				vetLabel.Text = "\t" + pet.vetNames [i];
				vetLabel.Opaque = true;
				vetLabel.BackgroundColor = (i % 2 == 0) ? UIColor.GroupTableViewBackgroundColor : UIColor.FromHSB (0f, 0f, 0.85f);
				vetStack.AddArrangedSubview (vetLabel);
			}

			otherInfoStack.Opaque = true;
			UILabel otherInfoLabel = new UILabel ();
			otherInfoLabel.Text = "\t" + pet.medicalOtherInfo;
			otherInfoLabel.Opaque = true;
			otherInfoLabel.BackgroundColor = UIColor.GroupTableViewBackgroundColor;
			otherInfoStack.AddArrangedSubview (otherInfoLabel);
		}// ViewDidLoad
			
		public override void ViewWillDisappear (bool animated) {
			int i = 5;
			i++;
			Console.WriteLine (i.ToString ());
			base.ViewWillDisappear (animated);
		}
	}// class MedicalInfoController
}// MedicalInfoController
