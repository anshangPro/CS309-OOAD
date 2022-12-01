using GameData;
using Units;
using UnityEngine;

namespace GUI.Arrow
{
    public class ArrowManager : MonoBehaviour
    {
        public static ArrowManager Instance { get; set; }
        public GameObject arrowPrefab;
        public static GameDataManager GameDataManager;
        private Unit _selectedUnit = null;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
            }

            Instance = this;
            GameDataManager = GameDataManager.Instance;
        }

        public void ShowArrow(Unit chosenUnit)
        {
            if (chosenUnit is not null)
            {
                GameObject selectedUnit = chosenUnit.gameObject;
                GameObject arrow = Instantiate(arrowPrefab, selectedUnit.transform);
                Destroy(arrow, 1f);
            }
        }
    }
}