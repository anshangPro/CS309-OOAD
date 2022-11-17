using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(fileName = "New Backpack", menuName = "New Backpack")]
    public class Backpack : ScriptableObject
    {
        /// <summary>
        /// 玩家背包，因为储存多个物品，所以是一个集合
        /// </summary>
        public List<Item> itemList = new();
    }
}