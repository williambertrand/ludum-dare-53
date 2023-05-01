using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    private float TTL = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, TTL);
    }

}
