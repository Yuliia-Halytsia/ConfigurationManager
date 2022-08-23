using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ConfigurationManager.Models;
using ConfigurationManager.Utils;
using PropertyInfo = System.Reflection.PropertyInfo;

namespace ConfigurationManager.ObjectCreation
{
    public class CreationViaReflection : IObjectCreator
    {
        public T Create<T>(IEnumerable<ConfigurationProperty> properties)
        {
            var instance = Activator.CreateInstance<T>();
            var members = typeof(T).GetMembers();
            foreach (var property in properties)
            {
                var member = members.First(p => p.Name == property.PropertyName);

                switch (member.MemberType)
                {
                    case MemberTypes.Field:
                        var fieldInfo = (FieldInfo)member;
                        fieldInfo.SetValue(instance, property.Value.Convert(fieldInfo.FieldType));
                        break;
                    case MemberTypes.Property:
                        var propertyInfo = (PropertyInfo)member;
                        propertyInfo.SetValue(instance, property.Value.Convert(propertyInfo.PropertyType));
                        break;
                    default:
                        throw new ArgumentException("MemberInfo must be of type FieldInfo or PropertyInfo");
                }
            }

            return instance;
        }
    }
}
