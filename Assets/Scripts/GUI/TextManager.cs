using System;
using StateMachine;
using TMPro;
using UnityEngine;
using Random = System.Random;

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
                GameStatus.GameOver => "GameOver",
                _ => _tmp.text
            };

            var seed = Guid.NewGuid().GetHashCode();
            _tmp.color = GameData.GameDataManager.Instance.gameStatus switch
            {
                GameStatus.Default => Color.black,
                GameStatus.UnitChosen => Color.blue,
                GameStatus.BlockSelected => Color.magenta,
                GameStatus.Move => Color.blue,
                GameStatus.MenuAfterMove => Color.black,
                GameStatus.FightMenu => Color.red,
                GameStatus.Fight => Color.red,
                GameStatus.GameOver => Color.black,
                _ => Color.white
            };
        }
    }
}