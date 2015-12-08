using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Collections.Generic;

namespace AnimalCare
{
	partial class NewMedicineController : UIViewController
	{
		private const int SECTION_SPACER = 25;
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
		private UIStackView timeStack;
		private UIButton[] timeButtons;
		private NSDate[] timeDates;
		private NSDateFormatter dateFormat;
		private UIDatePicker timePicker;
		private UILabel dayLabel;
		private UIButton dayButton;
		private UIDatePicker dayPicker;
		private NSCalendar gregorian;
		private UILabel prescriptionLengthLabel;
		private UITextField prescriptionLengthTextField;
		private UIStepper prescriptionLengthStepper;
		private UILabel prescriptionDayLabel;
		private UILabel refillsLabel;
		private UITextField refillsTextField;
		private UIStepper refillsStepper;
		private UITextView pharmacyTextView;
		private UITextField pharmacyTextField;

		public NewMedicineController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLayoutSubviews () {
			base.ViewDidLayoutSubviews ();
			ScrollView.ContentSize = new CoreGraphics.CGSize (width: stackView.Frame.Width, height: stackView.Frame.Height);
		}

		// Called when the UIKeyboardDidShowNotification is sent
		void keyboardDidAppear (NSNotification notification) {
			NSDictionary info = notification.UserInfo;
			CoreGraphics.CGSize kbSize = NSValue.ValueFromNonretainedObject (info.ObjectForKey (UIKeyboard.FrameBeginUserInfoKey)).CGRectValue.Size;
			UIEdgeInsets contentInsets = new UIEdgeInsets (new System.nfloat(0.0), new System.nfloat(0.0), kbSize.Height, new System.nfloat(0.0));
			ScrollView.ContentInset = contentInsets;

			// If active text field is hidden by keyboard, scroll it so it's visible
			CoreGraphics.CGRect aRect = this.View.Frame;
			var height = aRect.Size.Height;
			height -= kbSize.Height;
			var size = aRect.Size;
			size.Height = height;
			aRect.Size = size;
			UIView activeField = (UIView)notification.Object;
			if (!aRect.Contains (activeField.Frame.Location)) {
				CoreGraphics.CGPoint scrollPoint = new CoreGraphics.CGPoint (0.0, activeField.Frame.Location.Y - kbSize.Height);
				ScrollView.SetContentOffset (scrollPoint, true);
			}
		}// keyboardDidAppear

		// Called when the UIKeyboardWillHideNotification is sent
		void keyboardWillDissapear (NSNotification notification) {
			UIEdgeInsets contentInsets = UIEdgeInsets.Zero;
			ScrollView.ContentInset = contentInsets;
			ScrollView.ScrollIndicatorInsets = contentInsets;
		}// keyboardWillDissaper

		public override void ViewDidLoad () {
			base.ViewDidLoad ();
			//scrollView = stackView.Superview as UIScrollView;
			//scrollView.KeyboardDismissMode = UIScrollViewKeyboardDismissMode.OnDrag;
			//scrollView.ScrollEnabled = true;
			// Set up Navigation Bar
			var saveButton = new UIBarButtonItem (UIBarButtonSystemItem.Save, save);
			var cancelButton = new UIBarButtonItem (UIBarButtonSystemItem.Cancel, cancel);
			NavigationItem.Title = "New Medication:";
			NavigationItem.RightBarButtonItem = saveButton;
			NavigationItem.HidesBackButton = true;
			NavigationItem.LeftBarButtonItem = cancelButton;


			//NSNotificationCenter.DefaultCenter.AddObserver (this, new ObjCRuntime.Selector("keyboardDidAppear:"), UIKeyboard.DidShowNotification, null);
			//NSNotificationCenter.DefaultCenter.AddObserver (this, new ObjCRuntime.Selector ("keyboardWillDissapear:"), UIKeyboard.WillHideNotification, null);

			// Set up the date formatter
			dateFormat = new NSDateFormatter();
			dateFormat.DateStyle = NSDateFormatterStyle.None;
			dateFormat.TimeStyle = NSDateFormatterStyle.Short;

			// Set up new pet form
			nameField = new UITextField();
			nameField.Text = "Medication Name";
			nameField.BorderStyle = UITextBorderStyle.RoundedRect;
			nameField.ReturnKeyType = UIReturnKeyType.Done;

			medTypeLabel = new UILabel ();
			medTypeLabel.Text = "Type of Medication";
			medTypeButton = new UIButton (UIButtonType.RoundedRect);
			medTypeButton.AddTarget (editMedType, UIControlEvent.TouchUpInside);
			medTypeButton.SetTitle ("Pill", UIControlState.Normal);
			medTypeButton.TitleLabel.Font = medTypeButton.TitleLabel.Font.WithSize (medTypeLabel.Font.PointSize);
			medTypePicker = new UIPickerView ();
			medTypePicker.Delegate = new MedTypePickerDelegate (this);
			medTypePicker.DataSource = new MedTypePickerDataSource ();

			freqLabel = new UILabel ();
			freqLabel.Text = "Frequency";
			freqTextField = new UITextField ();
			freqTextField.Text = "1";
			freqTextField.KeyboardType = UIKeyboardType.NumberPad;
			freqTextField.BorderStyle = UITextBorderStyle.RoundedRect;
			freqTextField.Enabled = false;
			freqTextField.AddTarget (freqTextFieldChanged, UIControlEvent.EditingDidEnd | UIControlEvent.EditingDidEndOnExit);
			freqStepper = new UIStepper ();
			freqStepper.Value = 1;
			freqStepper.MinimumValue = 1;
			freqStepper.Enabled = false;
			freqStepper.AddTarget (freqStepperIncremented, UIControlEvent.ValueChanged);
			UIStackView freqStackView = new UIStackView (new UIView[] { freqTextField, freqStepper });
			freqStackView.Spacing = 8;
			freqStackView.Axis = UILayoutConstraintAxis.Horizontal;
			freqButton = new UIButton (UIButtonType.RoundedRect);
			freqButton.SetTitle ("Daily", UIControlState.Normal);
			freqButton.TitleLabel.Font = freqButton.TitleLabel.Font.WithSize (medTypeLabel.Font.PointSize);
			freqButton.AddTarget (editFrequency, UIControlEvent.TouchUpInside);
			freqPicker = new UIPickerView ();
			freqPicker.Delegate = new MedFreqPickerDelegate (this);
			freqPicker.DataSource = new MedFreqPickerDataSource ();

			timePicker = new UIDatePicker ();
			gregorian = new NSCalendar (NSCalendarType.Gregorian);
			timePicker.Date = gregorian.DateBySettingsHour (9, 0, 0, NSDate.Now, NSCalendarOptions.MatchNextTime);
			timePicker.Mode = UIDatePickerMode.Time;
			timePicker.AddTarget (timePickerChanged, UIControlEvent.AllEvents);

			UILabel timeLabel = new UILabel ();
			timeLabel.Text = "Time";
			timeButtons = new UIButton[1];
			timeDates = new NSDate[1];
			timeDates [0] = gregorian.DateBySettingsHour (9, 0, 0, NSDate.Now, NSCalendarOptions.MatchNextTime);
			timeButtons [0] = new UIButton (UIButtonType.RoundedRect);
			timeButtons [0].SetTitle ("9:00 AM", UIControlState.Normal);
			timeButtons [0].AddTarget (openTimePicker, UIControlEvent.TouchUpInside);
			timeStack = new UIStackView ();
			timeStack.Alignment = UIStackViewAlignment.Leading;
			timeStack.Distribution = UIStackViewDistribution.FillProportionally;
			timeStack.Spacing = 5;
			timeStack.Axis = UILayoutConstraintAxis.Vertical;
			timeStack.AddArrangedSubview (timeLabel);
			foreach (var button in timeButtons) {
				timeStack.AddArrangedSubview (button);
			}
			timeStack.AddArrangedSubview (timePicker);
			timePicker.Hidden = true;

			dayLabel = new UILabel ();
			dayLabel.Text = "Day";
			dayLabel.Hidden = true;
			dayButton = new UIButton (UIButtonType.RoundedRect);
			var day = gregorian.GetComponentFromDate (NSCalendarUnit.Weekday, NSDate.Now);
			dayButton.SetTitle (gregorian.WeekdaySymbols[day], UIControlState.Normal);
			dayButton.AddTarget (openDayPicker, UIControlEvent.TouchUpInside);
			dayButton.Hidden = true;
			dayPicker = new UIDatePicker ();
			dayPicker.Mode = UIDatePickerMode.Date;
			dayPicker.MinimumDate = NSDate.Now;
			dayPicker.MaximumDate = NSDate.Now.AddSeconds (604800);
			dayPicker.AddTarget (dayPickerChanged, UIControlEvent.AllEvents);
			dayPicker.Hidden = true;

			prescriptionLengthLabel = new UILabel ();
			prescriptionLengthLabel.Text = "Prescription Length";
			prescriptionLengthTextField = new UITextField ();
			prescriptionLengthTextField.Text = "20";
			prescriptionLengthTextField.AddTarget (prescriptionTextFieldChanged, UIControlEvent.EditingDidEnd | UIControlEvent.EditingDidEndOnExit);
			prescriptionLengthTextField.KeyboardType = UIKeyboardType.NumberPad;
			prescriptionLengthTextField.BorderStyle = UITextBorderStyle.RoundedRect;
			prescriptionLengthStepper = new UIStepper ();
			prescriptionLengthStepper.Value = 20;
			prescriptionLengthStepper.MinimumValue = 1;
			prescriptionLengthStepper.AddTarget (prescriptionStepperIncremented, UIControlEvent.ValueChanged);
			prescriptionDayLabel = new UILabel ();
			prescriptionDayLabel.Text = "Days";
			UIStackView prescriptionStackView = new UIStackView (new UIView[] {
				prescriptionLengthTextField,
				prescriptionLengthStepper,
				prescriptionDayLabel
			});
			prescriptionStackView.Axis = UILayoutConstraintAxis.Horizontal;
			prescriptionStackView.Alignment = UIStackViewAlignment.Leading;
			prescriptionStackView.Distribution = UIStackViewDistribution.FillProportionally;
			prescriptionStackView.Spacing = 5;

			refillsLabel = new UILabel ();
			refillsLabel.Text = "Refills Remaining";
			refillsTextField = new UITextField ();
			refillsTextField.Text = "0";
			refillsTextField.KeyboardType = UIKeyboardType.NumberPad;
			refillsTextField.BorderStyle = UITextBorderStyle.RoundedRect;
			refillsTextField.AddTarget (refillsTextFieldChanged, UIControlEvent.EditingDidEnd | UIControlEvent.EditingDidEndOnExit);
			refillsStepper = new UIStepper ();
			refillsStepper.Value = 0;
			refillsStepper.MinimumValue = 0;
			refillsStepper.AddTarget (refillsStepperIncremented, UIControlEvent.ValueChanged);
			UIStackView refillsStackView = new UIStackView (new UIView[] { refillsTextField, refillsStepper });
			refillsStackView.Spacing = 5;
			refillsStackView.Alignment = UIStackViewAlignment.Leading;
			refillsStackView.Distribution = UIStackViewDistribution.FillProportionally;
			refillsStackView.Axis = UILayoutConstraintAxis.Horizontal;

			pharmacyTextView = new UITextView ();
			pharmacyTextView.Text = "Pharmacy Address";
			pharmacyTextView.ScrollEnabled = false;
			pharmacyTextView.BackgroundColor = UIColor.LightGray;
			pharmacyTextField = new UITextField ();
			pharmacyTextField.Text = "Pharmacy Address";
			pharmacyTextField.BorderStyle = UITextBorderStyle.RoundedRect;
			



			// Sets up the stackview
			stackView.Spacing = 5;
			stackView.Alignment = UIStackViewAlignment.Leading;
			stackView.Distribution = UIStackViewDistribution.EqualSpacing;
			stackView.AddArrangedSubview (nameField);
			UIView spaceView = new UIView (CoreGraphics.CGRect.FromLTRB (0, 0, 50, SECTION_SPACER));
			spaceView.AddConstraint (NSLayoutConstraint.Create (spaceView, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, SECTION_SPACER));
			stackView.AddArrangedSubview (spaceView);
			stackView.AddArrangedSubview (medTypeLabel);
			stackView.AddArrangedSubview (medTypeButton);
			stackView.AddArrangedSubview (medTypePicker);
			medTypePicker.Hidden = true;
			UIView spaceView2 = new UIView (CoreGraphics.CGRect.FromLTRB (0, 0, 50, SECTION_SPACER));
			spaceView2.AddConstraint (NSLayoutConstraint.Create (spaceView2, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, SECTION_SPACER));
			stackView.AddArrangedSubview (spaceView2);
			stackView.AddArrangedSubview (freqLabel);
			stackView.AddArrangedSubview (freqStackView);
			stackView.AddArrangedSubview (freqButton);
			stackView.AddArrangedSubview (freqPicker);
			freqPicker.Hidden = true;
			UIView spaceView3 = new UIView (CoreGraphics.CGRect.FromLTRB (0, 0, 50, SECTION_SPACER));
			spaceView3.AddConstraint (NSLayoutConstraint.Create (spaceView3, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, SECTION_SPACER));
			stackView.AddArrangedSubview (spaceView3);
			stackView.AddArrangedSubview (timeStack);
			stackView.AddArrangedSubview (dayLabel);
			stackView.AddArrangedSubview (dayButton);
			stackView.AddArrangedSubview (dayPicker);
			UIView spaceView4 = new UIView (CoreGraphics.CGRect.FromLTRB (0, 0, 50, SECTION_SPACER));
			spaceView4.AddConstraint (NSLayoutConstraint.Create (spaceView4, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, SECTION_SPACER));
			stackView.AddArrangedSubview (spaceView4);
			stackView.AddArrangedSubview (prescriptionLengthLabel);
			stackView.AddArrangedSubview (prescriptionStackView);
			UIView spaceView5 = new UIView (CoreGraphics.CGRect.FromLTRB (0, 0, 50, SECTION_SPACER));
			spaceView5.AddConstraint (NSLayoutConstraint.Create (spaceView5, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, SECTION_SPACER));
			stackView.AddArrangedSubview (spaceView5);
			stackView.AddArrangedSubview (refillsLabel);
			stackView.AddArrangedSubview (refillsStackView);
			UIView spaceView6 = new UIView (CoreGraphics.CGRect.FromLTRB (0, 0, 50, SECTION_SPACER));
			spaceView6.AddConstraint (NSLayoutConstraint.Create (spaceView6, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, SECTION_SPACER));
			stackView.AddArrangedSubview (spaceView6);
			stackView.AddArrangedSubview (pharmacyTextField);
			UIView spaceView7 = new UIView (CoreGraphics.CGRect.FromLTRB (0, 0, 50, SECTION_SPACER));
			spaceView7.AddConstraint (NSLayoutConstraint.Create (spaceView7, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, SECTION_SPACER));
			stackView.AddArrangedSubview (spaceView7);


		}// ViewDidLoad

		public void save (Object sender, EventArgs e) {
			Medication newMew = new Medication (nameField.Text,
				medType: (Medication.MedicationTypes)(int)(medTypePicker.SelectedRowInComponent (0)),
				frequency: (int)freqStepper.Value,
				frequencyPeriod: (Medication.MedicationFrequency)(int)(freqPicker.SelectedRowInComponent (0)),
				pharmacyAddress: pharmacyTextField.Text);
			List<Medication> medList = new List<Medication> (petController.pet.medications);
			medList.Add (newMew);
			petController.pet.medications = medList.ToArray ();
			NavigationController.PopViewController (true);
		}// save

		public void cancel (Object sender, EventArgs e) {
			NavigationController.PopViewController (true);
		}// cancel

		public void openTimePicker (Object sender, EventArgs e) {
			if (timePicker.Hidden == false) {
				timePicker.Hidden = true;
			} else {
				timeStack.RemoveArrangedSubview (timePicker);
				int location = Array.IndexOf (timeStack.ArrangedSubviews, sender);
				UIButton button = sender as UIButton;
				int tbIndex = Array.IndexOf (timeButtons, button);
				timePicker.Date = timeDates [tbIndex];
				timeStack.InsertArrangedSubview (timePicker, (nuint)(location + 1));
				timePicker.Hidden = false;
			}
		}// openTimePicker - attached to all UIButtons in timeStack

		public void timePickerChanged (Object sender, EventArgs e) {
			int location = Array.IndexOf (timeStack.Subviews, timePicker);
			UIButton button = timeStack.Subviews [location - 1] as UIButton;
			button.SetTitle (dateFormat.StringFor (timePicker.Date), UIControlState.Normal);
		}// timePickerChanged - attached to timePicker

		public void openDayPicker (Object sender, EventArgs e) {
			dayPicker.Hidden = !dayPicker.Hidden;
		}// openDayPicker - attached to dayButton

		public void dayPickerChanged (Object sender, EventArgs e) {
			NSDate day = dayPicker.Date;
			nint weekDay = gregorian.GetComponentFromDate (NSCalendarUnit.Weekday, day);
			dayButton.SetTitle (gregorian.WeekdaySymbols[weekDay], UIControlState.Normal);
		}// dayPickerChanged - attached to dayPicker

		public void editMedType (Object sender, EventArgs e) {
			medTypePicker.Hidden = !medTypePicker.Hidden;
		}// editMedType - attached to medTypeButton

		public void editFrequency (Object sender, EventArgs e) {
			freqPicker.Hidden = !freqPicker.Hidden;
		}// editFrequency - attached to freqButton

		public void refillsStepperIncremented (Object sender, EventArgs e) {
			refillsTextField.Text = ((int)refillsStepper.Value).ToString ();
		}// refillsStepperIncremented - attached to refillsStepper

		public void refillsTextFieldChanged (Object sender, EventArgs e) {
			string text = refillsTextField.Text;
			int result;
			if (int.TryParse (text, out result)) {
				refillsStepper.Value = result;
			} else {
				refillsTextField.Text = ((int)refillsStepper.Value).ToString ();
			}
		}// refillsTextFieldChanged - attached to refillsTextField

		public void prescriptionStepperIncremented (Object sender, EventArgs e) {
			prescriptionLengthTextField.Text = ((int)(prescriptionLengthStepper.Value)).ToString ();
			if (prescriptionLengthStepper.Value == 1)
				prescriptionDayLabel.Text = "Day";
			else
				prescriptionDayLabel.Text = "Days";
		}// prescriptionStepperIncremented - attached to prescriptionStepper

		public void prescriptionTextFieldChanged (Object sender, EventArgs e) {
			string text = prescriptionLengthTextField.Text;
			int result;
			if (int.TryParse (text, out result)) {
				prescriptionLengthStepper.Value = result;
			} else {
				prescriptionLengthTextField.Text = ((int)(prescriptionLengthStepper.Value)).ToString ();
			}
			if (prescriptionLengthStepper.Value == 1)
				prescriptionDayLabel.Text = "Day";
			else
				prescriptionDayLabel.Text = "Days";
		}// prescriptionTextFieldChanged - attached to prescriptionTextField

		public void freqStepperIncremented (Object sender, EventArgs e) {
			freqTextField.Text = ((int)freqStepper.Value).ToString ();
			//frequencyChanged ();
		}// freqStepperIncremented - attached to freqStepper

		public void freqTextFieldChanged (Object sender, EventArgs e) {
			string text = freqTextField.Text;
			int result;
			if (int.TryParse (text, out result)) {
				freqStepper.Value = result;
				//frequencyChanged ();
			} else {
				freqTextField.Text = ((int)freqStepper.Value).ToString ();
			}
		}// freqTextFieldChanged - attached to freqTextField

		private void frequencyChanged () {
			int frequencyNum = int.Parse (freqTextField.Text);
			Medication.MedicationFrequency frequencyType = (Medication.MedicationFrequency)((int)(freqPicker.SelectedRowInComponent (0)));
			if (frequencyType == Medication.MedicationFrequency.Daily && frequencyNum > 1) {
				if (timeButtons.Length < frequencyNum) {
					UIButton[] newButtons = new UIButton[frequencyNum];
					NSDate[] newDates = new NSDate[frequencyNum];
					timeButtons.CopyTo (newButtons, 0);
					timeDates.CopyTo (newDates, 0);
					for (int i = timeButtons.Length; i < frequencyNum; i++) {
						newDates [i] = newDates [i - 1].AddSeconds (3600);
						newButtons [i] = new UIButton (UIButtonType.RoundedRect);
						newButtons [i].SetTitle (dateFormat.StringFor (newDates [i]), UIControlState.Normal);
						newButtons[i].AddTarget (openTimePicker, UIControlEvent.TouchUpInside);
						timeStack.AddArrangedSubview (newButtons[i]);
					}
					timeButtons = newButtons;
					timeDates = newDates;
				} else if (timeButtons.Length > frequencyNum) {
					for (int i = frequencyNum-1; i < timeButtons.Length; i++) {
						timeButtons [i].Hidden = true;
					}
				}
			} else {
				for (int i = 1; i < timeButtons.Length; i++) {
					timeButtons [i].Hidden = true;
				}
			}
		}// frequencyChanged

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
				controller.dayLabel.Hidden = true;
				controller.dayButton.Hidden = true;
				controller.dayPicker.Hidden = true;
				if (frequency.Equals ("Daily")) {
					controller.freqTextField.Enabled = false;
					controller.freqStepper.Enabled = false;
				} else {
					controller.freqTextField.Enabled = true;
					controller.freqStepper.Enabled = true;
					if (frequency.Equals ("Weekly")) {
						controller.dayLabel.Hidden = false;
						controller.dayButton.Hidden = false;
					}
				}
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
