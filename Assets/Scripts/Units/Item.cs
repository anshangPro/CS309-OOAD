using Interfaces;

namespace Units
{
    public abstract class Item
    {
        /// <summary>
        /// 道具剩余使用次数
        /// </summary>
        public abstract int RemainQuantity { get; protected set; }
        /// <summary>
        /// 道具总使用次数
        /// </summary>
        public abstract int Quantity { get; protected set; }

        public string Name;
        
        /// <summary>
        /// 对单位unit使用此道具
        /// </summary>
        /// <param name="unit"></param>
        /// <returns>返回道具是否使用成功</returns>
        public abstract bool itemUse(Unit unit);
    }
    
}