using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace extensions
{
    public static class VectorExtensions
    {

        /* Saves on calling a sqrt */
        public static float DistanceSquared(this Vector3 pos1, Vector3 pos2)
        {
            return (pos1.x - pos2.x) * (pos1.x - pos2.x) +
                (pos1.y - pos2.y) * (pos1.y - pos2.y) +
                (pos1.z - pos2.z) * (pos1.z - pos2.z);
        }

        /* Saves on calling a sqrt */
        public static float DistanceSquared(this Vector2 pos1, Vector2 pos2)
        {
            return (pos1.x - pos2.x) * (pos1.x - pos2.x) +
                (pos1.y - pos2.y) * (pos1.y - pos2.y);
        }
    }

}
