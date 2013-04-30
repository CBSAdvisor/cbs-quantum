using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cbs.CgMinerApi.Converters
{
    public class ResponseStatusConverter : JsonConverter
    {
        public ResponseStatusConverter() 
            : base()
        {

        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) 
        {
            string val = "F";

            if (value is eMsgStatus)
            {
                switch ((eMsgStatus)value)
                {
                    case eMsgStatus.Warning:
                        val = "W";
                        break;
                    case eMsgStatus.Informational:
                        val = "I";
                        break;
                    case eMsgStatus.Success:
                        val = "S";
                        break;
                    case eMsgStatus.Error:
                        val = "E";
                        break;
                    case eMsgStatus.Fatal:
                        val = "F";
                        break;
                    default:
                        val = "F";
                        break;
                }
            }
            else 
            {
                throw new JsonSerializationException("Expected eMsgStatus object value.");
            }
            writer.WriteValue(val);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.String || objectType != typeof(eMsgStatus))
            {
                return null;
            }

            eMsgStatus result = eMsgStatus.Fatal;

            switch ((string)reader.Value)
            { 
                case "W":
                    result = eMsgStatus.Warning;
                    break;
                case "I":
                    result = eMsgStatus.Informational;
                    break;
                case "S":
                    result = eMsgStatus.Success;
                    break;
                case "E":
                    result = eMsgStatus.Error;
                    break;
                case "F":
                    result = eMsgStatus.Fatal;
                    break;
                default:
                    result = eMsgStatus.Fatal;
                    break;
            }

            return result;
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanConvert(Type objectType)
        {
            if (objectType != typeof(eMsgStatus))
            {
                return false;
            }

            return true;
        }
    }
}
