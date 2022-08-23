using System.Threading.Tasks;
using ConfigurationManager.FileProcessing;
using ConfigurationManager.ObjectCreation;
using ConfigurationManager.PropertiesProcessing;
using ConfigurationManager.TextProcessing;

namespace ConfigurationManager.Demo
{
    public class DemoConfigurationProvider : IConfigurationProvider
    {
        private readonly ConfigurationProvider _configurationProvider;

        public DemoConfigurationProvider(string filePath)
        {
            _configurationProvider = new ConfigurationProvider(
                new FileConfigurationPropertiesProvider(
                    new FileHierarchyManager(
                        new FileHierarchyBuilder(
                            new DashFileNameIterator(),
                            filePath), 
                        new FileHierarchyProcessor(
                            new FileHierarchyValidator())), 
                    new FilePropertyParsingFacade( 
                        new FileConfigurationProcessor(
                            new ConfigurationPropertyParser()),
                        new ExceptionOnParsingErrorStrategy()),
                    new PropertyValidationFacade(
                        new SemanticValidator(), 
                        new ExceptionOnSemanticErrorStrategy())),
                new TypeMembersProvider(),
                new CreationViaReflection());
        }

        public async Task<T> GetAsync<T>() => await _configurationProvider.GetAsync<T>();
    }
}