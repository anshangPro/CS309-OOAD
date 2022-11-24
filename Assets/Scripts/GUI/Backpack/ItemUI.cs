using System;
using GameData;
using Interfaces;
using StateMachine;
using Units.Items;
using UnityEngine;
using UnityEngine.UI;

namespace GUI.Backpack
{
    public class ItemUI : MonoBehaviour, IClickable
    {
        public Image itemImage;
        public Text itemNum;
        private static readonly int ItemClicked = Animator.StringToHash("itemClicked");

        // ReSharper disable once IdentifierTypo
        private const string Templete = "IteamSpritesheet_";

        /// 根据点到的按钮来执行相应的操作, 这里只能根据按钮的sprite名字来区分
        public bool IsClicked()
        {
            string spriteName = gameObject.GetComponent<Image>().sprite.name;

            const string healthDrug = Templete + "201";

            const string magicDrug = Templete + "202";

            if (GameDataManager.Instance.gameStatus == GameStatus.MenuAfterMove)
            {
                switch (spriteName)
                {
                    case healthDrug:
                        HealthDrug.Instance.ItemUse();
                        break;

                    case magicDrug:
                        MagicDrug.Instance.ItemUse();
                        break;
                    default:
                        throw new NotImplementedException();
                }

                Animator animator = GameManager.gameManager.GetComponent<Animator>();
                animator.SetTrigger(ItemClicked);
            }

            UIManager.Instance.BackpackUI.SetActive(!UIManager.Instance.BackpackUI.activeSelf);

            // TODO: 这里需要设置一下使用道具的动画trigger by 张琦
            return true;
        }
    }
}