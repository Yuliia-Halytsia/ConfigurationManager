using System.Threading.Tasks;
using ConfigurationManager.ObjectCreation;
using ConfigurationManager.PropertiesProcessing;

namespace ConfigurationManager
{
    public interface IConfigurationProvider
    {
        Task<T> GetAsync<T>();
    }

    public class ConfigurationProvider : IConfigurationProvider
    {
        private readonly IConfigurationPropertiesProvider _configurationPropertiesProvider;
        private readonly ITypeMembersProvider _typeMembersProvider;
        private readonly IObjectCreator _objectCreator;

        public ConfigurationProvider(
            IConfigurationPropertiesProvider configurationPropertiesProvider,
            ITypeMembersProvider typeMembersProvider,
            IObjectCreator objectCreator)
        {
            _configurationPropertiesProvider = configurationPropertiesProvider;
            _typeMembersProvider = typeMembersProvider;
            _objectCreator = objectCreator;
        }

        public async Task<T> GetAsync<T>()
        {
            var eligibleProperties = _typeMembersProvider.GetEligibleMembers<T>();
            var properties = await _configurationPropertiesProvider.GetAsync<T>(eligibleProperties);
            return _objectCreator.Create<T>(properties);
        }
    }
}