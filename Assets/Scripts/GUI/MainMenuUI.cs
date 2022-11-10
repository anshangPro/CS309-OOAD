using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GUI
{
    public class MainMenuUI : MonoBehaviour
    {
        private GameObject mainMenu;
        private GameObject selectSceneMenu;

        private void Start()
        {
            mainMenu = GameObject.Find("ManiMenu");
            selectSceneMenu = GameObject.Find("SelectSceneMenu");
            
            mainMenu.SetActive(true);
            selectSceneMenu.SetActive(false);
        }

        public void PlayBtn()
        {
            mainMenu.SetActive(false);
            selectSceneMenu.SetActive(true);
        }

        public void ExitBtn()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void BackBtn()
        {
            selectSceneMenu.SetActive(false);
            mainMenu.SetActive(true);
        }

        public void LoadSuperFlatScene()
        {
            SceneManager.LoadScene("Scenes/SampleScene");
        }
    }
}