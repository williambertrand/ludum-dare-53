using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OTBG.UI.Menues
{
    public class MenuController : MonoBehaviour
    {
        public List<Menu> allMenues = new List<Menu>();
        public Menu mainMenu;
        
        private Menu _currentMenu;

        private void Awake()
        {
            allMenues = GetComponentsInChildren<Menu>(true).ToList();
            allMenues.ForEach(m => m.menuController = this);
        }

        private void Start()
        {
            OpenMenu(mainMenu);
        }

        public void OpenMenu(Menu menu)
        {
            OpenMenu(menu.menuID);
        }

        public void OpenMenu(string id)
        {
            ToggleWindow(id);
        }

        private void ToggleWindow(string id)
        {
            Menu menu = allMenues.FirstOrDefault(m => m.menuID == id);
            if(menu == null)
            {
                Debug.LogError($"Menu ID: {id} not found. Make sure to inherit a menu from the class Menu and apply an ID to it.");
                return;
            }

            if (_currentMenu != menu && _currentMenu != null)
                _currentMenu.Close();

            allMenues.ForEach(m => m.gameObject.SetActive(false));

            menu.gameObject.SetActive(true);
            menu.Open();
            
            _currentMenu = menu;
        }

        [Button]
        public void FindMenues()
        {
            allMenues = GetComponentsInChildren<Menu>(true).ToList();
            allMenues.ForEach(m => m.menuController = this);
        }

        [Button]
        public void GoToDefault()
        {
            allMenues = GetComponentsInChildren<Menu>(true).ToList();
            OpenMenu(mainMenu);
        }
    }
}

