using System;
using Archive;
using GameData;
using GUI.Backpack;
using Interfaces;
using Units.Items;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                    break;
                
                // Backpack UI
                case "closeBackpackButton":
                    _uiManager.BackpackButton();
                    break;
                
                // main_menu:
                case "SaveButton":
                    _uiManager.SaveButton();
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
                case "LoadButton":
                    _uiManager.LoadButton();
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

                case "NextLevelButton":
                    // TODO: 需要实现下一关卡的切换
                    break;

                case "ExitGameButton":
                    SceneManager.LoadScene("Scenes/MainMenu");
                    break;
                
                case "WithdrawMoveButton":
                    GameDataManager.Instance.WithdrawMove();
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