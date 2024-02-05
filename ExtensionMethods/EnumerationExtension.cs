using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace ExtensionMethods
{
    public static class EnumerationExtension
    {
        public static string Description(this Enum enumeration)
        {
            try
            {
                return enumeration.GetType().GetMember(enumeration.ToString()).FirstOrDefault().GetCustomAttribute<DisplayAttribute>().GetName();
            }
            catch (Exception)
            {
                return nameof(enumeration);
            }
        }
    }
}