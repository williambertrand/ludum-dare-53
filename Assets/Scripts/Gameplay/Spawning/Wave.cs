using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Wave_", menuName = "Create Enemy Wave")]
public class Wave : ScriptableObject
{
    public List<EnemyGroup> enemyGroups = new List<EnemyGroup>();
    public float timeBetweenGroups;

    [Button]
    public void TestNumbers()
    {
        Debug.Log(enemyGroups.Sum(g => g.enemies.Count));
    }

    public int GetNumberOfEnemiesInWave() => enemyGroups.Sum(g => g.enemies.Count);
}
