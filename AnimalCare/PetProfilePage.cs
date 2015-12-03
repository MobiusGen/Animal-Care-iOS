using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace AnimalCare
{
	partial class PetProfilePage : UIViewController
	{
		PetTabController controller;

		public PetProfilePage (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad () {
			base.ViewDidLoad ();
			controller = this.ParentViewController as PetTabController;
			// Set all the labels with the pet's info
			this.BreedLabel.Text = "Breed: " + controller.pet.breed;
			this.ageLabel.Text = "Age: " + (controller.pet.age < 0 ? "" : controller.pet.age.ToString ());
			this.weightLabel.Text = "Weight: " + ( controller.pet.weight < 0 ? "" : controller.pet.weight + " kg");
			this.weightLabel.Text = "Weight: " + controller.pet.weight + " kg";
			NSDateFormatter format = new NSDateFormatter ();
			format.TimeStyle = NSDateFormatterStyle.None;
			format.DateStyle = NSDateFormatterStyle.Medium;
			this.birthdayLabel.Text = "Birthday: " + (controller.pet.birthday == null ? "" : format.StringFor (controller.pet.birthday));
			this.colorLabel.Text = "Color: " + controller.pet.bodyColor;
			this.eyeColorLabel.Text = "Eye Color: " + controller.pet.eyeColor;
			this.heightLabel.Text = "Height: " + (controller.pet.height < 0 ? "" : controller.pet.height + " cm");
			this.lengthLabel.Text = "Length: " + (controller.pet.length < 0 ? "" : controller.pet.length + " cm");
			if (controller.pet.identifyingMarks == null) {
				this.identifyingMarksLabel.Text = "Identifying Marks: None";
			} else {
				string marks = "Identifying Marks: ";
				for (int i = 0; i < controller.pet.identifyingMarks.Length; i++) {
					marks += controller.pet.identifyingMarks [i] + (i == controller.pet.identifyingMarks.Length - 1 ? "" : ", ");
				}// for
				this.identifyingMarksLabel.Text = marks;
			}// if-else
			this.idChipBrandLabel.Text = "ID Chip Brand: " + controller.pet.idBrand;
			this.idChipNumberLabel.Text = "ID Chip Number: " + controller.pet.idNumber;
			this.notesTextView.Text = "Notes : " + controller.pet.notes;
			this.profilePicture.Image = controller.pet.profilePicture;
			this.profilePicture.ClipsToBounds = true;
		}// ViewDidLoad
	}// class PetProfilePage
}// namespace AnimalCare
