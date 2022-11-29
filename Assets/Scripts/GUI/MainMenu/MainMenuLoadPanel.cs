using System;
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
        public static string JsonToLoadDir; // 是new game时，json的文件夹还是load时的json文件夹
        public const string JsonDirNewGame = "./game-scene/";
        public const string JsonDirLoadGame = "./save/";

        private void OnEnable()
        {
            string[] files = Directory.GetFiles(JsonToLoadDir, "*.json");
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