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
using Android.Util;

namespace XAppP001
{
    [Activity(Label = "@string/DisplayMetricsActivity", Icon = "@drawable/DisplayMetrics")]
    public class DisplayMetricsActivity : Activity
    {
        private TextView tvDensityDpiValue;
        private TextView tvDensityValue;
        private TextView tvScaledDensityValue;
        private TextView tvHeightPixelsValue;
        private TextView tvWidthPixelsValue;
        private TextView tvXdpiValue;
        private TextView tvYdpiValue;

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.Main, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            return base.OnPrepareOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            // TODO Auto-generated method stub
            switch (item.ItemId)
            {
                case Resource.Id.miQuit:
                    // выход из приложения
                    Finish();
                    break;
                case Resource.Id.miCalculator:
                    Intent intent = new Intent(this, typeof(MainActivity));
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

            SetContentView(Resource.Layout.DisplayMetrics);
            DisplayMetrics metrics = Resources.DisplayMetrics;

            tvDensityValue = FindViewById<TextView>(Resource.Id.tvDensityValue);
            tvDensityValue.Text = metrics.Density.ToString();

            tvDensityDpiValue = FindViewById<TextView>(Resource.Id.tvDensityDpiValue);
            tvDensityDpiValue.Text = metrics.DensityDpi.ToString();

            tvScaledDensityValue = FindViewById<TextView>(Resource.Id.tvScaledDensityValue);
            tvScaledDensityValue.Text = metrics.ScaledDensity.ToString();

            tvHeightPixelsValue = FindViewById<TextView>(Resource.Id.tvHeightPixelsValue);
            tvHeightPixelsValue.Text = metrics.HeightPixels.ToString();

            tvWidthPixelsValue = FindViewById<TextView>(Resource.Id.tvWidthPixelsValue);
            tvWidthPixelsValue.Text = metrics.WidthPixels.ToString();

            tvXdpiValue = FindViewById<TextView>(Resource.Id.tvXdpiValue);
            tvXdpiValue.Text = metrics.Xdpi.ToString();

            tvYdpiValue = FindViewById<TextView>(Resource.Id.tvYdpiValue);
            tvYdpiValue.Text = metrics.Ydpi.ToString();
        }
    }
}