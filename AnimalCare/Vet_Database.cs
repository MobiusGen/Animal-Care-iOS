using System;
using System.Collections.Generic;

namespace AnimalCare {
	public class Vet_Database {
		private List<Vet> vets;

		public Vet_Database () {
			vets = new List<Vet> ();
		}// Vet_Database constructor

		/// <summary>
		/// Adds the new vet to the database. Will fail if a vet with the same name
		/// already exists.
		/// </summary>
		/// <returns><c>true</c>, if new vet was added, <c>false</c> otherwise.</returns>
		/// <param name="newVet">New vet.</param>
		public bool addNewVet(Vet newVet) {
			if (vets.Contains (newVet))
				return false;
			vets.Add (newVet);
			return true;
		}

		/// <summary>
		/// Removes the vet.
		/// </summary>
		/// <param name="vet">The name of the vet to be removed.</param>
		public void removeVet(string withName) {
			Vet vet = new Vet (withName);
			if (vets.Contains (vet))
				vets.Remove (vet);
		}

		/// <summary>
		/// Returns true if the specified vet exists.
		/// </summary>
		/// <param name="withName">The name of the vet to search for.</param>
		public bool ContainsVet(string withName) {
			Vet vet = new Vet (withName);
			return vets.Contains (vet);
		}

		/// <summary>
		/// Gets the specified vet.
		/// </summary>
		/// <returns>The vet the with specified name, or a blank Vet if there is none.</returns>
		/// <param name="byName">The name of the desired vet.</param>
		public Vet GetVet(string byName) {
			foreach (var vet in vets) {
				if (vet.name.Equals (byName))
					return vet;
			}// foreach
			return new Vet();
		}

		/// <summary>
		/// Returns the current number of vets.
		/// </summary>
		/// <returns>The vets.</returns>
		public int numVets() {
			return vets.Count;
		}

		/// <summary>
		/// Returns an array of the names of all the vets.
		/// </summary>
		/// <returns>The names.</returns>
		public string[] getNames() {
			string[] names = new string[vets.Count];
			Vet[] vetArray = vets.ToArray ();
			for (int i = 0; i < vets.Count; i++) {
				names [i] = vetArray [i].name;
			}
			return names;
		}// getNames

	}// class Vet_Database

	public struct Vet {
		public string name { get; set; }
		public string officePhone { get; set; }
		public string officeExtension { get; set; }
		public string cellPhone { get; set; }
		public string address { get; set; }
		public string notes { get; set; }
		public bool hasOfficePhone { get { return !officePhone.Equals (""); } }
		public bool hasCellPhone { get { return !cellPhone.Equals (""); } }
		public bool hasAddress { get { return !address.Equals (""); } }
		public string fullOfficePhone { get {
				if (officeExtension.Equals (""))
					return officePhone;
				else
					return officePhone + "," + officeExtension;
			}}

		public Vet (string name, string officePhone = "", string officeExtension = "",
			string cellPhone = "", string address = "", string notes = "") {
			this.name = name;
			this.officePhone = officePhone;
			this.officeExtension = officeExtension;
			this.cellPhone = cellPhone;
			this.address = address;
			this.notes = notes;
		}// Vet constructor

		/// <summary>
		/// Gets the address in a format that can be used by a URL
		/// </summary>
		/// <returns>The formated address.</returns>
		public string getFormatedAddress() {
			string result = address.Replace (' ', '+');
			result = result.Replace ('\n', '+');
			return result;
		}

		public override bool Equals (object obj) {
			Vet compareVet = (Vet)obj;
			return this.name.Equals (compareVet.name);
		}// Equals

		public override int GetHashCode () {
			return this.name.GetHashCode ();
		}// GetHashCode

	}// struct Vet
}// namespace AnimalCare

