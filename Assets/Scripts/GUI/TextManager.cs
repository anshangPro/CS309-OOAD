using System;
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

        // Start is called before the first frame update
        private void Start()
        {
            // 显示状态的文本
            _curState = gameObject;
            _tmp = _curState.GetComponent<TMP_Text>();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            _tmp.text = GameData.GameDataManager.Instance.gameStatus switch
            {
                GameStatus.Default => "Default",
                GameStatus.UnitChosen => "UnitChosen",
                GameStatus.BlockSelected => "BlockSelected",
                GameStatus.Move => "Move",
                GameStatus.MenuAfterMove => "MenuAfterMove",
                GameStatus.FightMenu => "FightMenu",
                GameStatus.Fight => "Fight",
                _ => _tmp.text
            };

            _tmp.color = GameData.GameDataManager.Instance.gameStatus switch
            {
                GameStatus.Default => Color.black,
                GameStatus.UnitChosen => Color.blue,
                GameStatus.BlockSelected => Color.magenta,
                GameStatus.Move => Color.blue,
                GameStatus.MenuAfterMove => Color.black,
                GameStatus.FightMenu => Color.red,
                GameStatus.Fight => Color.red,
                _ => Color.white
            };
        }
    }
}