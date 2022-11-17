using UnityEngine;

namespace Units.Items
{
    public class HealthDrug : Item
    {
        public HealthDrug()
        {
            ItemName = "HealthDrug";
            var sprites = Resources.LoadAll<Sprite>("Sprites/IteamSpritesheet");
            ItemImage = sprites[201];
        }   


        public override bool ItemUse(Unit unit)
        {
            throw new System.NotImplementedException();
        }
    }
}