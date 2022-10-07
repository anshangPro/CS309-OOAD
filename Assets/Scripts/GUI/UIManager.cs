using System;
using GUI;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace GUI
{
    public class UIManager : MonoBehaviour, IClickable
    {
        private static UIManager _instance;

        public static UIManager Instance
        {
            get { return _instance; }
        }

        /// default 状态下的按钮
        private Button[] _buttonArray; //所有定义在Canvas下的按钮 


        private void Start()
        {
            _buttonArray = GetComponentsInChildren<Button>(true); //获取所有的Button按钮
            CreateColliderForButton();
        }

        /// <summary>
        /// 有按钮被点击到 
        /// </summary>
        public bool IsClicked()
        {
            bool check = true;
            switch (MouseController.GameObjectName)
            {
                // default_state:
                case "MenuButton":

                    break;
                case "SaveButton":
                    break;
                case "BackButton":
                    break;
                case "QuitButton":
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
            foreach (var button in _buttonArray)
            {
                if (button.gameObject.GetComponent<Collider>() == null)
                {
                    //创建碰撞器
                    var buttonSize = button.gameObject.GetComponent<RectTransform>().sizeDelta;
                    var buttonBoxCollider = button.gameObject.AddComponent<BoxCollider>();
                    buttonBoxCollider.size = new Vector3(buttonSize.x, buttonSize.y, 2);
                    buttonBoxCollider.center = new Vector3(0, 0, 1);
                }
            }
        }


        public void Update()
        {
            throw new NotImplementedException();
        }

        public void ExitMainMenu()
        {
            Application.Quit();
            Debug.Log("Game closed");
        }
    }
}