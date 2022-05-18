using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public IDictionary<string, string> CreateDictionary(object obj)
        {
            var members = obj.GetProperties();
            if (members.IsNullOrEmpty())
                return new Dictionary<string, string>();

            var parameters = new IParameter[members.Length];
            for (var i = 0; i < members.Length; i++)
            {
                var member = members[i];
                parameters[i] = Create(member.Name, ((PropertyInfo)member).GetValue(obj));
            }

            return CreateDictionary(parameters);
        }

        public T ParametersToObject<T>(IDictionary<string, string> parameters)
        {
            var obj = Activator.CreateInstance<T>();
            var properties = obj.GetProperties();

            foreach (var param in parameters)
            {
                var property = properties.FirstOrDefault(p => p.Name == param.Key);
                if (property == null)
                {
                    Debug.Log($"No property found for parameter: {param.Key}");
                    continue;
                }

                ((PropertyInfo) property).SetValue(obj, param.Value);
            }

            return obj;
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