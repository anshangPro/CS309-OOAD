using System.IO;
using GameData;
using GUI.Save;
using UnityEngine;

namespace GUI.MainMenu
{
    public class MainMenuLoadPanel : MonoBehaviour
    {
        public GameObject content;
        public GameObject loadPrefab;

        private void OnEnable()
        {
            string[] files = Directory.GetFiles(GameDataManager.Instance.SavePath, "*.json");
            foreach (string fileToLoad in files)
            {
                GameObject optionGameObject = Instantiate(loadPrefab, content.transform);
                optionGameObject.GetComponent<LoadOption>().SetLoad(fileToLoad);
            }
        }

        private void OnDisable()
        {
            foreach (Transform child in content.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}