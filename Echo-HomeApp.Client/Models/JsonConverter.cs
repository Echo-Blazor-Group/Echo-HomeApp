using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class JsonConverter
{
    public List<string> MyList { get; set; }
}

public class MyObjectConverter : JsonConverter
{
    //public override bool CanConvert(Type objectType)
    //{
    //    return objectType == typeof(JsonConverter);
    //}

    //public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
    //{
    //    JObject jsonObject = JObject.Load(reader);
    //    JsonConverter obj = new JsonConverter();
    //    obj.MyList = jsonObject["myList"].ToObject<List<string>>();
    //    return obj;
    //}

    //public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
    //{
    //    JsonConverter obj = (JsonConverter)value;
    //    JObject jsonObject = new JObject();
    //    jsonObject.Add("myList", JArray.FromObject(obj.MyList));
    //    jsonObject.WriteTo(writer);
    //}
}

