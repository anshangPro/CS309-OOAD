using System;
using GUI;
using Interfaces;
using StateMachine;
using UnityEngine;
using UnityEngine.UI;

namespace GUI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

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

        private static readonly int AttackClicked = Animator.StringToHash("attackClicked");
        private static readonly int SkipAttackClicked = Animator.StringToHash("skipAttackClicked");

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(Instance);
            }
        }

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


        internal void MenuButton()
        {
            _defaultUI.SetActive(false);
            _mainMenuUI.SetActive(true);
        }

        internal void BackButton()
        {
            _mainMenuUI.SetActive(false);
            _defaultUI.SetActive(true);
        }

        internal void ShowMenuAfterMove()
        {
            _menuAfterMoveUI.SetActive(true);
        }

        internal void AttackButton()
        {
            // _gameManager.AttackButtonOnClick();
            Animator animator = GameManager.gameManager.GetComponent<Animator>();
            animator.SetTrigger(AttackClicked);
            _menuAfterMoveUI.SetActive(false);
            _fightMenuUI.SetActive(true);
        }


        internal void SkipAttackButton()
        {
            Animator animator = GameManager.gameManager.GetComponent<Animator>();
            animator.SetTrigger(SkipAttackClicked);
            _fightMenuUI.SetActive(false);
            
        }

        internal void QuitButton()
        {
            Debug.Log("The game will be closed in the real game ");
            Application.Quit();
        }
    }
}