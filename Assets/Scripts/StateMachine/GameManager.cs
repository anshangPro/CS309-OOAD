using Unity.VisualScripting;
using UnityEngine;

namespace StateMachine
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager gameManager { get; private set; }

        private void Awake()
        {
            if (gameManager != null && gameManager != this && !gameManager.gameObject.IsDestroyed())
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