using System.Collections.Generic;
using System.Xml.Serialization;

namespace SherbetSpawnpoints.Models.Config
{
    public class MapSpawnpoints
    {
        [XmlAttribute]
        public string MapName;

        public List<ConfigSpawnpoint> Spawnpoints = new List<ConfigSpawnpoint>();
    }
}