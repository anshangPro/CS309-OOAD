// using System;
// using Interfaces;
// using UnityEngine;
//
// namespace Units.Items
// {
//     public class FirstAidKit : ScriptableObject, ITem
//     {
//         
//         // {
//         //     public override int Quantity { get; protected set; }
//         //     public override int RemainQuantity { get; protected set; }
//         //     public int HealingAmount;
//         //
//         //     public override bool itemUse(Unit unit)
//         //     {
//         //         if (RemainQuantity <= 0) return false;
//         //         RemainQuantity--;
//         //         unit.Health += HealingAmount;
//         //         unit.Health = Math.Min(unit.Health, unit.MaxHealth);
//         //         return true;
//         //     }
//         // int ITem.RemainQuantity { get; set; }
//         //
//         // int ITem.Quantity { get; set; }
//
//         // public bool itemUse(Unit unit)
//         // {
//         //     if (((ITem)this).RemainQuantity <= 0) return false;
//         //     RemainQuantity--;
//         //     unit.Health += HealingAmount;
//         //     unit.Health = Math.Min(unit.Health, unit.MaxHealth);
//         //     return true;
//         // }
//         public bool ItemUse(Unit unit)
//         {
//             throw new NotImplementedException();
//         }
//     }
// }

