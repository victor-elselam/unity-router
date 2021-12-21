using System.Collections.Generic;

namespace Elselam.UnityRouter.Extensions
{
    public interface IParameterManager
    {
        /// <summary>
        /// Create a single parameter with key and value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IParameter Create<T>(string key, T value);
        /// <summary>
        /// Create parameters dictionary with unlimited parameters
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IDictionary<string, string> CreateDictionary(params IParameter[] parameters);

        /// <summary>
        /// Helper to get desired value with a safe and clean sintax
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        T GetParamOfType<T>(IDictionary<string, string> parameters, string key, T defaultValue = default);
    }
}