using OTBG.Extensions;

namespace OTBG.Utility
{
    public static class UtilityFuncs
    {
        public static T[] FindInterfacesInScene<T>(bool findNotActive = false) where T : class
        {
            return UnityEngine.Object.FindObjectsOfType<UnityEngine.Object>(findNotActive).FindObjectsOfInterface<T>();
        }

        public static T FindInterfaceInScene<T>(bool findNotActive = false) where T : class
        {
            return UnityEngine.Object.FindObjectsOfType<UnityEngine.Object>(findNotActive).FindInterfaceInScene<T>();
        }
    }
}