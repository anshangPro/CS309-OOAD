using System.Linq;
using Units;
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
        public Units.Backpack Backpack = new();


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

        /// <summary>
        /// 将道具插入背包中 并更新在UI中将一个物体的数据仓库显示出来
        /// </summary>
        /// <param name="item"></param>
        public void InsertItem(Item item)
        {
            if (Backpack.ItemSet.ContainsKey(item.ItemName))
            {
                foreach (Item a in Backpack.ItemSet.Values)
                {
                    a.ItemNum++;
                }
            }
            else
            {
                Backpack.ItemSet.Add(item.ItemName, item);
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

            foreach (Item item in Instance.Backpack.ItemSet.Values)
            {
                InsertItemToUI(item);
            }
        }
    }
}