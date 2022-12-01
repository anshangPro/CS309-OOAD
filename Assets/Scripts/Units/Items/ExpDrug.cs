using GameData;
using GUI.Backpack;
using GUI.PopUpFont;
using UnityEngine;
using UnityEngine.UI;

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
            var itemNum = GameDataManager.Instance.GetCurrentPlayer().Backpack.ItemSet[Instance.ItemName].ItemNum;
            if (itemNum > 0)
            {
                Unit selectedUnit = GameDataManager.Instance.SelectedUnit;
                selectedUnit.level++;
                selectedUnit.UpdatePanel();
                selectedUnit.Exp = 0;

                PopUpFontManager.Instance.CreatePopUp(selectedUnit.GetComponent<Transform>(),
                    "Level Up!", Color.yellow);


                GameDataManager.Instance.GetCurrentPlayer().Backpack.ItemSet[Instance.ItemName].ItemNum--;
                BackpackManager.UpdateItemToUI();
            }
        }
    }
}