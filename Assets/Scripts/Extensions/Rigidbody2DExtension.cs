using System;
using System.Collections;
using UnityEngine;

namespace OTBG.Extensions
{
    public static class Rigidbody2DExtension
    {
        public static IEnumerator Knockback(this Rigidbody2D rb2D, int direction, float force,float height,float time, Action onComplete)
        {
            Vector2 knockbackDirection = new Vector2(direction * force, height);
            rb2D.velocity = Vector3.zero;
            rb2D.velocity = knockbackDirection;


            yield return new WaitForSeconds(0.5f);

            onComplete?.Invoke();
        }
    }
}