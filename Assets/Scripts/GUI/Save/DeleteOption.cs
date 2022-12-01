using System.IO;
using UnityEngine;
using Interfaces;

namespace GUI.Save
{
    public class DeleteOption : MonoBehaviour, IClickable
    {
        public string fileName;
        public SaveOption saveOption;
        public bool IsClicked()
        {
            File.Delete(fileName);
            DestroyImmediate(saveOption.gameObject);
            return true;
        }
    }
}