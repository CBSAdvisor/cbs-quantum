using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Cbs.CgMinerApi
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GpuSection : ImplementNotifyPropertyChanged
    {
        private int _gpu;
        private string _enabled;
        private string _status;
        private float _temperature;
        private int _fanSpeed;
        private int _fanPercent;
        private int _gpuClock;
        private int _memoryClock;
        private float _gpuVoltage;
        private int _gpuActivity;
        private int _powertune;
        private float _mhsAv;
        private float _mhs5s;
        private int _accepted;
        private int _rejected;
        private int _hardwareErrors;
        private float _utility;
        private string _intensity;
        private int _lastSharePool;
        private int _lastShareTime;
        private float _totalMH;
        private int _diff1Work;
        private float _difficultyAccepted;
        private float _difficultyRejected;
        private float _lastShareDifficulty;
        private int _lastValidWork;

        private static CultureInfo _enUsCulrureInfo = CultureInfo.GetCultureInfo("en-US");

        public GpuSection()
        {
        }

        public static GpuSection ReadFromJObject(JObject jObj)
        {
            GpuSection gpuInfo = new GpuSection();

            return gpuInfo;
        }

        #region Properties

        [JsonProperty("GPU")]
        public int GPU
        {
            get { return _gpu; }
            set { _gpu = value; NotifyPropertyChanged(); }
        }

        [JsonProperty("Enabled")]
        public string Enabled
        {
            get { return _enabled; }
            set { _enabled = value; NotifyPropertyChanged(); }
        }
        public string Status { get; set; }
        public float Temperature { get; set; }
        public int FanSpeed { get; set; }
        public int FanPercent { get; set; }
        public int GPUClock { get; set; }
        public int MemoryClock { get; set; }
        public float GPUVoltage { get; set; }

        [JsonProperty("GPU Activity")]
        public int GPUActivity
        {
            get { return _gpuActivity; }
            set { _gpuActivity = value; NotifyPropertyChanged(); }
        }

        public int Powertune { get; set; }

        [JsonProperty("MHS 5s")]
        public float MHS5s
        {
            get { return _mhs5s; }
            set { _mhs5s = value; NotifyPropertyChanged(); }
        }

        [JsonProperty("MHS av")]
        public float MHSav
        {
            get { return _mhsAv; }
            set { _mhsAv = value; NotifyPropertyChanged(); }
        }

        [JsonProperty("Accepted")]
        public int Accepted
        {
            get { return _accepted; }
            set { _accepted = value; NotifyPropertyChanged(); }
        }

        public int Rejected { get; set; }
        public int HardwareErrors { get; set; }
        public float Utility { get; set; }
        public string Intensity { get; set; }
        public int LastSharePool { get; set; }
        public int LastShareTime { get; set; }

        [JsonProperty("Total MH")]
        public float TotalMH
        {
            get { return _totalMH; }
            set { _totalMH = value; NotifyPropertyChanged(); }
        }

        public int Diff1Work { get; set; }
        public float DifficultyAccepted { get; set; }
        public float DifficultyRejected { get; set; }
        public float LastShareDifficulty { get; set; }
        public int LastValidWork { get; set; }

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
