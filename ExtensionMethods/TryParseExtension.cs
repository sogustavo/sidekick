using System;
using System.ComponentModel;

namespace ExtensionMethods
{
    public static class TryParseExtension
    {
        public static T? TryParse<T>(this object obj) where T : struct
        {
            if (obj is null)
            {
                return null;
            }

            T? result = null;

            TypeConverter tc = TypeDescriptor.GetConverter(typeof(T));

            if (tc is not null)
            {
                try
                {
                    var value = obj.ToString();

                    result = (T)tc.ConvertFromString(value);
                }
                catch (Exception) { }
            }

            return result;
        }
    }
}