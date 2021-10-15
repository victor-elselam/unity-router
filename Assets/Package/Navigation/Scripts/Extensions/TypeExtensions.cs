using System;
using System.Collections.Generic;
using System.Linq;

namespace elselam.Navigation.Extensions {
    public static class TypeExtensions {

        private static IEnumerable<Type> assemblies;
        
        public static Type GetTypeByName(string typeName) {
            assemblies ??= AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes());
            return assemblies.FirstOrDefault(t => t.Name == typeName);
        }
    }
}