using Units;

namespace Interfaces
{
    public interface ITem
    {
        // /// <summary>
        // /// 道具剩余使用次数
        // /// </summary>
        // public int RemainQuantity { get; protected set; }
        //
        // /// <summary>
        // /// 道具总使用次数
        // /// </summary>
        // public int Quantity { get; protected set; }
        
        /// <summary>
        /// 对单位unit使用此道具
        /// </summary>
        /// <param name="unit"></param>
        /// <returns>返回道具是否使用成功</returns>
        public bool ItemUse(Unit unit);
    }
}