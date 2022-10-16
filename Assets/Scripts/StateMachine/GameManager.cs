using System.Collections.Generic;
using UnityEngine;
using Unit = Units.Unit;

namespace StateMachine
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager gameManager { get; private set; }

        private void Awake()
        {
            if (gameManager != null && gameManager != this)
            {
                Destroy(gameObject);
            }
            else
            {
                gameManager = this;
            }
        }
    }
}