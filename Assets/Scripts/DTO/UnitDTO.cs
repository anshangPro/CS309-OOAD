using System.Collections.Generic;
using Units;
using UnityEngine;

namespace DTO
{
    public class UnitDTO
    {
        public int type;
        public int[] coornidate;
        public int ofPlayer;
        public string UnitName;
        public float MaxHealth;
        public float Health;
        public float MaxMp;
        public float Mp;
        public float Damage;
        public float Defense;
        public int level;
        public int AtkRange;
        public Dictionary<string, string> optional;

        public Vector3Int GetCoordinate()
        {
            return new Vector3Int(coornidate[0], coornidate[1], coornidate[2]);
        }
        
        public UnitDTO(){}

        public UnitDTO(Unit unit)
        {
            type = unit.type;
            coornidate = new int[2];
            coornidate[0] = unit.onBlock.X;
            coornidate[1] = unit.onBlock.Z;
            UnitName = unit.UnitName;
            MaxHealth = unit.MaxHealth;
            Health = unit.Health;
            MaxMp = unit.MaxMp;
            Mp = unit.Mp;
            Damage = unit.Damage;
            Defense = unit.Defense;
            AtkRange = unit.AtkRange;
            optional = new Dictionary<string, string>();
        }
    }
}