using System;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace K2_betterware_Biostart_Assistance.Core.DTOs
{

    public class InfoToStringConverter : JsonConverter<string>
    {
        public override string Read(
            ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (var jsonDoc = JsonDocument.ParseValue(ref reader))
            {
                return jsonDoc.RootElement.GetRawText();
            }
        }

        public override void Write(
            Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }

    public class Data_event
    {
        public class raiz_data
        {
            [JsonConverter(typeof(InfoToStringConverter))]
            public string EventCollection { get; set; }
            [JsonConverter(typeof(InfoToStringConverter))]
            public string Response { get; set; }
        }


        public class intern_row
        {
            [JsonConverter(typeof(InfoToStringConverter))]
            public string rows { get; set; }
            //public List<Rows> rows { get; set; }

        }

        public class intern_data
        {


            [JsonConverter(typeof(InfoToStringConverter))]
            public string id { get; set; }
            [JsonConverter(typeof(InfoToStringConverter))]
            public string server_datetime { get; set; }
            [JsonConverter(typeof(InfoToStringConverter))]
            public string datetime { get; set; }
            [JsonConverter(typeof(InfoToStringConverter))]
            public string index { get; set; }

            [JsonConverter(typeof(InfoToStringConverter))]
            public string user_group_id { get; set; }
            [JsonConverter(typeof(InfoToStringConverter))]
            public string device_id { get; set; }
            [JsonConverter(typeof(InfoToStringConverter))]
            public string event_type_id { get; set; }
            [JsonConverter(typeof(InfoToStringConverter))]
            public string is_dst { get; set; }
            [JsonConverter(typeof(InfoToStringConverter))]
            public string timezone { get; set; }
            [JsonConverter(typeof(InfoToStringConverter))]
            public string user_update_by_device { get; set; }
            [JsonConverter(typeof(InfoToStringConverter))]
            public string hint { get; set; }
            [JsonConverter(typeof(InfoToStringConverter))]
            public string user_id_name { get; set; }
        }

        public class intern_in_data
        {

            [JsonConverter(typeof(InfoToStringConverter))]
            public string id { get; set; }
            [JsonConverter(typeof(InfoToStringConverter))]
            public string name { get; set; }

            [JsonConverter(typeof(InfoToStringConverter))]
            public string code { get; set; }
            [JsonConverter(typeof(InfoToStringConverter))]
            public string half { get; set; }
            [JsonConverter(typeof(InfoToStringConverter))]
            public string hour { get; set; }
            [JsonConverter(typeof(InfoToStringConverter))]
            public string negative { get; set; }

            [JsonConverter(typeof(InfoToStringConverter))]
            public string user_id_name { get; set; }


        }

        public class intern_in_usr
        {
            [JsonConverter(typeof(InfoToStringConverter))]
            public string user_id { get; set; }

            [JsonConverter(typeof(InfoToStringConverter))]
            public string name { get; set; }

            [JsonConverter(typeof(InfoToStringConverter))]
            public string photo_exists { get; set; }
        }




    }
}