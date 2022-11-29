using System;
using TMPro;
using UnityEngine;

namespace GUI.PopUpFont
{
    public class PopUpAnimation : MonoBehaviour
    {
        public AnimationCurve opacityCurve;
        public AnimationCurve scaleCurve;
        public AnimationCurve heightCurve;

        private TextMeshProUGUI tmp;
        private float time = 0;
        private Vector3 origin;

        private void Awake()
        {
            tmp = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            origin = transform.position;
        }

        void Start()
        {
        }

        // Update is called once per frame  
        void Update()
        {
            tmp.color = new Color(1, 1, 1, opacityCurve.Evaluate(time));
            transform.localScale = Vector3.one * scaleCurve.Evaluate(time);
            transform.position = origin + new Vector3(0, 1 + heightCurve.Evaluate(time), 0);
            time += Time.deltaTime;
        }
    }
}