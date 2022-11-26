using System;
using System.IO;
using GameData;
using StateMachine;
using Units;
using UnityEngine;

namespace GUI.Save
{
    public class SavePanelManager : MonoBehaviour
    {
        public GameObject content;
        public GameObject savePrefab;

        private void OnEnable()
        {
            GameDataManager gameData = GameDataManager.Instance;
            gameData.PanelShowing = true;
            string[] files = Directory.GetFiles(gameData.SavePath, "*.json");
            foreach (string save in files)
            {
                GameObject s = Instantiate(savePrefab, content.transform);
                SaveOption so = s.GetComponent<SaveOption>();
                so.SetSave(save);
            }
        }

        private void OnDisable()
        {
            UIManager.Instance.DefaultUI.SetActive(true);
            int len = content.transform.childCount;
            for (int i = 0; i < len; i++)
            {
                Destroy(content.transform.GetChild(i).gameObject);
            }
        }
    }
}