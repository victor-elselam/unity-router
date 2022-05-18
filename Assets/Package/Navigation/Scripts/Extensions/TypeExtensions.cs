using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Elselam.UnityRouter.Extensions
{
    public static class TypeExtensions
    {

        private static IEnumerable<Type> assemblies;

        public static Type GetTypeByName(string typeName)
        {
            assemblies ??= AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes());
            return assemblies.FirstOrDefault(t => t.Name == typeName);
        }

        public static MemberInfo[] GetProperties (this object obj)
        {
            return obj.GetType().GetMembers().Where(m => m.MemberType == MemberTypes.Property).ToArray();
        }
    }
}