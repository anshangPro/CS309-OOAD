using GameData;
using GUI.Backpack;
using GUI.PopUpFont;
using UnityEngine;

namespace Units.Items
{
    public class MagicDrug : Item
    {
        public static MagicDrug Instance { get; } = new();

        public MagicDrug()
        {
            ItemName = "MagicDrug";
            var sprites = Resources.LoadAll<Sprite>("Sprites/IteamSpritesheet");
            ItemImage = sprites[202];
        }

        public override void ItemUse()
        {
            Unit selectedUnit = GameDataManager.Instance.SelectedUnit;
            // selectedUnit.Mp += 10;
            // float currentMp = selectedUnit.Mp;
            // selectedUnit.Mp = (currentMp + 10 <= selectedUnit.MaxMp)
            //     ? selectedUnit.Mp + 10
            //     : selectedUnit.MaxMp;
            
            foreach (Skill skill in selectedUnit.Skills)
            {
                skill.RemainSkillPoint += 1;
                skill.RemainSkillPoint = Mathf.Min(skill.SkillPoint, skill.RemainSkillPoint);
            }
            
            PopUpFontManager.Instance.CreatePopUp(selectedUnit.GetComponent<Transform>(),
                "SP recover!", Color.blue);

            GameDataManager.Instance.GetCurrentPlayer().Backpack.ItemSet[Instance.ItemName].ItemNum--;

            BackpackManager.UpdateItemToUI();
        }
    }
}