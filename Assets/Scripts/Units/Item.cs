using UnityEngine;

namespace Units
{
    [CreateAssetMenu(fileName = "New Item", menuName = "New Item")]
    public class Item : ScriptableObject
    {
        public string itemName; //物品名字
        public Sprite itemImage; //物品的照片
        public int itemNum = 0; //物品的数量，默认是一个，因为拾取第一个后直接为1，再拾取就直接+1即可

        [TextArea] //改变输入框格式，提示输入框容量
        public string itemInfo;

        public virtual bool ItemUse(Unit unit)
        {
            throw new System.NotImplementedException();
        }
    }
}