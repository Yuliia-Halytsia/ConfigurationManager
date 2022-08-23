using System;

namespace ConfigurationManager.Models
{
    public class TypeMemberInfo
    {
        public string Name { get; }
        public Type Type { get; }

        public TypeMemberInfo(string name, Type type)
        {
            Name = name;
            Type = type;
        }
    }
}