using Units.Items;
using UnityEngine;
using UnityEngine.Serialization;

namespace GUI.Backpack
{
    /// <summary>
    /// Backpack的管理者，以MVC的模式来给UI进行更新 by 张琦
    /// </summary>
    public class BackpackManager : MonoBehaviour
    {
        public static BackpackManager Instance { get; private set; }

        public GameObject backpackUI;

        public Units.Backpack backpack;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
            }

            Instance = this;
        }

        private void Start()
        {
        }
    }
}