using System.Collections.Generic;
using System.Linq;
using GameData;
using GUI;
using Units;
using UnityEngine;
using Util;

namespace StateMachine
{
    public class Default : StateMachineBehaviour
    {
        private GameDataManager gameData = GameDataManager.Instance;
        private static readonly int RestartAnime = Animator.StringToHash("restart");


        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!gameData.Started)
            {
                animator.SetTrigger(RestartAnime);
                return;
            }
            
            UIManager.Instance.SetVisiableWithdrawButton(true);
            UIManager.Instance.SetVisiableSkipRoundButton(true);
            if (gameData.SelectedSkill is not null)
            {
                gameData.SelectedSkill.Skill.CancelEffect();
                gameData.SelectedSkill = null;
            }

            gameData.gameStatus = StateMachine.GameStatus.Default;
            if (gameData.CurrentPlayer == -1)
            {
                // 初始化游戏数据

                //  - 初始化各玩家棋子
                for (int playerNumber = 0; playerNumber < GameData.GameDataManager.PlayerNum; playerNumber++)
                {
                    foreach (Unit thisUnit in gameData.Players[playerNumber].UnitsList)
                    {
                        // 初始化所有棋子状态机相关属性
                        thisUnit.ofPlayer = playerNumber;
                    }
                }

                gameData.CurrentPlayer = 0;
                // foreach (Unit unit in gameData.GetCurrentPlayer().UnitsList)
                // {
                //     // 设置正开始回合的所有单位 hasMoved, hasAttacked 属性为 False
                //     unit.OnTurnBegin();
                // }
            }

            if (gameData.GetCurrentPlayer().TurnFinish())
            {
                foreach (Unit unit in gameData.GetCurrentPlayer().UnitsList)
                {
                    // 设置已结束回合的所有单位 hasMoved, hasAttacked 属性为 True
                    unit.OnTurnEnd();
                }

                //交换玩家 **设置下一玩家，确认回合结束不在此处**
                gameData.TurnRound();
                foreach (Unit unit in gameData.GetCurrentPlayer().UnitsList)
                {
                    Debug.Log($"Now is {gameData.GetCurrentPlayer()}'s turn");
                    // 设置正开始回合的所有单位 hasMoved, hasAttacked 属性为 False
                    unit.OnTurnBegin();
                }
            }

            gameData.TakeSnapshot();

            if (gameData.ShouldAgentOperate())
            {
                gameData.Agent.Think();
                gameData.Agent.ClickUnitToMove();
            }

            HighlightBlockUtil.HighlightSelectableUnitOnBlocks();

            Debug.Log("Turn end, now player is: " + gameData.CurrentPlayer);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            UIManager.Instance.SetVisiableWithdrawButton();
            UIManager.Instance.SetVisiableSkipRoundButton();
        }
    }
}