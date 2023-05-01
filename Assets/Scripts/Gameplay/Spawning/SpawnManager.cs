using JetBrains.Annotations;
using OTBG.Utility;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.Events;

public enum EntityType
{
    Player,
    Enemy,
    Destructables
}

public class SpawnManager : MonoSingleton<SpawnManager>
{
    public static event Action<SpawnArea> OnAreaStarted;
    public static event Action<SpawnArea> OnAreaFinished;


    public SpawnArea currentArea;

    [Button]
    public void StartSpawnArea(SpawnArea spawnArea)
    {
        //TODO Lock Camera to not go passed a point.
        //TODO Make the spawn area start spawning units until all dead. (DONE)

        currentArea = spawnArea;
        spawnArea.StartWave();
        OnAreaStarted?.Invoke(currentArea);
        
        CarriageController.Instance.MoveToNextLocation(currentArea.wagonPosition.position);
    }

    public void FinishSpawnArea()
    {
        if (currentArea == null)
            return;

        OnAreaFinished?.Invoke(currentArea);

        currentArea = null;

        print("Spawn Area Completed");
        //TODO Announce an event or something announcing the area is completed so we can move on.
    }
}
