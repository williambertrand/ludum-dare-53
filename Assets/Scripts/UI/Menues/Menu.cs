using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace OTBG.UI.Menues
{
    public class Menu : MonoBehaviour
    {
        [FoldoutGroup("Events")]
        public UnityEvent OnOpen;
        [FoldoutGroup("Events")]
        public UnityEvent OnClose;

        public string menuID;
        [HideInInspector]
        public MenuController menuController;
        /// <summary>
        /// This gets called when the menu gets opened.
        /// If you want to use the event in the inspector. Add base.Open to the overriden function
        /// </summary>
        public virtual void Open()
        {
            OnOpen?.Invoke();
        }

        /// <summary>
        /// This gets called when the menu gets closed.
        /// If you want to use the event in the inspector. Add base.Close to the overriden function
        /// </summary>
        public virtual void Close()
        {
            OnClose?.Invoke();
        }
    }
}

