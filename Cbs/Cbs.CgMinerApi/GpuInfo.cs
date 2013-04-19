using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Cbs.CgMinerApi
{
    public class GpuInfo : ImplementNotifyPropertyChanged
    {
        private long _accepted;
        private float _acceptedDifficulty;
        private int _activity;
        private int _clock;
        private string _devicePath;
        private float _diff1Work;
        private string _driver;
        private bool _enabled;
        private long _errors;
        private int _fanPercent;
        private int _fanSpeed;
        private int _id;
        private int _intensity;
        private string _kernel;
        private float _lastShareDifficulty;
        private int _lastSharePool;
        private long _lastShareTime;
        private long _lastValidWork;
        private int _memory;
        private float _mhs5sec;
        private float _mhsavg;
        private int _powertune;
        private long _rejected;
        private float _rejectedDifficulty;
        private string _returnString;
        private string _status;
        private float _temperature;
        private double _totalMH;
        private float _utility;
        private float _voltage;

        private static CultureInfo _enUsCulrureInfo = CultureInfo.GetCultureInfo("en-US");

        public GpuInfo()
        {
        }

        public static GpuInfo ReadFromJObject(JObject jObj)
        {
            GpuInfo gpuInfo = new GpuInfo();

            gpuInfo.Accepted = jObj.Value<long>("accepted");
            gpuInfo.Activity = jObj.Value<int>("activity");
            gpuInfo.Mhs5sec = jObj.Value<float>("mhs 5s");
            gpuInfo.Mhsavg = jObj.Value<float>("mhs av");
            gpuInfo.TotalMH = jObj.Value<double>("total mh");

            return gpuInfo;
        }

        #region Properties

        public long Accepted
        {
            get { return _accepted; }
            set { _accepted = value; NotifyPropertyChanged(); }
        }

        public int Activity
        {
            get { return _activity; }
            set { _activity = value; NotifyPropertyChanged(); }
        }

        public float Mhs5sec
        {
            get { return _mhs5sec; }
            set { _mhs5sec = value; NotifyPropertyChanged(); }
        }

        public float Mhsavg
        {
            get { return _mhsavg; }
            set { _mhsavg = value; NotifyPropertyChanged(); }
        }

        public double TotalMH
        {
            get { return _totalMH; }
            set { _totalMH = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region Private methods

        private static string GetValue(Dictionary<string, string> dictionary, string key)
        {
            string value = "n/a";
            dictionary.TryGetValue(key, out value);
            return value;
        }

        private static long GetLongValue(Dictionary<string, string> dictionary, string key)
        {
            long result = 0;
            long.TryParse(GetValue(dictionary, key), NumberStyles.Integer, _enUsCulrureInfo, out result);
            return result;
        }

        private static int GetIntValue(Dictionary<string, string> dictionary, string key)
        {
            int result = 0;
            int.TryParse(GetValue(dictionary, key), NumberStyles.Integer, _enUsCulrureInfo, out result);
            return result;
        }

        private static float GetFloatValue(Dictionary<string, string> dictionary, string key)
        {
            float result = 0;
            float.TryParse(GetValue(dictionary, key), NumberStyles.Float, _enUsCulrureInfo, out result);
            return result;
        }

        private static double GetDoubleValue(Dictionary<string, string> dictionary, string key)
        {
            double result = 0;
            double.TryParse(GetValue(dictionary, key), NumberStyles.Number, _enUsCulrureInfo, out result);
            return result;
        }

        #endregion
    }
}
