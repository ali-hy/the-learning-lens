using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEngine;

public static class JsonHelper
{
    private static readonly JsonSerializerSettings settings = new JsonSerializerSettings
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
    };

    public static string ToJson(object obj)
    {
        return JsonConvert.SerializeObject(obj, settings);
    }

    public static T FromJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json, settings);
    }
}
