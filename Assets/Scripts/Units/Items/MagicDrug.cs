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

        public override bool ItemUse(Unit unit)
        {
            throw new System.NotImplementedException();
        }
    }
}