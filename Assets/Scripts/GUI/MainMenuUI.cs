using System;
using GameData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GUI
{
    public class MainMenuUI : MonoBehaviour
    {
        private GameObject _mainMenu;
        private GameObject _selectSceneMenu;
        private GameObject _selectModeMenu;
        private bool[] scenceLoaded;

        private void Start()
        {
            _mainMenu = GameObject.Find("MainMenu");
            _selectSceneMenu = GameObject.Find("SelectSceneMenu");
            _selectModeMenu = GameObject.Find("SelectModeMenu");
            scenceLoaded = new bool[SceneManager.sceneCount];

            _mainMenu.SetActive(true);
            _selectSceneMenu.SetActive(false);
            _selectModeMenu.SetActive(false);
        }

        public void PlayBtn()
        {
            _mainMenu.SetActive(false);
            _selectSceneMenu.SetActive(false);
            _selectModeMenu.SetActive(true);
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
            _mainMenu.SetActive(true);
            _selectSceneMenu.SetActive(false);
            _selectModeMenu.SetActive(false);
        }

        public void PvpModeButton()
        {
            GameDataManager.Instance.Pve = false;
            EnterSelectSceneMenu();
        }

        public void PveModeButton()
        {
            GameDataManager.Instance.Pve = true;
            EnterSelectSceneMenu();
        }

        private void EnterSelectSceneMenu()
        {
            _mainMenu.SetActive(false);
            _selectSceneMenu.SetActive(true);
            _selectModeMenu.SetActive(false);
        }

        public void LoadSuperFlatScene()
        {
            SceneManager.LoadScene("Scenes/SampleScene");
        }
    }
}