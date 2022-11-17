using System.Collections.Generic;
using UnityEngine;

namespace DTO
{
    public class UniDto
    {
        public string type;
        public int[] coornidate;
        public int ofPlayer;
        public string UnitName;
        public int MaxHealth;
        public int Health;
        public int MaxMp;
        public int Mp;
        public int Damage;
        public int level;
        public int AtkRange;
        public Dictionary<string, string> optional;

        public Vector3Int GetCoordinate()
        {
            return new Vector3Int(coornidate[0], coornidate[1], coornidate[2]);
        }
    }
}