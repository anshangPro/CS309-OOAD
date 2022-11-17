using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace GUI.Backpack
{
    public class ItemUI : MonoBehaviour, IClickable
    {
        public Image itemImage;
        public Text itemNum;
        public bool IsClicked()
        {
            throw new System.NotImplementedException();
        }
    }
}