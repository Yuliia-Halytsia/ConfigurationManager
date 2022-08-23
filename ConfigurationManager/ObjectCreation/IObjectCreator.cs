using System.Collections.Generic;
using ConfigurationManager.Models;

namespace ConfigurationManager.ObjectCreation
{
    public interface IObjectCreator
    {
        T Create<T>(IEnumerable<ConfigurationProperty> properties);
    }
}