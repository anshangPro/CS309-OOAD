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
        private static UIManager _uiManager = UIManager.Instance;

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
                // default UI
                case "MenuButton":
                    Debug.Log("MenuButton is clicked !");
                    _uiManager.MenuButton();
                    break;
                case "BackpackButton":
                    Debug.Log("BackpackButton is clicked !");
                    _uiManager.BackpackButton();
                    // BackpackManager.Instance.InsertItem(new HealthDrug());
                    break;


                // main_menu:
                case "SaveButton":
                    // 存档功能待实现
                    MapSaver.Save();
                    break;
                case "BackButton":
                    _uiManager.BackButton();
                    break;
                case "QuitButton":
                    UIManager.QuitButton();
                    break;

                // unit:
                case "SkipMoveButton":
                    UIManager.SkipMoveButton();
                    break;

                
                // menuAfterMove:
                case "AttackButton":
                    _uiManager.AttackButton();
                    break;


                // fightMenu:
                case "SkipAttackButton":
                    UIManager.SkipAttackButton();
                    break;
                case "SkillButton":
                    _uiManager.SkillButtion();
                    break;
                case "ItemButton":
                    _uiManager.ItemButton();
                    break;

                // skillPanel
                case "closePanelButton":
                    _uiManager.ClosePanelButton();
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