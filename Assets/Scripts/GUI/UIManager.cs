using System;
using GUI;
using Interfaces;
using StateMachine;
using UnityEngine;
using UnityEngine.UI;

namespace GUI
{
    public class UIManager : MonoBehaviour, IClickable
    {
        public static UIManager Instance { get; }

        private GameManager _gameManager;

        /// 所有定义在Canvas下的按钮 
        private Button[] _buttonArray;

        /// UI 总父节点
        private GameObject _uIManager;

        /// UI菜单节点
        private GameObject _mainMenuUI;

        private GameObject _defaultUI;
        private GameObject _characterUI;
        private GameObject _moveUI;
        private GameObject _menuAfterMoveUI;
        private GameObject _fightMenuUI;
        private GameObject _fightUI;


        private void Start()
        {
            _gameManager = GameManager.gameManager;
            CreateColliderForButton();
            _uIManager = GameObject.Find("UIManager");

            // menu
            _mainMenuUI = _uIManager.transform.Find("MainMenuUI").gameObject;
            _defaultUI = _uIManager.transform.Find("DefaultUI").gameObject;
            _characterUI = _uIManager.transform.Find("CharacterUI").gameObject;
            _moveUI = _uIManager.transform.Find("MoveUI").gameObject;
            _menuAfterMoveUI = _uIManager.transform.Find("MenuAfterMoveUI").gameObject;
            _fightMenuUI = _uIManager.transform.Find("FightMenuUI").gameObject;
            _fightUI = _uIManager.transform.Find("FightUI").gameObject;
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
                // default_state:
                case "MenuButton":
                    Debug.Log("MenuButton is clicked !");
                    MenuButton();
                    break;


                // main_menu_state:
                case "SaveButton":
                    // 存档功能待实现
                    break;
                case "BackButton":
                    BackButton();
                    break;
                case "QuitButton":
                    QuitButton();
                    break;

                // menuAfterMove:
                case "AttackButton":
                    AttackButton();
                    break;

                default:
                    Debug.Log("No button is clicked. Wrong operation!");
                    check = false;
                    break;
            }

            return check;
        }

        /// <summary>
        /// 给所有的按钮根绝按钮的大小来设置碰撞箱，使得射线可以检测到UI的Button
        /// </summary>
        private void CreateColliderForButton()
        {
            _buttonArray = GetComponentsInChildren<Button>(true); //获取所有的Button按钮
            foreach (Button button in _buttonArray)
            {
                if (button.gameObject.GetComponent<Collider>() == null) continue;

                //创建碰撞器
                Vector2 buttonSize = button.gameObject.GetComponent<RectTransform>().sizeDelta;
                BoxCollider buttonBoxCollider = button.gameObject.AddComponent<BoxCollider>();
                buttonBoxCollider.size = new Vector3(buttonSize.x, buttonSize.y, 2);
                buttonBoxCollider.center = new Vector3(0, 0, 1);
            }
        }


        private void MenuButton()
        {
            _gameManager.MenuButtonOnClick();
            _defaultUI.SetActive(false);
            _mainMenuUI.SetActive(true);
        }

        private void BackButton()
        {
            _gameManager.BackButtonOnClick();
            _mainMenuUI.SetActive(false);
            _defaultUI.SetActive(true);
        }

        public void ShowMenuAfterMove()
        {
            _menuAfterMoveUI.SetActive(true);
        }

        private void AttackButton()
        {
            _gameManager.AttackButtonOnClick();
            _menuAfterMoveUI.SetActive(false);
        }


        private static void QuitButton()
        {
            Application.Quit();
            Debug.Log("The game will be closed in the real game ");
        }
    }
}