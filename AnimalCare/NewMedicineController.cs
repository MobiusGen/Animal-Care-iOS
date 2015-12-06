using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace AnimalCare
{
	partial class NewMedicineController : UIViewController
	{
		public PetTabController petController { get; set; }
		private UITextField nameField;
		private UILabel medTypeLabel;
		private UIButton medTypeButton;
		private UIPickerView medTypePicker;
		private UILabel freqLabel;
		private UITextField freqTextField;
		private UIStepper freqStepper;
		private UIPickerView freqPicker;
		private UIButton freqButton;

		public NewMedicineController (IntPtr handle) : base (handle)
		{
		}

//		public override void ViewDidLayoutSubviews () {
//			base.ViewDidLayoutSubviews ();
//			scrollView.ContentSize = new CoreGraphics.CGSize (width: stackView.Frame.Width, height: stackView.Frame.Height);
//		}

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
			nameField = new UITextField();
			nameField.Text = "Medication Name";
			nameField.ReturnKeyType = UIReturnKeyType.Done;

			medTypeLabel = new UILabel ();
			medTypeLabel.Text = "Type of Medication";
			medTypeButton = new UIButton (UIButtonType.RoundedRect);
			medTypeButton.AddTarget (editMedType, UIControlEvent.TouchUpInside);
			medTypeButton.SetTitle ("Pill", UIControlState.Normal);
			medTypeButton.SetTitle ("", UIControlState.Selected);
			medTypePicker = new UIPickerView ();
			medTypePicker.Delegate = new MedTypePickerDelegate (this);
			medTypePicker.DataSource = new MedTypePickerDataSource ();

			freqLabel = new UILabel ();
			freqLabel.Text = "Frequency";
			freqTextField = new UITextField ();
			freqTextField.Text = "1";
			freqTextField.KeyboardType = UIKeyboardType.NumberPad;
			freqTextField.AddTarget (freqTextFieldChanged, UIControlEvent.EditingDidEnd | UIControlEvent.EditingDidEndOnExit);
			freqStepper = new UIStepper ();
			freqStepper.Value = 1;
			freqStepper.MinimumValue = 1;
			freqStepper.AddTarget (freqStepperIncremented, UIControlEvent.ValueChanged);
			UIStackView freqStackView = new UIStackView (new UIView[] { freqTextField, freqStepper });
			freqStackView.Spacing = 8;
			freqStackView.Axis = UILayoutConstraintAxis.Horizontal;
			freqButton = new UIButton (UIButtonType.RoundedRect);
			freqButton.SetTitle ("Daily", UIControlState.Normal);
			freqPicker = new UIPickerView ();
			freqPicker.Delegate = new MedFreqPickerDelegate (this);
			freqPicker.DataSource = new MedFreqPickerDataSource ();

			stackView.Alignment = UIStackViewAlignment.Leading;
			stackView.Distribution = UIStackViewDistribution.EqualCentering;
			stackView.AddArrangedSubview (nameField);
			stackView.AddArrangedSubview (medTypeLabel);
			stackView.AddArrangedSubview (medTypeButton);
			stackView.AddArrangedSubview (medTypePicker);
			medTypePicker.Hidden = true;
			stackView.AddArrangedSubview (freqLabel);
			stackView.AddArrangedSubview (freqStackView);
			stackView.AddArrangedSubview (freqButton);
			stackView.AddArrangedSubview (freqPicker);
			freqPicker.Hidden = true;

		}// ViewDidLoad

		public void save (Object sender, EventArgs e) {
			
		}// save

		public void cancel (Object sender, EventArgs e) {
			NavigationController.PopViewController (true);
		}// cancel

		public void editMedType (Object sender, EventArgs e) {
			medTypePicker.Hidden = !medTypePicker.Hidden;
		}// editMedType - attached to medTypeButton

		public void editFrequency (Object sender, EventArgs e) {
			freqPicker.Hidden = !freqPicker.Hidden;
		}// editFrequency - attached to freqButton

		public void freqStepperIncremented (Object sender, EventArgs e) {
			freqTextField.Text = ((int)freqStepper.Value).ToString ();
		}// freqStepperIncremented - attached to freqStepper

		public void freqTextFieldChanged (Object sender, EventArgs e) {
			string text = freqTextField.Text;
			int result;
			if (int.TryParse (text, out result)) {
				freqStepper.Value = result;
			}
		}// freqTextFieldChanged - attached to freqTextField

		public class MedFreqPickerDelegate : UIPickerViewDelegate {
			private Medication.MedicationFrequency[] medFrequencies;
			private NewMedicineController controller;

			public MedFreqPickerDelegate (NewMedicineController controller) {
				medFrequencies = Medication.ALL_MED_FREQUENCIES;
				this.controller = controller;
			}// MedFreqPickerDelegate constructor

			public override string GetTitle (UIPickerView pickerView, nint row, nint component) {
				return medFrequencies[row].ToString ();
			}// GetTitle

			public override void Selected (UIPickerView pickerView, nint row, nint component) {
				string frequency = GetTitle (pickerView, row, component);
				controller.freqButton.SetTitle (frequency, UIControlState.Normal);
			}// Selected

		}// class MedFreqPickerDelegate

		public class MedFreqPickerDataSource : UIPickerViewDataSource {
			private Medication.MedicationFrequency[] medFrequencies;

			public MedFreqPickerDataSource() {
				medFrequencies = Medication.ALL_MED_FREQUENCIES;
			}// MedFreqPickerDataSource constructor

			public override nint GetComponentCount (UIPickerView pickerView) {
				return 1;
			}// GetComponentCount

			public override nint GetRowsInComponent (UIPickerView pickerView, nint component) {
				return medFrequencies.Length;
			}// GetRowsInComponent

		}// class MedFreqPickerDataSource

		public class MedTypePickerDelegate : UIPickerViewDelegate {
			private Medication.MedicationTypes[] medTypes;
			private NewMedicineController controller;

			public MedTypePickerDelegate (NewMedicineController controller) {
				medTypes = Medication.ALL_MED_TYPES;
				this.controller = controller;
			}// MedTypePickerDelegate constructor

			public override string GetTitle (UIPickerView pickerView, nint row, nint component) {
				return medTypes[row].ToString ();
			}// GetTitle

			public override void Selected (UIPickerView pickerView, nint row, nint component) {
				string type = GetTitle (pickerView, row, component);
				controller.medTypeButton.SetTitle (type, UIControlState.Normal);
			}// Selected

		}// class MedTypePickerDelegate

		public class MedTypePickerDataSource : UIPickerViewDataSource {
			private Medication.MedicationTypes[] medTypes;

			public MedTypePickerDataSource() {
				medTypes = Medication.ALL_MED_TYPES;
			}// MedTypePickerDataSource constructor

			public override nint GetComponentCount (UIPickerView pickerView) {
				return 1;
			}// GetComponentCount

			public override nint GetRowsInComponent (UIPickerView pickerView, nint component) {
				return medTypes.Length;
			}// GetRowsInComponent

		}// class MedTypePickerDataSource

	}// class NewMedicineController
}// namespace AnimalCare
