using Cysharp.Threading.Tasks;
using RocketExtensions.Models;
using RocketExtensions.Plugins;
using SherbetSpawnpoints.Models.Config;
using UnityEngine;

namespace SherbetSpawnpoints.Commands
{
    public class RemoveSpawnpointCommand : RocketCommand
    {
        public override async UniTask Execute(CommandContext context)
        {
            ConfigSpawnpoint spawnpoint = null;

            foreach (var p in Plugin.CurrentMap.Spawnpoints)
            {
                if (Vector3.Distance(p.Position.Vector3, context.LDMPlayer.Position) <= 40f)
                {
                    spawnpoint = p;
                    break;
                }
            }

            if (spawnpoint == null)
            {
                await context.ReplyKeyAsync("Spawnpoint_Remove_NotFound");
                return;
            }

            Plugin.CurrentMap.Spawnpoints.Remove(spawnpoint);
            await context.ReplyKeyAsync("Spawnpoint_Removed", spawnpoint.Position, spawnpoint.Position.Rot);
        }

        public new SpawnpointsPlugin Plugin =>
            base.Plugin as SpawnpointsPlugin;
    }
}