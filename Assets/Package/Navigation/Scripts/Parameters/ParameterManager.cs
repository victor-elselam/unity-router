using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elselam.UnityRouter.Extensions
{
    public class ParameterManager : IParameterManager
    {
        public IParameter Create<T>(string key, T value) => new ParamTyped<T>(key, value);

        public IDictionary<string, string> CreateDictionary(params IParameter[] parameters)
        {
            var dictio = new Dictionary<string, string>();
            foreach (var param in parameters)
                dictio[param.Key] = param.Value;

            return dictio;
        }

        public T GetParamOfType<T>(IDictionary<string, string> parameters, string key, T defaultValue = default)
        {
            if (parameters.IsNullOrEmpty())
                return defaultValue;

            var hasValue = parameters.TryGetValue(key, out string result);
            if (!hasValue)
                return defaultValue;

            try
            {
                var value = JsonConvert.DeserializeObject<T>(result);
                return value;
            }
            catch (Exception e)
            {
                var type = typeof(T);
                var expected = JsonConvert.SerializeObject(Activator.CreateInstance(type));
                Debug.LogError(
                    $"Error parsing parameter {key} of type {typeof(T)} \n" +
                    $"Expected: {expected} \n" +
                    $"Found: {result} \n" +
                    $"Exception: {e}");
                return default;
            }
        }

        
    }
}