using System.Text.RegularExpressions;
using ConfigurationManager.Models;

namespace ConfigurationManager.TextProcessing
{
    public interface IConfigurationPropertyParser
    {
        bool TryParse(string line, out ConfigurationProperty property);
        bool IsComment(string line);
    }

    public class ConfigurationPropertyParser : IConfigurationPropertyParser
    {
        private const string Template = @"^([a-zA-Z_](\w)+\.){1,}([a-zA-Z_](\w)+)+=((\w)+.*)+$";

        public bool IsComment(string line)
            => Regex.IsMatch(line, @"^[#(\/\/)](.*)+");

        public bool TryParse(string line, out ConfigurationProperty property)
        {
            if (!Regex.IsMatch(line, Template))
            {
                property = null;
                return false;
            }    
            
            var separatorIndex = line.IndexOf('=');
            var propertyInfo = line[..separatorIndex].Split('.');
            property = new ConfigurationProperty
            {
                Namespace = string.Join('.', propertyInfo[..^2]),
                ClassName = propertyInfo[^2],
                PropertyName = propertyInfo[^1],
                Value = line[++separatorIndex..]
            };

            return true;
        }
    }
}