using GUI;
using UnityEngine;

namespace GUI
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager _instance;

        public static UIManager Instance
        {
            get { return _instance; }
        }
        
        
    }
}