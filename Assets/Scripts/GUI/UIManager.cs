using GameData;
using StateMachine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GUI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }


        /// 所有定义在Canvas下的按钮 
        private Button[] _buttonArray;

        /// UI 总父节点
        private GameObject _uIManager;


        /// UI菜单节点
        public GameObject MainMenuUI { get; private set; }

        public GameObject DefaultUI { get; private set; }

        public GameObject CharacterUI { get; private set; }

        public GameObject MoveUI { get; private set; }

        public GameObject MenuAfterMoveUI { get; private set; }

        public GameObject FightMenuUI { get; private set; }

        public GameObject FightUI { get; private set; }

        public GameObject BackpackUI { get; private set; }

        public GameObject SkillPanel { get; private set; }
        
        public GameObject GameOverMenuUI { get; private set; }
        public TMP_Text WinnerPlayerIDText { get; set; }

        private static readonly int AttackClicked = Animator.StringToHash("attackClicked");
        private static readonly int SkipAttackClicked = Animator.StringToHash("skipAttackClicked");
        private static readonly int SkipMoveClicked = Animator.StringToHash("skipMoveClicked");

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
            CreateColliderForButton();
            _uIManager = GameObject.Find("UIManager");
            // menu
            MainMenuUI = _uIManager.transform.Find("MainMenuUI").gameObject;
            DefaultUI = _uIManager.transform.Find("DefaultUI").gameObject;
            CharacterUI = _uIManager.transform.Find("CharacterUI").gameObject;
            MoveUI = _uIManager.transform.Find("MoveUI").gameObject;
            MenuAfterMoveUI = _uIManager.transform.Find("MenuAfterMoveUI").gameObject;
            FightMenuUI = _uIManager.transform.Find("FightMenuUI").gameObject;
            FightUI = _uIManager.transform.Find("FightUI").gameObject;
            BackpackUI = _uIManager.transform.Find("BackpackUI").gameObject;
            
            SkillPanel = _uIManager.transform.Find("SkillPanel").gameObject;
            GameOverMenuUI = _uIManager.transform.Find("GameoverMenuUI").gameObject;
            WinnerPlayerIDText = GameOverMenuUI.transform.Find("WinnerPlayerID").gameObject.GetComponent<TMP_Text>();
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

        internal void BackpackButton()
        {
            BackpackUI.SetActive(!BackpackUI.activeSelf);
            if (GameDataManager.Instance.gameStatus == GameStatus.MenuAfterMove
                && BackpackUI.activeSelf == false)
            {
                MenuAfterMoveUI.SetActive(true);
            }
        }

        internal void ItemButton()
        {
            MenuAfterMoveUI.SetActive(false);
            BackpackUI.SetActive(true);
        }

        internal void MenuButton()
        {
            DefaultUI.SetActive(false);
            MainMenuUI.SetActive(true);
        }

        internal void BackButton()
        {
            MainMenuUI.SetActive(false);
            DefaultUI.SetActive(true);
        }

        internal void SkillButtion()
        {
            SkillPanel.SetActive(true);
            GameDataManager.Instance.SkillShowing = true;
            MenuAfterMoveUI.SetActive(false);
        }

        internal void AttackButton()
        {
            // _gameManager.AttackButtonOnClick();
            Animator animator = GameManager.gameManager.GetComponent<Animator>();
            animator.SetTrigger(AttackClicked);
            MenuAfterMoveUI.SetActive(false);
        }

        internal static void SkipMoveButton()
        {
            GameDataManager.Instance.SelectedBlock = GameDataManager.Instance.SelectedUnit.onBlock;
            Animator animator = GameManager.gameManager.GetComponent<Animator>();
            animator.SetTrigger(SkipMoveClicked);
        }

        internal static void SkipAttackButton()
        {
            Animator animator = GameManager.gameManager.GetComponent<Animator>();
            animator.SetTrigger(SkipAttackClicked);
        }

        internal static void QuitButton()
        {
            SceneManager.LoadScene("Scenes/MainMenu");
        }

        internal void ClosePanelButton()
        {
            SkillPanel.SetActive(false);
            DefaultUI.SetActive(true);
            MenuAfterMoveUI.SetActive(true);
        }
    }
}