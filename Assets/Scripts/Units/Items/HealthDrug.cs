using GameData;
using UnityEngine;

namespace Units.Items
{
    // 给当前行动的单位 +10 血
    public class HealthDrug : Item
    {
        public HealthDrug()
        {
            ItemName = "HealthDrug";
            var sprites = Resources.LoadAll<Sprite>("Sprites/IteamSpritesheet");
            ItemImage = sprites[201];
        }


        public static void ItemUse()
        {
            Unit selectedUnit = GameDataManager.Instance.SelectedUnit;
            selectedUnit.Health += 10;
        }
    }
}