using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEngine;

public static class JsonHelper
{
    public static T FromJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings() {
            ContractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            }
        });
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}
