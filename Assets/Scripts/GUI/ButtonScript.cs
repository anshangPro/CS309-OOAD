using Interfaces;
using UnityEngine;

namespace GUI
{
    public class ButtonScript : MonoBehaviour, IClickable
    {
        private UIManager _uiManager;
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
                // default_state:
                case "MenuButton":
                    Debug.Log("MenuButton is clicked !");
                    _uiManager.MenuButton();
                    break;


                // main_menu_state:
                case "SaveButton":
                    // 存档功能待实现
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

                default:
                    Debug.Log("No button is clicked. Wrong operation!");
                    check = false;
                    break;
            }

            return check;
        }
    }
}