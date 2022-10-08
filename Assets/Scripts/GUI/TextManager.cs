using StateMachine;
using TMPro;
using UnityEngine;

namespace GUI
{
    public class TextManager : MonoBehaviour
    {
        /// 显示当前状态的文本
        private GameObject _curState;

        private TMP_Text _tmp;

        private void Start()
        {
            // 显示状态的文本
            _curState = transform.Find("CurState").gameObject;
            _tmp = _curState.GetComponent<TMP_Text>();
        }

        private void FixedUpdate()
        {
            _tmp.text = GameManager.gameManager.Status switch
            {
                GameStatus.Default => "Default",
                GameStatus.Character => "Character",
                GameStatus.Move => "Move",
                GameStatus.MainMenu => "MainMenu",
                GameStatus.MenuAfterMove => "MenuAfterMove",
                GameStatus.FightMenu => "FightMenu",
                GameStatus.Fight => "Fight",
                _ => _tmp.text
            };
        }
    }
}