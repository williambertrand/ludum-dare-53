using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TutorialItem : MonoBehaviour
{

    public float travelDistance;
    public Transform popupPoint;
    public List<SpriteRenderer> images;
    
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        for (int i = 0; i < images.Count; i++)
        {
            SpriteRenderer image = images[i];
            transform.DOMove(image.transform.position + Vector3.up * travelDistance, 1.0f);
            image.DOFade(0, 1.0f).OnComplete(() => Destroy(this.gameObject));
        }
    }
}
