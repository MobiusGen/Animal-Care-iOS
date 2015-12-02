using System;
using UIKit;
using Foundation;

namespace AnimalCare {
	public class Pet {
		public struct Medication {
			private string medName { get; set; }
			private string medType {get; set; }
			private int freqNum { get; set; }
			private string freqPeriod {get; set; }
		}

		public string name { get; set; }
		public UIImage profilePicture { get; set; }
		public string breed { get; set; }
		public NSDate birthday { get; set; }
		public string bodyColor { get; set; }
		public string eyeColor { get; set; }
		public double height { get; set; } // In cm
		public double length { get; set; } // In cm
		public string[] identifyingMarks { get; set; }
		public string idBrand { get; set; }
		public string idNumber { get; set; }
		public string notes { get; set; }
		public string[] allergies { get; set; }
		public string[] medicalConditions { get; set; }
		public string medicalOtherInfo { get; set; }
		public string[] vetNames { get; set; }
		public Medication[] medications { get; set; }

		public Pet (string name, UIImage profilePicture = null, string breed = "",
			NSDate birthday = null, string bodyColor = "", string eyeColor = "",
			double height = -1.0, double length = -1.0, string[] identifyingMarks = null,
			string idBrand = "", string idNumber = "", string notes = "",
			string[] allergies = null, string[] medicalConditions = null, string medicalOtherInfo = null,
			string[] vetNames = null, Medication[] medications = null) {

			this.name = name;
			this.profilePicture = profilePicture;
			this.breed = breed;
			this.birthday = birthday;
			this.bodyColor = bodyColor;
			this.eyeColor = eyeColor;
			this.height = height;
			this.length = length;
			this.identifyingMarks = identifyingMarks;
			this.idBrand = idBrand;
			this.idNumber = idNumber;
			this.notes = notes;
			this.allergies = allergies;
			this.medicalConditions = medicalConditions;
			this.medicalOtherInfo = medicalOtherInfo;
			this.vetNames = vetNames;
			this.medications = medications;
		}// Pet constructor
	}// Pet class

	public class Medication {

		public Medication () {
			
		}// Medication constructor
		
	}// Medication class
}// AnimalCare namespace

