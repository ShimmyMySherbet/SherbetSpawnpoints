using SDG.Unturned;
using SherbetSpawnpoints.Models.Config;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SherbetSpawnpoints.Models
{
    public class SpawnUtility
    {
        public static Position SelectSpawn(MapSpawnpoints spawnpoints, bool lerp)
        {
            if (spawnpoints.Spawnpoints.Count == 0)
            {
                // Invalid Spawnpoint
                return new Position();
            }

            // Min is inclusive, while Max is exclusive, so no need to shift index
            var spawnIndex = Random.Range(0, spawnpoints.Spawnpoints.Count);
            var spawn = spawnpoints.Spawnpoints[spawnIndex];

            if (lerp)
            {
                var Lerped = LerpSpawn(spawn.Position, spawn.SpawnRange);

                // Get ground height
                var y = LevelGround.getHeight(new Vector3(Lerped.x, 0, Lerped.z));

                // TODO: Check for object obstructions

                return new Position() { Rot = spawn.Position.Rot, X = Lerped.z, Y = y, Z = Lerped.z };
            }
            return spawn.Position;
        }

        public static (float x, float z) LerpSpawn(Position position, float range)
        {
            // Use basic rejection sampling with a limit of 5

            var centrePos = new Vector2(position.X, position.Z);

            for (int i = 0; i < 5; i++)
            {
                var xLerp = Random.Range(-1f, 1f);
                var zLerp = Random.Range(-1f, 1f);

                var xOffset = range * xLerp;
                var zOffset = range * zLerp;

                var x = position.X + xOffset;
                var z = position.Z + zOffset;

                var randPos = new Vector2(x, z);

                // Check if it is within the circle
                if (Vector2.Distance(centrePos, randPos) <= range)
                {
                    return (x, z);
                }
            }

            // Limit reached, return origin
            return (position.X, position.Z);
        }
    }
}