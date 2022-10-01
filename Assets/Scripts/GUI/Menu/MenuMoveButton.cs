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
        private static MenuMoveButton Instance { get; set; }

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