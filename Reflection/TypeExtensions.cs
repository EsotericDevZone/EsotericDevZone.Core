using System;
using System.Linq;

namespace EsotericDevZone.Core
{
    public static partial class Reflection
    {
        /// <summary>
        /// Checks if type implements an interface
        /// </summary>        
        /// <example>
        /// typeof(List<int>).ImplementsInterface(IList<>) // true
        /// typeof(List<int>).ImplementsInterface(IList<int>) // true
        /// typeof(List<int>).ImplementsInterface(IList<double>) // false        
        /// </example>
        /// <returns>true if type implements interface, false otherwise</returns>
        public static bool ImplementsInterface(this Type type, Type @interface)
        {
            Validation.ThrowIf(!@interface.IsInterface, new ArgumentException("Target test type must be an interface"));            

            if (@interface.IsConstructedGenericType) 
            {
                return type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == @interface);
            }            
            return type.GetInterfaces().Any(i => i.FullName == @interface.FullName);
        }
            

    }
}
