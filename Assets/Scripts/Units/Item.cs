using UnityEngine;

namespace Units
{
    public class Item
    {
        public string ItemName; //物品名字
        public Sprite ItemImage; //物品的照片
        public int ItemNum = 0; //物品的数量，默认是一个，因为拾取第一个后直接为1，再拾取就直接+1即可


        public virtual void ItemUse()
        {
        }
    }
}