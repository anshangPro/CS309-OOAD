using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace GUI.Menu
{
    public class MenuAfterMoveAttack : MonoBehaviour, IClickable
    {
        private static MenuAfterMoveAttack _instance;

        public static MenuAfterMoveAttack Instance
        {
            get { return _instance; }
        }

        private Button _attackButton;

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

        // Update is called once per frame
        void Update()
        {
        }

        bool IClickable.IsClicked()
        {
            // 触发后将状态转换到 fightMenu
            switch (gameObject.name)
            {
                case "MenuAttack" :
                    GameManager.gameManager.AttackButtonOnClick();
                    break;
                case "MenuStay":
                    break;
            }
            // () => { return GameManager.gameManager};
            return true;
        }
    }
}