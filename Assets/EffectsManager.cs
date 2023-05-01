using System.Collections;
using System.Collections.Generic;
using OTBG.Utility;
using Unity.Mathematics;
using UnityEngine;

public class EffectsManager : MonoSingleton<EffectsManager>
{

    public GameObject hitEffect;
    

    public void SpawnHitEffect(Vector3 position)
    {
        Vector3 randScale = new Vector3(
            UnityEngine.Random.Range(0.2f, 1),
            UnityEngine.Random.Range(0.2f, 1),
            1
        );
        GameObject obj = Instantiate(hitEffect, position, quaternion.identity);
        obj.transform.localScale = randScale;
    }
}
