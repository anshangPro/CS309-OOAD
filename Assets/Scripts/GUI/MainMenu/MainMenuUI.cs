using System.Collections.Generic;
using System.IO;
using GameData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GUI.MainMenu
{
    public class MainMenuUI : MonoBehaviour
    {
        public GameObject mainMenu;
        public GameObject selectSceneMenu;
        public GameObject selectModeMenu;

        public AudioClip buttonClicked;

        private Dictionary<string, string> _sceneToJson = new Dictionary<string, string>()
        {
            {"Scenes/SampleScene", "SampleScene.json"}
        };

        private void ButtonClicked()
        {
            Camera.main!.GetComponent<AudioSource>().PlayOneShot(buttonClicked);
        }

        private void Start()
        {
            mainMenu.SetActive(true);
            selectSceneMenu.SetActive(false);
            selectModeMenu.SetActive(false);
        }

        public void PlayBtn()
        {
            ButtonClicked();
            mainMenu.SetActive(false);
            selectSceneMenu.SetActive(false);
            selectModeMenu.SetActive(true);
        }

        public void ExitBtn()
        {
            ButtonClicked();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void BackBtn()
        {
            ButtonClicked();
            mainMenu.SetActive(true);
            selectSceneMenu.SetActive(false);
            selectModeMenu.SetActive(false);
        }

        public void PvpModeButton()
        {
            ButtonClicked();
            GameDataManager.Instance.Pve = false;
            EnterSelectSceneMenu();
        }

        public void PveModeButton()
        {
            ButtonClicked();
            GameDataManager.Instance.Pve = true;
            EnterSelectSceneMenu();
        }

        private void EnterSelectSceneMenu()
        {
            mainMenu.SetActive(false);
            selectSceneMenu.SetActive(true);
            selectModeMenu.SetActive(false);
        }

        public void LoadSuperFlatScene()
        {
            GameDataManager.Instance.JsonToLoad = Path.Combine("Save", _sceneToJson["Scenes/SampleScene"]);
            SceneManager.LoadScene("Scenes/SampleScene");
        }
    }
}