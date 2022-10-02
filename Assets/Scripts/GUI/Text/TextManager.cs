using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace GUI
{
    public class TextManager : MonoBehaviour
    {
        private static TextManager _instance;

        public static TextManager Instance
        {
            get { return _instance; }
        }

        public GameManager gameManager;

        private TMP_Text _curState;

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

        // Start is called before the first frame update
        void Start()
        {
            gameManager = GameManager.gameManager;
            _curState = GameObject.Find("CurState").GetComponent<TMP_Text>();
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void UpdateButton()
        {
            switch (gameManager.status)
            {
                case GameStatus.Default:
                    _curState.text = "Default";
                    break;
                case GameStatus.Fight:
                    _curState.text = "Fight";
                    break;
                case GameStatus.Move:
                    _curState.text = "Move";
                    break;
                case GameStatus.FightMenu:
                    _curState.text = "FightMenu";
                    break;
                case GameStatus.MainMenu:
                    _curState.text = "MainMenu";
                    break;
                case GameStatus.MenuAfterMove:
                    _curState.text = "MenuAfterMove";
                    break;
                default:
                    break;
            }
        }
    }
}