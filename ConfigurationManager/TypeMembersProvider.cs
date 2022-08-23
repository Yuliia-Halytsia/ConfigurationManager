using System.Collections.Generic;
using System.Linq;
using ConfigurationManager.Models;

namespace ConfigurationManager
{
    public interface ITypeMembersProvider
    {
        IReadOnlyCollection<TypeMemberInfo> GetEligibleMembers<T>();
    }

    public class TypeMembersProvider : ITypeMembersProvider
    {
        public IReadOnlyCollection<TypeMemberInfo> GetEligibleMembers<T>()
            => typeof(T).GetProperties()
                .Select(m => new TypeMemberInfo($"{m.DeclaringType?.FullName}.{m.Name}", m.PropertyType))
                .ToArray();
    }
}