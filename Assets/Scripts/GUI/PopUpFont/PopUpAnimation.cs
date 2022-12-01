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

        private TextMeshProUGUI _tmp;
        private float _time;
        private Vector3 _origin;

        private void Awake()
        {
            _tmp = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _origin = transform.position;
        }

        void Start()
        {
        }

        // Update is called once per frame  
        void Update()
        {
            _tmp.color = new Color(_tmp.color.r, _tmp.color.g, _tmp.color.b, opacityCurve.Evaluate(_time));
            transform.localScale = Vector3.one * scaleCurve.Evaluate(_time);
            transform.position = _origin + new Vector3(0, 1 + heightCurve.Evaluate(_time), 0);
            _time += Time.deltaTime;
        }
    }
}