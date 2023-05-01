using UnityEngine;

public class DamageData
{
    public Transform damageDealer;
    public Transform target;
    public float damageDealt;
    public bool isCombo = false;

    /// <summary>
    /// This will return 1 if the target is right, and -1 for left of the damageDealer
    /// </summary>
    /// <returns></returns>
    public int GetDirectionToTarget()
    {
        Vector3 direction = damageDealer.position - damageDealer.position;
        return (int)Mathf.Sign(direction.x);
    }
}
