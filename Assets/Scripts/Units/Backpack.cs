using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    public class Backpack
    {
        /// <summary>
        /// 玩家背包，因为储存多个物品，所以是一个集合
        /// HashTable: name -> object
        /// </summary>
        public Hashtable ItemSet = new();
    }
}