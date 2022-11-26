using System.IO;
using Archive;
using GameData;
using Interfaces;
using StateMachine;
using TMPro;
using UnityEngine;
using Units;

namespace GUI.Save
{
    public class SaveOption: MonoBehaviour, IClickable
    {
        public TextMeshProUGUI saveName;
        public TextMeshProUGUI time;
        public string Save;

        public void SetSave(string s)
        {
            this.Save = s;
            FileInfo fi = new FileInfo(s);
            saveName.text = fi.Name.Replace(fi.Extension, "");
            time.text = fi.LastWriteTime.ToString();
        }

        public bool IsClicked()
        {
            GameDataManager data = GameDataManager.Instance;
            data.PanelShowing = false;
            UIManager.Instance.LoadMenuUI.SetActive(false);
            
            ItemLoader.Instance.GoDefault();
            data.JsonToLoad = Save;
            ItemLoader.Instance.Start();
            return true;
        }
    }
}