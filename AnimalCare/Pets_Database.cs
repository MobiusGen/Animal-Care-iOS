using System;
using System.Collections.Generic;

namespace AnimalCare {
	public class Pets_Database {
		private List<Pet> pets;

		public Pets_Database () {
			pets = new List<Pet> ();
		}// Pets_Database constructo

		/// <summary>
		/// Adds the new pet to the database. Will fail if a pet with the same name
		/// already exists.
		/// </summary>
		/// <returns><c>true</c>, if new pet was added, <c>false</c> otherwise.</returns>
		/// <param name="newPet">New pet.</param>
		public bool addNewPet (Pet newPet) {
			if (pets.Contains (newPet))
				return false;
			pets.Add (newPet);
		}// addNewPet

		/// <summary>
		/// Removes the pet.
		/// </summary>
		/// <param name="withName">The name of the pet to be removed.</param>
		public void removePet (string withName) {
			Pet pet = new Pet (withName);
			if (pets.Contains (pet))
				pets.Remove (pet);
		}// removePet

		/// <summary>
		/// Returns true if the specified pet exists
		/// </summary>
		/// <returns><c>true</c>, if pet exists, <c>false</c> otherwise.</returns>
		/// <param name="withName">The name of the pet to search for.</param>
		public bool ContainsPet (string withName) {
			Pet pet = new Pet (withName);
			return pets.Contains (pet);
		}// ContainsPet

		/// <summary>
		/// Gets the specified pet.
		/// </summary>
		/// <returns>The pet with the specified name, or null.</returns>
		/// <param name="byName">The name of the desired pet.</param>
		public Pet GetPet(string byName) {
			foreach (var pet in pets) {
				if (pet.name.Equals (byName))
					return pet;
			}
			return null;
		}// GetPet

		public int getCount() {
			return pets.Count;
		}

		/// <summary>
		/// Returns an array of the names of all the vets.
		/// </summary>
		/// <returns>The names.</returns>
		public string[] getNames() {
			string[] names = new string[pets.Count];
			Pet[] petArray = pets.ToArray ();
			for (int i = 0; i < pets.Count; i++) {
				names [i] = petArray [i].name;
			}
			return names;
		}// getNames

	}// class Pets_Database
}// namespace AnimalCare

