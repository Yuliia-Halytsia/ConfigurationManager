using System;
using System.ComponentModel;

namespace ConfigurationManager.Utils
{
    public static class Converter
    {
        public static T Convert<T>(this string value)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            return (T)converter.ConvertFromString(value);
        }

        public static object Convert(this string value, Type type)
        {
            var converter = TypeDescriptor.GetConverter(type);
            return converter.ConvertFromString(value);
        }

        public static bool TryConvert(this string value, Type type, out object convertedValue)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(type);
                convertedValue = converter.ConvertFromString(value);
                return true;
            }
            catch
            {
                convertedValue = null;
                return false;
            }
        }
    }
}
