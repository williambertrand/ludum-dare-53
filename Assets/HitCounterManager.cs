using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCounterManager : MonoBehaviour
{
    public HitCounter counter;

    public float spawnHeight;
    public float travelDistance;

    private void OnEnable()
    {
        Entity.OnDamaged += Entity_OnDamaged;
    }   

    private void OnDisable()
    {
        Entity.OnDamaged -= Entity_OnDamaged;
    }

    private void Entity_OnDamaged(Entity obj, DamageData damageData)
    {
        HitCounter counter = CreateCounter(obj.transform.position + Vector3.up * spawnHeight);
        counter.Initialise(damageData.damageDealt, travelDistance);
    }

    public HitCounter CreateCounter(Vector3 position)
    {
        return Instantiate(counter, position, Quaternion.identity);
    }
}
