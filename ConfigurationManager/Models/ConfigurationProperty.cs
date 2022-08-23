using System;

namespace ConfigurationManager.Models
{
    public class ConfigurationProperty
    {
        public string Namespace { get; set; }
        public string ClassName { get; set; }
        public string PropertyName { get; set; }
        public string Value { get; set; }

        public string FullName =>  $"{Namespace ?? string.Empty}.{ClassName}.{PropertyName}".TrimStart('.');
    }

    public class ConfigurationTypeProperty : ConfigurationProperty
    {
        public Type Type { get; set; }
    }
}