using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Interfaces;
using UnityEngine;

namespace GUI.Menu
{
    public class MenuMoveButton : MonoBehaviour, IClickable
    {
        private static MenuMoveButton _instance;

        public static MenuMoveButton Instance
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

        private void FixedUpdate()
        {
            throw new NotImplementedException();
        }

        bool IClickable.IsClicked()
        {
            return true;
        }
    }
}