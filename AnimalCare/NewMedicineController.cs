using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace AnimalCare
{
	partial class NewMedicineController : UIViewController
	{
		public PetTabController petController { get; set; }

		public NewMedicineController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLayoutSubviews () {
			base.ViewDidLayoutSubviews ();
			scrollView.ContentSize = new CoreGraphics.CGSize (width: stackView.Frame.Width, height: stackView.Frame.Height);
		}

		public override void ViewDidLoad () {
			base.ViewDidLoad ();
			// Set up Navigation Bar
			var saveButton = new UIBarButtonItem (UIBarButtonSystemItem.Save);
			var cancelButton = new UIBarButtonItem (UIBarButtonSystemItem.Cancel, cancel);
			NavigationItem.Title = "New Medication";
			NavigationItem.RightBarButtonItem = saveButton;
			NavigationItem.HidesBackButton = true;
			NavigationItem.LeftBarButtonItem = cancelButton;

			// Set up new pet form
			UITextField nameField = new UITextField();
			nameField.Text = "Medication Name";
			nameField.ReturnKeyType = UIReturnKeyType.Done;

			var medTypeLabel = new UILabel ();
			medTypeLabel.Text = "Type of Medication";
			UIPickerView medTypePicker = new UIPickerView ();
			//var medTypeDelegate = new MedTypePickerDelegate (this);
			medTypePicker.Delegate = new MedTypePickerDelegate (this);
			medTypePicker.DataSource = new MedTypePickerDelegate (this);

			var freqLabel = new UILabel ();
			freqLabel.Text = "Frequency";
			var freqStepper = new UIStepper ();
			freqStepper.Value = 1;
			var freqPicker = new UIPickerView ();
			//var freqPickerDelegate = new MedFreqPickerDelegate (this);
			freqPicker.Delegate = new MedFreqPickerDelegate (this);
			freqPicker.DataSource = new MedFreqPickerDelegate (this);

			stackView.AddArrangedSubview (nameField);
			//stackView.AddArrangedSubview (medTypeLabel);
			//stackView.AddArrangedSubview (medTypePicker);
			stackView.AddArrangedSubview (freqLabel);
			stackView.AddArrangedSubview (freqStepper);
			//stackView.AddArrangedSubview (freqPicker);

		}// ViewDidLoad

		public void save (Object sender, EventArgs e) {
			
		}// save

		public void cancel (Object sender, EventArgs e) {
			NavigationController.PopViewController (true);
		}// cancel

		public class MedFreqPickerDelegate : UIPickerViewDelegate {
			private NewMedicineController controller;
			private Medication.MedicationFrequency[] medFrequencies;

			public MedFreqPickerDelegate (NewMedicineController controller) {
				this.controller = controller;
				medFrequencies = Medication.ALL_MED_FREQUENCIES;
			}// MedFreqPickerDelegate constructor

			public override string GetTitle (UIPickerView pickerView, nint row, nint component) {
				return medFrequencies[row].ToString ();
			}// GetTitle

			public override void Selected (UIPickerView pickerView, nint row, nint component) {
				
			}// Selected

			public nint GetComponentCount (UIPickerView pickerView) {
				return 1;
			}// GetComponentCount

			public nint GetRowsInComponent (UIPickerView pickerView, nint component) {
				return medFrequencies.Length;
			}// GetRowsInComponent

		}// class MedFreqPickerDelegate

		public class MedTypePickerDelegate : UIPickerViewDelegate {
			private NewMedicineController controller;
			private Medication.MedicationTypes[] medTypes;

			public MedTypePickerDelegate (NewMedicineController controller) {
				this.controller = controller;
				medTypes = Medication.ALL_MED_TYPES;
			}// MedTypePickerDelegate constructor

			public override string GetTitle (UIPickerView pickerView, nint row, nint component) {
				return medTypes[row].ToString ();
			}// GetTitle

			public override void Selected (UIPickerView pickerView, nint row, nint component) {
				
			}// Selected

			public nint GetComponentCount (UIPickerView pickerView) {
				return 1;
			}// GetComponentCount

			public nint GetRowsInComponent (UIPickerView pickerView, nint component) {
				return medTypes.Length;
			}// GetRowsInComponent

		}// class MedTypePickerDelegate
	}// class NewMedicineController
}// namespace AnimalCare
