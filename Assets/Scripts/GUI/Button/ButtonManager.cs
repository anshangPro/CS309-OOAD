using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUI
{
    /// <summary>
    /// 根据状态来展示对应的按钮
    /// </summary>
    public class ButtonManager : MonoBehaviour
    {
        private static ButtonManager _instance;

        public static ButtonManager Instance
        {
            get { return _instance; }
        }

        public GameManager gameManager;
        private GameObject _menuAttack;
        private GameObject _menuMove;
        private GameObject _menuStay;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        void Start()
        {
            gameManager = GameManager.gameManager;
            _menuAttack = GameObject.Find("MenuAttack");
            _menuMove = GameObject.Find("MenuMove");
            _menuStay = GameObject.Find("MenuStay");
        }

        /// <summary>
        /// 根据当前的状态展示对应的按钮
        /// </summary>
        public void UpdateButton()
        {
             switch (gameManager.status)
            {
                case GameStatus.Default:
                
                    break;
                case GameStatus.Fight:
                    break;
                case GameStatus.Move:
                    break;
                case GameStatus.FightMenu:
                    break;
                case GameStatus.MainMenu:
                    break;
                case GameStatus.MenuAfterMove:
                    break;
                default:
                    break;
            }
        }
    }
}