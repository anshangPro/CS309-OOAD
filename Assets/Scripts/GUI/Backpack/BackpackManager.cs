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
        public Units.Backpack backpack;


        private void FixedUpdate()
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
        /// 在UI中将一个物体的数据仓库显示出来
        /// </summary>
        /// <param name="item"></param>
        public void InsertItemToUI(Item item)
        {
            ItemUI grid = Instantiate(Instance.itemPrefab, Instance.backpackUI.transform);
            grid.itemImage.sprite = item.itemImage;
            grid.itemNum.text = item.itemNum.ToString();
        }

        /// <summary>
        /// 将背包数据仓库中所有物体显示在UI上
        /// </summary>
        private void UpdateItemToUI()
        {
            for (int i = 0; i < Instance.backpackUI.transform.childCount; i++)
            {
                Destroy(Instance.backpackUI.transform.GetChild(i).gameObject);
            }

            foreach (Item item in Instance.backpack.itemList)
            {
                InsertItemToUI(item);
            }
        }
    }
}