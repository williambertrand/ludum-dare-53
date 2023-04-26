using UnityEngine;
namespace OTBG.UI.Popups
{
    public class Popup : MonoBehaviour
    {
        public string popupID;

        [HideInInspector]
        public PopupController popupController;

        public virtual void Open()
        {

        }

        public virtual void Close()
        {

        }
    }
}

