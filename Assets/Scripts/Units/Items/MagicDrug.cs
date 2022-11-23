using GameData;
using UnityEngine;

namespace Units.Items
{
    public class MagicDrug : Item
    {
        public MagicDrug()
        {
            ItemName = "MagicDrug";
            var sprites = Resources.LoadAll<Sprite>("Sprites/IteamSpritesheet");
            ItemImage = sprites[202];
        }

        public static void ItemUse()
        {
            Unit selectedUnit = GameDataManager.Instance.SelectedUnit;
            selectedUnit.Mp += 10;
        }
    }
}