using Newtonsoft.Json;

namespace Elselam.UnityRouter.Extensions
{
    internal class ParamTyped<T> : IParameter
    {
        [JsonProperty("Key")]
        public string Key { get; }

        [JsonProperty("Value")]
        public string Value { get; }
        internal ParamTyped(string key, T value)
        {
            Key = key;
            Value = value is string text ? text : JsonConvert.SerializeObject(value);
        }
    }
}