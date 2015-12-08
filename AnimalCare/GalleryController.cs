using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace AnimalCare
{
	partial class GalleryController : UIViewController
	{
		public Pets_Database pets { get; set; }
		private const string cellIdentifier = "photoCell";
		private const double CELL_WIDTH = 100;
		private const double CELL_HEIGHT = 100;

		public GalleryController (IntPtr handle) : base (handle)
		{
		}// GalleryController constuctor

		public override void ViewDidLoad () {
			base.ViewDidLoad ();
			NavigationItem.Title = "Gallery";
			UIBarButtonItem editButton = new UIBarButtonItem (UIBarButtonSystemItem.Edit, editButtonClicked);
			NavigationItem.SetRightBarButtonItem (editButton, true);
			UIBarButtonItem tagsButton = new UIBarButtonItem ("Tags", UIBarButtonItemStyle.Bordered, tagsButtonClicked);
			UIBarButtonItem cameraButton = new UIBarButtonItem (UIBarButtonSystemItem.Camera, cameraButtonClicked);
			toolbar.SetItems (new UIBarButtonItem[]{tagsButton,
				new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace), cameraButton}, true);
			collectionView.RegisterClassForCell (typeof(UICollectionViewCell), cellIdentifier);
			collectionView.Source = new GallerySource (this, cellIdentifier, CELL_WIDTH, CELL_HEIGHT);
		}// ViewDidLoad


		private void editButtonClicked(Object obj, EventArgs e) {
			Console.WriteLine ("Edit button clicked");
		}// editButtonClicked
			
		private void tagsButtonClicked(Object obj, EventArgs e) {
			Console.WriteLine ("Tags button clicked");
		}// tagsButtonClicked

		private void cameraButtonClicked (Object obj, EventArgs e) {
			Console.WriteLine ("Camera button clicked");
			UIImagePickerController imagePicker = new UIImagePickerController ();
			imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
			imagePicker.FinishedPickingMedia += handle_FinishedPickingMedia;
			imagePicker.Canceled += handle_Cancled;
			NavigationController.PresentModalViewController (imagePicker, true);
		}// cameraButtonClicked

		protected void handle_FinishedPickingMedia (Object sender, UIImagePickerMediaPickedEventArgs args) {
			UIImage image = args.OriginalImage;
			if (image != null) {
				Photo_Database.addPhoto (new PetPhoto(image));
			}
			UIImagePickerController picker = sender as UIImagePickerController;
			picker.DismissModalViewController (true);
			collectionView.ReloadData ();
		}// handle_FinishedPickingMedia

		protected void handle_Cancled (Object sender, EventArgs e) {
			UIImagePickerController picker = sender as UIImagePickerController;
			picker.DismissModalViewController (true);
		}// handle_Cancled

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender) {
			//photoDetailController controller = segue.DestinationViewController as photoDetailController;
			Console.WriteLine ("Preparing for segue: " + segue.ToString ());
			Console.WriteLine ("From object: " + sender.GetType ().ToString ());
		}

		public class GallerySource : UICollectionViewSource {
			private GalleryController controller;
			private string identifier;
			private double cellWidth;
			private double cellHeight;

			public GallerySource (GalleryController controller, string identifier, double cellWidth, double cellHeight) {
				this.controller = controller;
				this.identifier = identifier;
				this.cellWidth = cellWidth;
				this.cellHeight = cellHeight;
			}// GallerySource constructor

			public override UICollectionViewCell GetCell (UICollectionView collectionView, NSIndexPath indexPath) {
				UICollectionViewCell cell = collectionView.DequeueReusableCell (identifier, indexPath) as UICollectionViewCell;
				UIImageView imageView = new UIImageView (Photo_Database.getPhotoAt (indexPath.Row).picture.Scale (new CoreGraphics.CGSize(cellWidth, cellHeight)));
				imageView.Center = cell.ContentView.Center;
				imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
				//var width = cell.ContentView.Frame.Width;
				//var height = cell.ContentView.Frame.Height;
				//cell.ContentView.AddConstraint (NSLayoutConstraint.Create (cell.ContentView, NSLayoutAttribute.Width,
				//	NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1f, width));
				//cell.ContentView.AddConstraint (NSLayoutConstraint.Create (cell.ContentView, NSLayoutAttribute.Height,
				//	NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1f, height));
				cell.ContentView.AddSubview (imageView);
				//imageView.AddConstraint (NSLayoutConstraint.Create (imageView, NSLayoutAttribute.Height,
				//	NSLayoutRelation.Equal, cell.ContentView, NSLayoutAttribute.Height, 1f, 0f));
				//imageView.AddConstraint (NSLayoutConstraint.Create (imageView, NSLayoutAttribute.Width,
				//	NSLayoutRelation.Equal, cell.ContentView, NSLayoutAttribute.Width, 1f, 0f));
				//cell.Frame = rect;
				return cell;
			}// GetCell

			public override nint GetItemsCount (UICollectionView collectionView, nint section) {
				return Photo_Database.numPictures ();
			}// GetItemsCount

//			public override bool CanMoveItem (UICollectionView collectionView, NSIndexPath indexPath) {
//				return base.CanMoveItem (collectionView, indexPath);
//			}// CanMoveItem;
//
//			public override void ItemDeselected (UICollectionView collectionView, NSIndexPath indexPath) {
//				base.ItemDeselected (collectionView, indexPath);
//			}// ItemDeselected
//
//			public override void ItemHighlighted (UICollectionView collectionView, NSIndexPath indexPath) {
//				base.ItemHighlighted (collectionView, indexPath);
//			}// ItemHighlighted
//
			public override void ItemSelected (UICollectionView collectionView, NSIndexPath indexPath) {
				PetPhoto photo = Photo_Database.getPhotoAt (indexPath.Row);
				photoDetailController detailController = controller.Storyboard.InstantiateViewController ("photoDetail") as photoDetailController;
				detailController.photo = photo;
				controller.NavigationController.PushViewController (detailController, true);
			}// ItemSelected
//
//			public override void MoveItem (UICollectionView collectionView, NSIndexPath sourceIndexPath, NSIndexPath destinationIndexPath) {
//				base.MoveItem (collectionView, sourceIndexPath, destinationIndexPath);
//			}// MoveItem
//
//			public override bool ShouldShowMenu (UICollectionView collectionView, NSIndexPath indexPath) {
//				return base.ShouldShowMenu (collectionView, indexPath);
//			}// ShouldShowMenu

		}// class GallerySource

	}// class GalleryController
		
}// namespace AnimalCare
