namespace ConfigurationManager.Demo.Models
{
    public class ApplicationSettings
    {
        public string Name { get; set; }
        public string Owner { get; set; }
        public AccessType AccessType { get; set; }
        public int Version { get; set; }
    }

    public enum AccessType
    {
        Public,
        Private
    }
}
