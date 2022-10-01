using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace GUI.Menu
{
    public class MenuStayButton : MonoBehaviour, IClickable
    {
        private static MenuStayButton _instance;

        public static MenuStayButton Instance
        {
            get { return _instance; }
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
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