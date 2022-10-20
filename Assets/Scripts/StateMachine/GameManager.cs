using UnityEngine;

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
                DontDestroyOnLoad(gameManager);
            }
        }
    }
}