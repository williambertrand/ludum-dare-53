using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using OTBG.Extensions;
using Sirenix.OdinInspector;

public class SpawnArea : MonoBehaviour
{
    public List<Entity> spawnedEntities = new List<Entity>();
    public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

    public Wave wave;
    public int enemyGroupIndex;
    public int numberOfEnemiesDefeated;

    private Coroutine coroutine_StartWaveLoop;

    public void StartWave()
    {
        if(coroutine_StartWaveLoop == null)
            coroutine_StartWaveLoop = StartCoroutine(StartWaveLoop());
    }

    public void FinishWave()
    {
        if(coroutine_StartWaveLoop != null)
        {
            StopCoroutine(coroutine_StartWaveLoop);
            coroutine_StartWaveLoop = null;
        }    
    }

    public IEnumerator StartWaveLoop()
    {
        while(enemyGroupIndex < wave.enemyGroups.Count)
        {
            EnemyGroup group = wave.enemyGroups[enemyGroupIndex];

            yield return SpawnGroup(group);

            enemyGroupIndex++;

            yield return new WaitForSeconds(wave.timeBetweenGroups);
        }

        SpawnAreaCompleted();

    }

    public IEnumerator SpawnGroup(EnemyGroup group)
    {
        Queue<Entity> queue = new Queue<Entity>();
        group.enemies.ForEach(e => queue.Enqueue(e));
        while(queue.Count > 0)
        {
            Entity entity = queue.Dequeue();
            yield return new WaitUntil(() => GetAvailableSpawnPoint() != null);

            SpawnPoint spawnPoint = GetAvailableSpawnPoint();
            AddEntity(entity, spawnPoint);

            yield return new WaitForSeconds(0.2f);
        }
    }

    public SpawnPoint GetAvailableSpawnPoint()
    {
        List<SpawnPoint> availablePoints = spawnPoints.Where(p => p.CanSpawn()).ToList();
        if (availablePoints == null || availablePoints.Count <= 0)
        {
            return null;
        }

        return availablePoints.Random();
        
    }

    public void AddEntity(Entity entity, SpawnPoint spawnPoint)
    {
        Entity spawnedEntity = spawnPoint.SpawnEntity(entity);
        spawnedEntities.Add(spawnedEntity);
        spawnedEntity.OnDeath += RemoveEntity;
    }

    public void RemoveEntity(Entity entity)
    {
        numberOfEnemiesDefeated++;
        spawnedEntities.Remove(entity);
        entity.OnDeath -= RemoveEntity;

        CheckForFinish();
    }

    public void SpawnAreaCompleted()
    {
        FinishWave();

        CheckForFinish();
    }

    public void CheckForFinish()
    {
        if (numberOfEnemiesDefeated >= wave.GetNumberOfEnemiesInWave())
            SpawnManager.Instance.FinishSpawnArea();
    }

    [Button]
    public void FindSpawnPoints()
    {
        spawnPoints = GetComponentsInChildren<SpawnPoint>().ToList();    
    }
}
