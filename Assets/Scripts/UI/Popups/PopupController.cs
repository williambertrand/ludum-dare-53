using OTBG.Extensions;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace OTBG.UI.Popups
{

    public class PopupController : MonoBehaviour
    {
        public List<Popup> allPopups = new List<Popup>();
        [FoldoutGroup("Info")]
        [SerializeField]
        private bool _closeOthers = false;

        private void Awake()
        {
            allPopups = transform.GetImmediateComponentsInChildren<Popup>().ToList();
            allPopups.ForEach(p =>
            { 
                p.popupController = this;
                p.gameObject.SetActive(false);
            });
        }

        public void OpenPopup(Popup popup)
        {
            ToggleWindow(popup.popupID);
        }

        public void OpenPopup(string id)
        {
            ToggleWindow(id);
        }

        public void ClosePopup(Popup popup)
        {
            popup.Close();
            popup.gameObject.SetActive(false);
        }

        public void ClosePopup(string id)
        {
            Popup popup = allPopups.FirstOrDefault(p => p.popupID == id);
            popup.Close();
            popup.gameObject.SetActive(false);
        }

        private void ToggleWindow(string id)
        {
            if (_closeOthers)
                allPopups.ForEach(p =>p.gameObject.SetActive(false));

            Popup popup = allPopups.FirstOrDefault(p => p.popupID == id);

            if (popup == null)
            {
                Debug.LogError($"Popup ID: {id} not found. Make sure to inherit a popup from the class Popup and apply an ID to it.");
                return;
            }

            popup.gameObject.SetActive(true);
            popup.Open();
        }

        [Button]
        private void FindPopups()
        {
            allPopups = transform.GetImmediateComponentsInChildren<Popup>().ToList();
            allPopups.ForEach(p =>
            {
                p.popupController = this;
            });
        }

        [Button]
        public void CloseAllPopups()
        {
            allPopups.ForEach(p =>
            {
                p.Close();
                p.gameObject.SetActive(false);
            });
        }
    }
}

