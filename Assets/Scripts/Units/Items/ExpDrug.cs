using GameData;
using GUI.Backpack;
using UnityEngine;

namespace Units.Items
{
    public class ExpDrug : Item
    {
        public static ExpDrug Instance { get; } = new();

        public ExpDrug()
        {
            ItemName = "ExpDrug";
            var sprites = Resources.LoadAll<Sprite>("Sprites/IteamSpritesheet");
            ItemImage = sprites[28];
        }

        public override void ItemUse()
        {
            Unit selectedUnit = GameDataManager.Instance.SelectedUnit;
            selectedUnit.level++;
            selectedUnit.UpdatePanel();
            selectedUnit.Exp = 0;

            GameDataManager.Instance.GetCurrentPlayer().Backpack.ItemSet[Instance.ItemName].ItemNum--;
            BackpackManager.UpdateItemToUI();
        }
    }
}