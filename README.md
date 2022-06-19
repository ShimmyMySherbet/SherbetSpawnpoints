# SherbetSpawnpoints
A Custom Spawnpoints plugin for Unturned

## Commands


### **/AddSpawnpoint** `[Range]`
Creates a new spawn point based on your current position. `Range` is the random spawn range around the position players will spawn when Area Spawns are enabled, and defaults to 0 (disabled).

### **/RemoveSpawnpoint**
Deletes the closest spawnpoint to you, with a range limit of 40m.

## Config

### EnableAreaSpawns
Enables/Disables range spawns. When enabled, players will spawn in a random position around a random spawnpoint. Max range is specified when creating the spawnpoint.

### DisableHome
When enabled, the home button on the respawn screen is disabled, always forcing a new spawnpoint instead.

### ProtectSpawnpoints
When enabled, players will be unable to place items around the spawnpoints. Similar to vanilla spawnpoint building protections.

### SpawnProtectionRange
Specifies the range around a spawnpoint building protection extends.

### Maps
Stores the per-map spawnpoints. Use the `/AddSpawnpoint` and `/RemoveSpawnpoint` commands instead of editing this directly.