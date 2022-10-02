using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GUI
{
    public class TextManager : MonoBehaviour
    {
        private static TextManager _instance;

        public static TextManager Instance
        {
            get { return _instance; }
        }

        public GameManager gameManager;
        private GameObject _curState;

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
            gameManager = GameManager.gameManager;
            _curState = GameObject.Find("CurState");
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}