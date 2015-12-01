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

		private string name { get; set; }
		private UIImage profilePicture { get; set; }
		private string breed { get; set; }
		private NSDate birthday { get; set; }
		private string bodyColor { get; set; }
		private string eyeColor { get; set; }
		private double height { get; set; } // In cm
		private double length { get; set; } // In cm
		private string[] identifyingMarks { get; set; }
		private string idBrand { get; set; }
		private string idNumber { get; set; }
		private string notes { get; set; }
		private string[] allergies { get; set; }
		private string[] medicalConditions { get; set; }
		private string medicalOtherInfo { get; set; }
		private string[] vetNames { get; set; }
		private Medication[] medications { get; set; }

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

