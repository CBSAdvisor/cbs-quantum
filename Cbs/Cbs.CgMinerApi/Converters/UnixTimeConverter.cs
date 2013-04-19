using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cbs.CgMinerApi.Converters
{
    public class UnixTimeConverter : JsonConverter
    {
        public UnixTimeConverter() 
            : base()
        {

        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) 
        {
            uint num;
            if (value is DateTime)
            {
                num = UnixTime.GetFromDateTime((DateTime)value);
            }
            else 
            {
                throw new JsonSerializationException("Expected date object value.");
            }
            writer.WriteValue(num);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.Integer || objectType != typeof(DateTime))
            {
                return null;
            }

            return UnixTime.ConvertToDateTime(Convert.ToUInt32(reader.Value));
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanConvert(Type objectType)
        {
            if ((objectType != typeof(DateTime)) && (objectType != typeof(DateTime?)))
            {
                return false;
            }

            return true;
        }
    }
}
