using Rocket.API.Collections;

namespace SherbetSpawnpoints
{
    public partial class SpawnpointsPlugin
    {
        public override TranslationList DefaultTranslations => new TranslationList()
        {
            { "Spawnpoint_Added", "[color=green]Created spawnpoint with spawn range {0}[/color]" },
            { "Spawnpoint_Removed", "[color=orange]Removed nearby spawnpoint at {0}, with range of {1}[/color]" },
            { "Spawnpoint_Remove_NotFound", "[color=red]No nearby spawnpoint to remove[/color]" }
        };
    }
}