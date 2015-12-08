using System;
using UIKit;
using System.Collections.Generic;
using System.Collections;

namespace AnimalCare {
	public static class Photo_Database {
		private static List<PetPhoto> allPics = new List<PetPhoto>();

		//public Photo_Database () {
		//	allPics = new List<PetPhoto> ();
		//}// Photo_Database constructor

		public static int numPictures() {
			return allPics.Count;
		}// numPictures

		public static IEnumerator<PetPhoto> GetEnumerator () {
			return allPics.GetEnumerator ();
		}

		public static PetPhoto getPhotoAt (int arg) {
			return allPics.ToArray () [arg];
		}// getPhotoAt

		public static IEnumerator<PetPhoto> getEnumerator () {
			return allPics.GetEnumerator ();
		}// getEnumerator

		public static void addPhoto (PetPhoto newPhoto) {
			allPics.Add (newPhoto);
		}// addPhoto

		public static bool removePhoto (PetPhoto delPhoto) {
			return allPics.Remove (delPhoto);
		}// removePhoto

		public static PetPhoto[] getPictures (string withTag) {
			Predicate<PetPhoto> test = new Predicate<PetPhoto> ((PetPhoto obj) => obj.hasTag (withTag));
			if (allPics.Exists (test)) {
				return allPics.FindAll (test).ToArray ();
			} else {
				return new PetPhoto[0];
			}
		}// getPictures

	}// class Photo_Database

	public class PetPhoto {
		public UIImage picture { get; set; }
		public string title { get; set; }
		public string desc { get; set; }
		public string[] tags { get; set; }
		private int hash;

		public PetPhoto (UIImage picture, string title = "") {
			this.picture = picture;
			this.title = title;
			if (title.Equals ("")) {
				this.title = "Picture" + Photo_Database.numPictures ();
			}
			this.desc = "";
			this.tags = new string[0];
			this.hash = this.picture.GetHashCode ();
		}// PetPhoto constructor

		public override int GetHashCode () {
			int result = this.hash;
			int count = 2;
			result += title.GetHashCode ();
			if (!desc.Equals ("")){
				count++;
				result += desc.GetHashCode ();
			}
			if (tags.Length > 0) {
				count++;
				result += tags.GetHashCode ();
			}
			return (result / count);
		}// GetHashCode

		public override bool Equals (object obj) {
			if (!obj.GetType ().Equals (this))
				return false;
			PetPhoto compPhoto = obj as PetPhoto;
			if (this.GetHashCode () != compPhoto.GetHashCode ())
				return false;
			if (!this.picture.Equals (compPhoto.picture))
				return false;
			if (!this.title.Equals (compPhoto.title))
				return false;
			if (!this.desc.Equals (compPhoto.desc))
				return false;
			if (this.tags.Length == compPhoto.tags.Length) {
				for (int i = 0; i < this.tags.Length; i++) {
					if (!this.tags [i].Equals (compPhoto.tags [i]))
						return false;
				}
				return true;
			} else {
				return false;
			}
		}// Equals

		public void addTag(string newTag) {
			List<string> tagList = new List<string> (tags);
			tagList.Add (newTag);
			this.tags = tagList.ToArray ();
		}// addTag

		public bool hasTag (string tag) {
			return Array.IndexOf (tags, tag) >= 0;
		}// hasTag

		public bool deleteTag (string delTag) {
			List<string> tagList = new List<string> (tags);
			bool result = tagList.Remove (delTag);
			this.tags = tagList.ToArray ();
			return result;
		}// deleteTag

		public void clearTags() {
			this.tags = new string[0];
		}// clearTags

	}// class PetPhoto

}// namespace AnimalCare

