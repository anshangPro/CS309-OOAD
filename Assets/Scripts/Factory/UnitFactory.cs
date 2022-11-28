using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Factory
{
    public class UnitFactory : MonoBehaviour
    {
        public static UnitFactory Instance { get; private set; }

        public List<GameObject> unitPrefeb = new List<GameObject>();
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public Unit GetUnit(int type)
        {
            GameObject unitObj = Instantiate(unitPrefeb[type]);
            Unit unit = unitObj.GetComponent<Unit>();
            return unit;
        }

    }
}