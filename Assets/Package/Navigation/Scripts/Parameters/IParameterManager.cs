using System.Collections.Generic;

namespace Elselam.UnityRouter.Extensions
{
    public interface IParameterManager
    {
        IParameter Create<T>(string key, T value);
        IDictionary<string, string> CreateDictionary(params IParameter[] parameters);
        T GetParamOfType<T>(IDictionary<string, string> parameters, string key);
    }
}