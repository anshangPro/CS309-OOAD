using Interfaces;
using UnityEngine;

namespace Units.Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item/New HealthDrug")]
    public class HealthDrug : ScriptableObject, ITem
    {
        public string itemName; //物品名字
        public Sprite itemSprite; //物品的照片
        public int itemHeld = 1; //物品的数量，默认是一个，因为拾取第一个后直接为1，再拾取就直接+1即可

        [TextArea] //使text可以富文本进行多行书写
        public string itemInfo; //物品的介绍描述

        public bool ItemUse(Unit unit)
        {
            throw new System.NotImplementedException();
        }
    }
}