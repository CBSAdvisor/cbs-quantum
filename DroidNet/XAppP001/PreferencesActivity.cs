using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XAppP001
{
    [Activity(Label = "@string/PreferencesActivity")]
    public class PreferencesActivity : Activity
    {
        public const string XAPPP001PREF_NAME = "XAppP001Pref";
        public const string PREFKEY_CALCULATOR_NUM1_VALUE = "calculator_num1_value";
        public const string PREFKEY_CALCULATOR_NUM2_VALUE = "calculator_num2_value";

        private EditText etDefaultNum1;
        private EditText etDefaultNum2;
        private Button btnSave;
        private Button btnCancel;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Preferences);

            etDefaultNum1 = FindViewById<EditText>(Resource.Id.etDefaultNum1);
            etDefaultNum2 = FindViewById<EditText>(Resource.Id.etDefaultNum2);

            btnSave = FindViewById<Button>(Resource.Id.btnSave);
            btnCancel = FindViewById<Button>(Resource.Id.btnCancel);

            btnSave.Click += OnButtonClick;
            btnCancel.Click += OnButtonClick;

            var sPref = GetSharedPreferences(XAPPP001PREF_NAME, FileCreationMode.Private);
            String num1Val = sPref.GetString(PreferencesActivity.PREFKEY_CALCULATOR_NUM1_VALUE, "0");
            String num2Val = sPref.GetString(PreferencesActivity.PREFKEY_CALCULATOR_NUM2_VALUE, "0");

            etDefaultNum1.Text = num1Val;
            etDefaultNum2.Text = num2Val;

        }

        void OnButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            switch (button.Id)
            {
                case Resource.Id.btnSave:
                    SaveText();
                    break;
                case Resource.Id.btnCancel:
                    Finish();
                    break;
                default:
                    break;
            }
        }

        private void SaveText()
        {
            ISharedPreferences sPref = GetSharedPreferences(XAPPP001PREF_NAME, FileCreationMode.Private);
            ISharedPreferencesEditor spe = sPref.Edit();

            spe.PutString(PREFKEY_CALCULATOR_NUM1_VALUE, etDefaultNum1.Text);
            spe.PutString(PREFKEY_CALCULATOR_NUM2_VALUE, etDefaultNum2.Text);
            spe.Commit();
            Toast.MakeText(this, "Text saved", ToastLength.Long).Show();
        }
    }
}