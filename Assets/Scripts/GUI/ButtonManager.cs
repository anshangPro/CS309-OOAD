using System;
using System.Collections;
using System.Collections.Generic;
// using 
using UnityEngine;

namespace GUI
{
    /// <summary>
    /// 根据状态来展示对应的按钮
    /// </summary>
    public class ButtonManager : MonoBehaviour
    {
        private ButtonManager Instance { get; set; }

        private GameManager _gameManager;
        private GameObject _menuAttack;
        private GameObject _menuMove;
        private GameObject _menuStay;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        void Start()
        {
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            _menuAttack = GameObject.Find("MenuAttack");
            _menuMove = GameObject.Find("MenuMove");
            _menuStay = GameObject.Find("MenuStay");
        }

        /// <summary>
        /// 根据状态机的状态来决定按钮是否可见
        /// </summary>
        public void SetButtonVisible()
        {
            //根据当前的状态展示对应的按钮
            switch (_gameManager.status)
            {
                case GameStatus.Default:
                    _menuAttack.SetActive(false);
                    _menuMove.SetActive(false);
                    _menuStay.SetActive(false);
                    break;
                case GameStatus.Menu:
                    _menuAttack.SetActive(true);
                    _menuMove.SetActive(true);
                    _menuStay.SetActive(true);
                    break;
                case GameStatus.Fight:
                    _menuAttack.SetActive(false);
                    _menuMove.SetActive(false);
                    _menuStay.SetActive(false);
                    break;
                case GameStatus.Move:
                    _menuAttack.SetActive(false);
                    _menuMove.SetActive(false);
                    _menuStay.SetActive(false);
                    break;
                case GameStatus.Moving:
                    _menuAttack.SetActive(false);
                    _menuMove.SetActive(false);
                    _menuStay.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }
}