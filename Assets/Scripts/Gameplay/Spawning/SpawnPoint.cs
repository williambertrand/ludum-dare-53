using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public LayerMask checkLayers;
    public float detectionRange;
    public Entity SpawnEntity(Entity entity)
    {
        Entity spawnedEnemy = CreateEntity(entity);
        return spawnedEnemy;
    }

    public Entity CreateEntity(Entity entityToSpawn)
    {
        return Instantiate(entityToSpawn, transform.position, Quaternion.identity);
    }

    public bool CanSpawn()
    {
        return Physics2D.OverlapCircle(transform.position, detectionRange, checkLayers) == null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
