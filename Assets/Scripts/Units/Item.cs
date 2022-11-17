using System;
using Interfaces;

namespace Units
{
    /// <summary>
    /// TODO: 因为C#不支持多继承，为了特定的物品既实现item又实现ScriptableObject,将item改写为接口 by 张琦
    /// </summary>
    [Obsolete("改为使用ITem接口的实现方式", true)]
    public abstract class Item
    {
    }
}