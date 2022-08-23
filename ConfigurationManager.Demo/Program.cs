using System.Threading.Tasks;
using ConfigurationManager.Demo.Models;

namespace ConfigurationManager.Demo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var manager = new DemoConfigurationProvider("ConfigurationFiles\\Env-1-2.txt");
            var serviceSettings = await manager.GetAsync<ServiceSettings>();
            var applicationSettings = await manager.GetAsync<ApplicationSettings>();
        }
    }
}
