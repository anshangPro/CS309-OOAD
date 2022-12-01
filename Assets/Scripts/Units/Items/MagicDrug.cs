using GameData;
using GUI.Backpack;
using GUI.PopUpFont;
using UnityEngine;
using UnityEngine.UI;

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
            var itemNum = GameDataManager.Instance.GetCurrentPlayer().Backpack.ItemSet[Instance.ItemName].ItemNum;
            if (itemNum > 0)
            {
                Unit selectedUnit = GameDataManager.Instance.SelectedUnit;

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
}