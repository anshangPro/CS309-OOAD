using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace GUI.Menu
{
    public class MenuStayButton : MonoBehaviour, IClickable
    {
        private static MenuStayButton Instance { get; set; }

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

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        bool IClickable.IsClicked()
        {
            return true;
        }
    }
}