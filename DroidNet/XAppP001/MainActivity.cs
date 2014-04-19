using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Text;
using System.Globalization;
using Android.Views.Animations;
using Android.Animation;
using Android.Graphics;
using Android.Util;

namespace XAppP001
{
    [Activity(Label = "@string/CalculatorActivity", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        EditText etNum1;
        EditText etNum2;
        Button btnAdd;
        Button btnSub;
        Button btnMult;
        Button btnDiv;
        TextView tvResult;
        String oper = String.Empty;

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.Main, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            menu.SetGroupVisible(Resource.Id.mgrpCalculator, true);
            return base.OnPrepareOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            // TODO Auto-generated method stub
            switch (item.ItemId)
            {
                case Resource.Id.miReset:
                    // очищаем поля
                    etNum1.Text = String.Empty;
                    etNum2.Text = String.Empty;
                    tvResult.Text = String.Empty;
                    break;
                case Resource.Id.miQuit:
                    // выход из приложения
                    //MoveTaskToBack(true);
                    Finish();
                    break;
                case Resource.Id.miDisplayMetrics:
                    Intent intent = new Intent(this, typeof(DisplayMetricsActivity));
                    StartActivity(intent);
                    break;
                case Resource.Id.miPreferences:
                    StartActivity(new Intent(this, typeof(PreferencesActivity)));
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            etNum1 = FindViewById<EditText>(Resource.Id.etNum1);
            etNum2 = FindViewById<EditText>(Resource.Id.etNum2);
            btnAdd = (Button)FindViewById(Resource.Id.btnAdd);
            btnSub = (Button)FindViewById(Resource.Id.btnSub);
            btnMult = (Button)FindViewById(Resource.Id.btnMult);
            btnDiv = (Button)FindViewById(Resource.Id.btnDiv);
            tvResult = (TextView)FindViewById(Resource.Id.tvResult);

            btnAdd.Click += btnOper_Click;
            btnSub.Click += btnOper_Click;
            btnMult.Click += btnOper_Click;
            btnDiv.Click += btnOper_Click;

            var sPref = GetSharedPreferences(PreferencesActivity.XAPPP001PREF_NAME, FileCreationMode.Private);
            String num1Val = sPref.GetString(PreferencesActivity.PREFKEY_CALCULATOR_NUM1_VALUE, "0");
            String num2Val = sPref.GetString(PreferencesActivity.PREFKEY_CALCULATOR_NUM2_VALUE, "0");

            etNum1.Text = num1Val;
            etNum2.Text = num2Val;

            if (bundle != null)
            {
                etNum1.Text = bundle.GetString(Resource.Id.etNum1.ToString(), num1Val);
                etNum2.Text = bundle.GetString(Resource.Id.etNum2.ToString(), num2Val);
                tvResult.Text = bundle.GetString(Resource.Id.tvResult.ToString(), String.Empty);
                Log.Debug(GetType().FullName, "Recovered instance state");
            }
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutString(Resource.Id.etNum1.ToString(), etNum1.Text);
            outState.PutString(Resource.Id.etNum2.ToString(), etNum2.Text);
            outState.PutString(Resource.Id.tvResult.ToString(), tvResult.Text);

            Log.Debug(GetType().FullName, "Saving instance state");

            base.OnSaveInstanceState(outState);
        }

        private void btnOper_Click(object sender, EventArgs e)
        {
            onClick((View)sender);
        }

        private void onClick(View v)
        {
            float num1 = 0;
            float num2 = 0;
            float result = 0;
            // Проверяем поля на пустоту
            if (TextUtils.IsEmpty(etNum1.Text)
            || TextUtils.IsEmpty(etNum2.Text))
            {
                return;
            }
            // читаем EditText и заполняем переменные числами
            num1 = float.Parse(etNum1.Text);
            num2 = float.Parse(etNum2.Text, CultureInfo.InvariantCulture);
            // определяем нажатую кнопку и выполняем соответствующую операцию
            // в oper пишем операцию, потом будем использовать в выводе
            switch (v.Id)
            {
                case Resource.Id.btnAdd:
                    oper = "+";
                    result = num1 + num2;
                    break;
                case Resource.Id.btnSub:
                    oper = "-";
                    result = num1 - num2;
                    break;
                case Resource.Id.btnMult:
                    oper = "*";
                    result = num1 * num2;
                    break;
                case Resource.Id.btnDiv:
                    if (num2 == 0)
                    {
                        Toast.MakeText(this, "Деление на ноль...", ToastLength.Long).Show();
                        return;
                    }
                    oper = "/";
                    result = num1 / num2;
                    break;
                default:
                    break;
            }
            // формируем строку вывода
            tvResult.Text = num1 + " " + oper + " " + num2 + " = " + result;
            var anim = AnimationUtils.LoadAnimation(this, Resource.Animation.tvResult);
            tvResult.StartAnimation(anim);
            ObjectAnimator bgAnim = ObjectAnimator.OfInt(tvResult, "backgroundColor", Color.White.ToArgb(), Color.Black.ToArgb());
            bgAnim.SetupStartValues();
            bgAnim.SetDuration(1500);
            bgAnim.Start();
        }
    }
}

