using System;
using Archive;
using GUI.Backpack;
using Interfaces;
using Units.Items;
using UnityEngine;

namespace GUI
{
    public class ButtonScript : MonoBehaviour, IClickable
    {
        private UIManager _uiManager;

        private void Awake()
        {
            _uiManager = UIManager.Instance;
        }

        private void Start()
        {
            _uiManager = UIManager.Instance;
        }


        /// <summary>
        /// 有按钮被点击到 
        /// </summary>
        public bool IsClicked()
        {
            bool check = true;
            Debug.Log("Hit object: " + MouseController.GameObjectName);
            switch (MouseController.GameObjectName)
            {
                case "MenuButton":
                    Debug.Log("MenuButton is clicked !");
                    _uiManager.MenuButton();
                    break;
                case "BackpackButton":
                    Debug.Log("BackpackButton is clicked !");
                    _uiManager.BackpackButton();
                    BackpackManager.Instance.InsertItem(new HealthDrug());
                    break;


                // main_menu_state:
                case "SaveButton":
                    // 存档功能待实现
                    MapSaver.Save();
                    break;
                case "BackButton":
                    _uiManager.BackButton();
                    break;
                case "QuitButton":
                    _uiManager.QuitButton();
                    break;

                // menuAfterMove:
                case "AttackButton":
                    _uiManager.AttackButton();
                    break;
                // fightMenu:
                case "SkipAttackButton":
                    _uiManager.SkipAttackButton();
                    break;
                case "SkillButton":
                    _uiManager.SkillButtion();
                    break;
                default:
                    Debug.Log("No button is clicked. Wrong operation!");
                    check = false;
                    break;
            }

            return check;
        }
    }
}