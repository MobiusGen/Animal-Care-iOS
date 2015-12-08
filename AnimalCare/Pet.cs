using System;
using UIKit;
using Foundation;

namespace AnimalCare {
	public class Pet {

		public string name { get; set; }
		public UIImage profilePicture { get; set; }
		public string breed { get; set; }
		public NSDate birthday { get; set; }
		public double weight { get; set; }
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
		public int age { get {
				if (birthday == null)
					return -1;
				NSCalendar gregorian = NSCalendar.CurrentCalendar;
				var unitFlags = NSCalendarUnit.Year;
				var comparison = gregorian.Components (unitFlags, birthday, NSDate.Now, NSCalendarOptions.None);
				return (int)comparison.Year;
			}}

		public Pet (string name, UIImage profilePicture = null, string breed = "",
			NSDate birthday = null, double weight = -1, string bodyColor = "", string eyeColor = "",
			double height = -1.0, double length = -1.0, string[] identifyingMarks = null,
			string idBrand = "", string idNumber = "", string notes = "",
			string[] allergies = null, string[] medicalConditions = null, string medicalOtherInfo = "",
			string[] vetNames = null, Medication[] medications = null ) {

			this.name = name;
			this.profilePicture = profilePicture;
			this.breed = breed;
			this.birthday = birthday;
			this.weight = weight;
			this.bodyColor = bodyColor;
			this.eyeColor = eyeColor;
			this.height = height;
			this.length = length;
			this.identifyingMarks = identifyingMarks ?? new string[0];
			this.idBrand = idBrand;
			this.idNumber = idNumber;
			this.notes = notes;
			this.allergies = allergies ?? new string[0];
			this.medicalConditions = medicalConditions ?? new string[0];
			this.medicalOtherInfo = medicalOtherInfo;
			this.vetNames = vetNames ?? new string[0];
			this.medications = (medications ?? new Medication[0]);
		}// Pet constructor

		public override bool Equals (object obj) {
			Pet comparePet = obj as Pet;
			return this.name.Equals (comparePet.name);
		}// Equals

		public override int GetHashCode() {
			return this.name.GetHashCode ();
		}// GetHashCode

	}// Pet class

	public struct Medication {
		public enum MedicationTypes { Pill = 0, Liquid, Shot, Drops, Gel, Other };
		public enum MedicationFrequency { Daily = 0, Weekly, Monthly, Yearly };

		public static Medication NULL_MEDICATION = new Medication ("");
		public static MedicationTypes[] ALL_MED_TYPES = new MedicationTypes[] {
			MedicationTypes.Pill, MedicationTypes.Liquid, MedicationTypes.Shot,
			MedicationTypes.Drops, MedicationTypes.Gel, MedicationTypes.Other};
		public static MedicationFrequency[] ALL_MED_FREQUENCIES = new MedicationFrequency[] {
			MedicationFrequency.Daily, MedicationFrequency.Weekly, MedicationFrequency.Monthly, MedicationFrequency.Yearly};

		public string name { get; set; }
		public MedicationTypes medType { get; set; }
		public int frequencyNumber { get; set; }
		public MedicationFrequency frequencyPeriod { get; set; }
		public string pharmacyAddress { get; set; }

		public Medication (string name, MedicationTypes medType = MedicationTypes.Pill, int frequency = 1,
			MedicationFrequency frequencyPeriod = MedicationFrequency.Daily, string pharmacyAddress = "") {
			this.name = name;
			this.medType = medType;
			this.frequencyNumber = frequency;
			this.frequencyPeriod = frequencyPeriod;
			this.pharmacyAddress = pharmacyAddress;
		}// Medication constructor

		public int getPharmacyAddressLines() {
			int index = 0;
			int count = 0;
			while (index != -1) {
				index = pharmacyAddress.IndexOf ('\n');
				count++;
			}
			return count;
		}// getPharmacyAddressLines
		
	}// Medication struct
}// AnimalCare namespace

