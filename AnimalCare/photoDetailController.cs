using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace AnimalCare
{
	partial class photoDetailController : UIViewController
	{
		public PetPhoto photo { get; set; }

		public photoDetailController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad () {
			base.ViewDidLoad ();
			image.Image = photo.picture;
			NavigationItem.Title = photo.title;
			UIBarButtonItem shareButton = new UIBarButtonItem (UIBarButtonSystemItem.Action, share);
			UIBarButtonItem editButton = new UIBarButtonItem (UIBarButtonSystemItem.Edit, edit);
			UIBarButtonItem descriptionButton = new UIBarButtonItem ("Description", UIBarButtonItemStyle.Bordered, show_HideDescription);
			UIBarButtonItem tagsButton = new UIBarButtonItem ("Tags", UIBarButtonItemStyle.Bordered, show_HideTags);
			NavigationItem.SetRightBarButtonItems (new UIBarButtonItem[]{shareButton, editButton}, true);
			toolbar.SetItems (new UIBarButtonItem[]{descriptionButton, new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace), tagsButton}, true);
		}// ViewDidLoad

		public void edit(Object obj, EventArgs e) {
			
		}

		// save to phone or share
		public void share(Object obj, EventArgs e) {
			
		}

		public void show_HideTags(Object obj, EventArgs e) {
			
		}

		public void show_HideDescription(Object obj, EventArgs e) {
			
		}


	}// class photoDetailController
}// namespace AnimalCare
