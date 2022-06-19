using Cysharp.Threading.Tasks;
using RocketExtensions.Models;
using RocketExtensions.Plugins;
using SherbetSpawnpoints.Models.Config;

namespace SherbetSpawnpoints.Commands
{
    public class AddSpawnpointCommand : RocketCommand
    {
        public override async UniTask Execute(CommandContext context)
        {
            var range = context.Arguments.Get(0, 0f);
            var newPoint = new ConfigSpawnpoint()
            {
                SpawnRange = range,
                Position = Position.FromVector3(context.LDMPlayer.Position, context.LDMPlayer.Rotation)
            };

            Plugin.CurrentMap.Spawnpoints.Add(newPoint);
            Plugin.Configuration.Save();

            await context.ReplyKeyAsync("Spawnpoint_Added", range);
        }

        public new SpawnpointsPlugin Plugin =>
            base.Plugin as SpawnpointsPlugin;
    }
}