using System;
using GameData;
using Units;
using Units.Items;
using UnityEngine;

namespace GUI.Backpack
{
    /// <summary>
    /// Backpack的管理者，以MVC的模式来给UI进行更新 by 张琦
    /// </summary>
    public class BackpackManager : MonoBehaviour
    {
        public static BackpackManager Instance { get; set; }

        public GameObject backpackUI;
        public ItemUI itemPrefab;


        private void OnEnable()
        {
            UpdateItemToUI();
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
            }

            Instance = this;
        }

        private void Start()
        {
            //优先级调后，保证player已经实例化
            foreach (Player player in GameDataManager.Instance.Players)
            {
                Units.Backpack backpack = player.Backpack;
                if (backpack.ItemSet.Count == 0)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        {
                            InsertItem(new HealthDrug(), backpack);
                            InsertItem(new MagicDrug(), backpack);
                            InsertItem(new ExpDrug(), backpack);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 将道具插入背包中 并更新在UI中将一个物体的数据仓库显示出来
        /// 考虑增加别的插入道具的方法
        /// </summary>
        /// <param name="item"></param>
        public void InsertItem(Item item, Units.Backpack backpack)
        {
            if (backpack.ItemSet.ContainsKey(item.ItemName))
            {
                foreach (Item a in backpack.ItemSet.Values)
                {
                    a.ItemNum++;
                }
            }
            else
            {
                backpack.ItemSet.Add(item.ItemName, item);
                UpdateItemToUI();
            }
        }

        private static void InsertItemToUI(Item item)
        {
            ItemUI grid = Instantiate(Instance.itemPrefab, Instance.backpackUI.transform);
            grid.itemImage.sprite = item.ItemImage;
            grid.itemNum.text = item.ItemNum.ToString();
        }

        /// <summary>
        /// 将背包数据仓库中所有物体显示在UI上
        /// </summary>
        public static void UpdateItemToUI()
        {
            for (int i = 0; i < Instance.backpackUI.transform.childCount; i++)
            {
                Destroy(Instance.backpackUI.transform.GetChild(i).gameObject);
            }

            Units.Backpack backpack = GameDataManager.Instance.GetCurrentPlayer().Backpack;
            foreach (Item item in backpack.ItemSet.Values)
            {
                InsertItemToUI(item);
            }
        }
    }
}