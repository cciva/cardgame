using System.IO;
using YamlDotNet;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Game.Lib
{
    public class ConfReader
    {
        public T ReadConf<T>(string file)
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .IgnoreUnmatchedProperties()        
                .Build();

            string content = File.ReadAllText(file);
            return deserializer.Deserialize<T>(content);
        }
    }
}