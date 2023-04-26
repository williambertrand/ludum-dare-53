using System.Collections.Generic;
using UnityEngine;

namespace OTBG.Extensions
{
    public static class ObjectExtensions
    {
        public static T[] FindObjectsOfInterface<T>(this Object[] objects) where T : class
        {
            List<T> interfaceObjects = new List<T>();
            foreach (Object obj in objects)
            {
                if (obj is T)
                    interfaceObjects.Add(obj as T);
            }
            return interfaceObjects.ToArray();
        }

        public static T FindInterfaceInScene<T>(this Object[] objects) where T : class
        {
            foreach (Object obj in objects)
            {
                if (obj is T)
                    return obj as T;
            }
            return null;
        }

        public static T FindInterfaceInScene<T>() where T : class
        {
            return Object.FindObjectsOfType<Object>(true).FindInterfaceInScene<T>();
        }
    }
}
