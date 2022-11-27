using System.Collections.Generic;
using System.IO;
using GameData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GUI.MainMenu
{
    public class MainMenuUI : MonoBehaviour
    {
        public GameObject _mainMenu;
        public GameObject _selectSceneMenu;
        public GameObject _selectModeMenu;

        private Dictionary<string, string> _sceneToJson = new Dictionary<string, string>()
        {
            {"Scenes/SampleScene", "SampleScene.json"}
        };
        private void Start()
        {
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
            GameDataManager.Instance.JsonToLoad = Path.Combine("Save", _sceneToJson["Scenes/SampleScene"]);
            SceneManager.LoadScene("Scenes/SampleScene");
        }
    }
}