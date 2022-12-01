using System;
using System.Collections.Generic;
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
        public GameObject addSavePrefab;
        private List<SaveOption> _saves = new();

        private void OnEnable()
        {
            GameDataManager gameData = GameDataManager.Instance;
            _saves.Clear();
            gameData.PanelShowing = true;
            string[] files = Directory.GetFiles(gameData.SavePath, "*.json");
            foreach (string save in files)
            {
                GameObject s = Instantiate(savePrefab, content.transform);
                SaveOption so = s.GetComponent<SaveOption>();
                _saves.Add(so);
                so.SetSave(save);
            }
            foreach (SaveOption save in _saves)
            {
                save.isSave = gameData.isSave;
            }
            if (gameData.isSave)
            {
                Instantiate(addSavePrefab, content.transform);
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