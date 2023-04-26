using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst;
using UnityEngine;

namespace OTBG.Extensions
{
    public static class ListExtensions
    {
        public static System.Random random = new System.Random();

        [BurstCompile]
        public static T Random<T>(this IEnumerable<T> list)
        {
            return list.ToArray().Random();
        }
        [BurstCompile]
        public static T Random<T>(this T[] array)
        {
            return array[random.Next(0, array.Length)];
        }

        public static List<T> GetImmediateComponentsInChildren<T>(this Transform parent) where T : Component
        {
            List<T> immediateChildrenComponents = new List<T>();
            for (int i = 0; i < parent.childCount; i++)
            {
                Transform child = parent.GetChild(i);
                T component = child.GetComponent<T>();
                if (component != null)
                {
                    immediateChildrenComponents.Add(component);
                }
            }
            return immediateChildrenComponents;
        }
    }
}