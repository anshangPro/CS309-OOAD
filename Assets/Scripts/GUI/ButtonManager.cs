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
        private static ButtonManager Instance { get; set; }
        private GameManager _gameManager;

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
            _gameManager = GameObject.Find("GameObject").GetComponent<GameManager>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //根据当前的状态展示对应的按钮
            switch (_gameManager.status)
            {
                case GameStatus.Default:
                    break;
                case GameStatus.Menu:
                    break;
                case GameStatus.Fight:
                    break;
                case GameStatus.Move:
                    break;
                case GameStatus.Moving:
                    break;
                default:
                    break;
            }
        }
    }
}