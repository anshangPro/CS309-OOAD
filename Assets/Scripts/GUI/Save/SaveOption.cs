using System.IO;
using Archive;
using GameData;
using Interfaces;
using StateMachine;
using TMPro;
using UnityEngine;
using Units;
using UnityEngine.Serialization;

namespace GUI.Save
{
    public class SaveOption: MonoBehaviour, IClickable
    {
        public TextMeshProUGUI saveName;
        private string _saveFileName;
        public TextMeshProUGUI time;
        public string Save;
        public bool isSave;
        public GameObject deletePrefab;

        public void SetSave(string s)
        {
            this.Save = s;
            FileInfo fi = new FileInfo(s);
            _saveFileName = fi.Name.Replace(fi.Extension, "");
            saveName.text = _saveFileName;
            time.text = fi.LastWriteTime.ToString();
            Debug.Log("isSave" + isSave.ToString());
            if (!isSave)
            {
                GameObject deletion = Instantiate(deletePrefab, transform);
                DeleteOption deleteOption = deletion.GetComponent<DeleteOption>();
                deleteOption.fileName = s;
                deleteOption.saveOption = this;
            }
        }

        public bool IsClicked()
        {
            GameDataManager data = GameDataManager.Instance;
            data.PanelShowing = false;
            if (isSave)
            {
                UIManager.Instance.DefaultUI.SetActive(true);
                MapSaver.Save($"{_saveFileName}.json");
                UIManager.Instance.LoadMenuUI.SetActive(false);
                return true;
            }
            else 
            {
                UIManager.Instance.LoadMenuUI.SetActive(false);

                ItemLoader.Instance.GoDefault(Save);
                return true;
            }
        }
    }
}