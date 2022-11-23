using System;
using System.Linq;
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

        
        private readonly Units.Backpack _backpack = new();


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
            
            for (int i = 0; i < 2; i++)
            {
                InsertItem(new HealthDrug());
                InsertItem(new MagicDrug());
            }
        }

        /// <summary>
        /// 将道具插入背包中 并更新在UI中将一个物体的数据仓库显示出来
        /// </summary>
        /// <param name="item"></param>
        public void InsertItem(Item item)
        {
            if (_backpack.ItemSet.ContainsKey(item.ItemName))
            {
                foreach (Item a in _backpack.ItemSet.Values)
                {
                    a.ItemNum++;
                }
            }
            else
            {
                _backpack.ItemSet.Add(item.ItemName, item);
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
        private static void UpdateItemToUI()
        {
            for (int i = 0; i < Instance.backpackUI.transform.childCount; i++)
            {
                Destroy(Instance.backpackUI.transform.GetChild(i).gameObject);
            }

            foreach (Item item in Instance._backpack.ItemSet.Values)
            {
                InsertItemToUI(item);
            }
        }
    }
}