using GameData;
using GUI.Backpack;
using GUI.PopUpFont;
using UnityEngine;

namespace Units.Items
{
    // 给当前行动的单位 +10 血
    public class HealthDrug : Item
    {
        public static HealthDrug Instance { get; } = new();

        public HealthDrug()
        {
            ItemName = "HealthDrug";
            var sprites = Resources.LoadAll<Sprite>("Sprites/IteamSpritesheet");
            ItemImage = sprites[201];
        }

        public override void ItemUse()
        {
            Unit selectedUnit = GameDataManager.Instance.SelectedUnit;
            float currentHealth = selectedUnit.Health;

            selectedUnit.Health = (currentHealth + 10 <= selectedUnit.MaxHealth)
                ? selectedUnit.Health + 10
                : selectedUnit.MaxHealth;
            
            PopUpFontManager.Instance.CreatePopUp(selectedUnit.GetComponent<Transform>(),
                "Hp +10 !", Color.red);
            
            GameDataManager.Instance.GetCurrentPlayer().Backpack.ItemSet[Instance.ItemName].ItemNum--;

            BackpackManager.UpdateItemToUI();
        }
    }
}