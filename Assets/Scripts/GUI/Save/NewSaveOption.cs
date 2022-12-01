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
    public class NewSaveOption : MonoBehaviour, IClickable
    {
        public bool IsClicked()
        {
            GameDataManager data = GameDataManager.Instance;
            data.PanelShowing = false;
            UIManager.Instance.DefaultUI.SetActive(true);
            UIManager.Instance.LoadMenuUI.SetActive(false);
            MapSaver.Save();
            return true;
        }
    }
}