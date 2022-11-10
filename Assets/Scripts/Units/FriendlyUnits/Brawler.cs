using System;
using UnityEngine;

namespace Units.FriendlyUnits
{
    /// <summary>
    /// 拳击手
    /// </summary>
    public class Brawler : Friendly
    {
        public Brawler()
        {
            this.UnitName = "Brawler";
            this.level = 1;
            this.BaseHealth = 30;
            this.BaseMp = 12;
            this.BaseMv = 5;
            this.BaseDefense = 5;
            this.BaseDamage = 17;
            this.BaseAtkRange = 1;
            this.DefenseUpdateRate = 1.3f;
            UpdatePanel();
        }
    }
}