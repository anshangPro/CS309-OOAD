using System.Globalization;
using System.IO;
using Archive;
using GameData;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GUI.MainMenu
{
    public class LoadOption : MonoBehaviour
    {
        public string jsonPath;
        public TextMeshProUGUI text;
        public TextMeshProUGUI time;

        public void SetLoad(string path)
        {
            jsonPath = path;
            FileInfo f = new FileInfo(path);
            text.text = f.Name.Replace(f.Extension, "");
            time.text = f.LastWriteTime.ToString(CultureInfo.CurrentCulture);
        }

        public void Click()
        {
            GameDataManager.Instance.PanelShowing = false;
            GameDataManager.Instance.JsonToLoad = jsonPath;
            // SceneManager.LoadScene("SampleScene");
            LevelLoader.Instance.LoadNextScene();
        }
    }
}