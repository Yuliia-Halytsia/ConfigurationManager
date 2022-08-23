# ConfigurationManager

Custom configuration manager to read settings from file. Implemented hierarchy of config files and validation.

# Example

```
var manager = new DemoConfigurationProvider(filePath);
var settings = await manager.GetAsync<T>();
````