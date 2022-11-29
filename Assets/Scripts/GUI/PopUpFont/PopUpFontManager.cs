using System;
using TMPro;
using UnityEngine;

namespace GUI.PopUpFont
{
    public class PopUpFontManager : MonoBehaviour
    {
        public static PopUpFontManager Instance { get; set; }

        public GameObject prefeb;

        // Start is called before the first frame update
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
            }

            Instance = this;
        }

        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void CreatePopUp(Transform position, string text, Color color)
        {
            GameObject popup = Instantiate(prefeb, position);
            Canvas popCanvas = popup.GetComponent<Canvas>();
            popCanvas.sortingLayerName = "Units";
            TextMeshProUGUI temp = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            temp.text = text;
            temp.color = color;
            Destroy(popup, 1f);
        }
    }
}