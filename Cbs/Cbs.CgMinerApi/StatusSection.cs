using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cbs.CgMinerApi.Converters;
using Newtonsoft.Json;

namespace Cbs.CgMinerApi
{
    public enum eMsgStatus
    {
        Warning,
        Informational,
        Success,
        Error,
        Fatal
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class StatusSection : ImplementNotifyPropertyChanged
    {
        private string _status;
        private DateTime _when;
        private int _code;
        private string _msg;
        private string _description;

        public StatusSection()
        {
        }

        #region Properties

        [JsonProperty("STATUS")]
        public string Status
        {
            get { return _status; }
            set { _status = value; NotifyPropertyChanged(); }
        }

        [JsonProperty("When")]
        [JsonConverter(typeof(UnixTimeConverter))]
        public DateTime When
        {
            get { return _when; }
            set { _when = value; NotifyPropertyChanged(); }
        }

        [JsonProperty("Code")]
        public int Code
        {
            get { return _code; }
            set { _code = value; NotifyPropertyChanged(); }
        }

        [JsonProperty("Msg")]
        public string Msg
        {
            get { return _msg; }
            set { _msg = value; NotifyPropertyChanged(); }
        }

        [JsonProperty("Description")]
        public string Description
        {
            get { return _description; }
            set { _description = value; NotifyPropertyChanged(); }
        }

        #endregion
    }
}
