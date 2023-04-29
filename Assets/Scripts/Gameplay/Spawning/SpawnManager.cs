using JetBrains.Annotations;
using OTBG.Utility;
using Sirenix.OdinInspector;
using UnityEngine.Events;

public enum EntityType
{
    Player,
    Enemy,
    Destructables
}

public class SpawnManager : MonoSingleton<SpawnManager>
{
    public SpawnArea currentArea;

    [Button]
    public void StartSpawnArea(SpawnArea spawnArea)
    {
        //TODO Lock Camera to not go passed a point.
        //TODO Make the spawn area start spawning units until all dead.

        currentArea = spawnArea;
        spawnArea.StartWave();
    }

    public void FinishSpawnArea()
    {
        if (currentArea == null)
            return;

        currentArea = null;

        print("Spawn Area Completed");
        //TODO Announce an event or something announcing the area is completed so we can move on.
    }
}
