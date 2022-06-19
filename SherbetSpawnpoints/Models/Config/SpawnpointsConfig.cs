using System.Collections.Generic;
using System.Xml.Serialization;
using Rocket.API;

namespace SherbetSpawnpoints.Models.Config
{
    public class SpawnpointsConfig : IRocketPluginConfiguration
    {
        public bool EnableAreaSpawns = false;

        public bool DisableHome = false;

        public bool ProtectSpawnpoints = false;

        public float SpawnProtectionRange = 50f;

        [XmlArrayItem(ElementName = "Map")]
        public List<MapSpawnpoints> Maps = new List<MapSpawnpoints>();

        public void LoadDefaults()
        {
        }
    }
}