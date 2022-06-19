using System;
using System.Linq;
using Rocket.API;
using Rocket.Core.Plugins;
using SDG.Unturned;
using SherbetSpawnpoints.Models;
using SherbetSpawnpoints.Models.Config;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace SherbetSpawnpoints
{
    public partial class SpawnpointsPlugin : RocketPlugin<SpawnpointsConfig>
    {
        public SpawnpointsConfig Config => Configuration.Instance;

        public MapSpawnpoints CurrentMap
        {
            get
            {
                var map = Config.Maps.FirstOrDefault(x => x.MapName.Equals(Provider.map, StringComparison.InvariantCultureIgnoreCase));
                if (map == null)
                {
                    map = new MapSpawnpoints() { MapName = Provider.map };
                    Config.Maps.Add(map);
                }
                return map;
            }
        }

        public override void LoadPlugin()
        {
            base.LoadPlugin();

            Logger.Log("SherbetSpawnpoints v1.0 Loaded. By ShimmyMySherbet#5694");
            Logger.Log($"Loaded {CurrentMap.Spawnpoints.Count} spawnpoints for map {Provider.map}");
            PlayerLife.OnSelectingRespawnPoint += OnSpawnpointRequested;
            BarricadeManager.onDeployBarricadeRequested += OnDeployBarricadeRequested;
            StructureManager.onDeployStructureRequested += OnDeployStructureRequested;
        }

        public override void UnloadPlugin(PluginState state = PluginState.Unloaded)
        {
            base.UnloadPlugin(state);
            PlayerLife.OnSelectingRespawnPoint -= OnSpawnpointRequested;
            BarricadeManager.onDeployBarricadeRequested -= OnDeployBarricadeRequested;
            StructureManager.onDeployStructureRequested -= OnDeployStructureRequested;
        }

        private void OnSpawnpointRequested(PlayerLife sender, bool wantsToSpawnAtHome, ref UnityEngine.Vector3 position, ref float yaw)
        {
            if (wantsToSpawnAtHome && !Config.DisableHome)
            {
                return;
            }

            var newPos = SpawnUtility.SelectSpawn(CurrentMap, Config.EnableAreaSpawns);

            if (!newPos.IsValid)
            {
                return;
            }

            (position, yaw) = newPos.Deconstruct();
        }

        #region "Spawn Build Protection"

        private bool IsProtected(Vector3 point)
        {
            foreach (var p in CurrentMap.Spawnpoints)
            {
                if (Vector3.Distance(p.Position.Vector3, point) <= Config.SpawnProtectionRange)
                {
                    return true;
                }
            }
            return false;
        }

        private void OnDeployBarricadeRequested(Barricade barricade, ItemBarricadeAsset ast, Transform hit, ref Vector3 point,
            ref float ax, ref float zy, ref float az, ref ulong o, ref ulong g, ref bool shouldAllow)
        {
            if (!Config.ProtectSpawnpoints || !shouldAllow)
            {
                return;
            }

            if (IsProtected(point))
            {
                shouldAllow = false;
            }
        }

        private void OnDeployStructureRequested(Structure structure, ItemStructureAsset asset, ref Vector3 point,
            ref float angle_x, ref float angle_y, ref float angle_z, ref ulong owner, ref ulong group, ref bool shouldAllow)
        {
            if (!Config.ProtectSpawnpoints || !shouldAllow)
            {
                return;
            }

            if (IsProtected(point))
            {
                shouldAllow = false;
            }
        }

        #endregion "Spawn Build Protection"
    }
}