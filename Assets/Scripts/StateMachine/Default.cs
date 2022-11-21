using System.Collections.Generic;
using System.Linq;
using GameData;
using Units;
using UnityEngine;
using Util;

namespace StateMachine
{
    public class Default : StateMachineBehaviour
    {
        private GameDataManager gameData = GameDataManager.Instance;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            gameData.gameStatus = StateMachine.GameStatus.Default;
            if (gameData.CurrentPlayer == -1)
            {
                // 初始化游戏数据

                //  - 初始化各玩家棋子
                List<Unit> units = new();
                for (int playerNumber = 0; playerNumber < GameData.GameDataManager.PlayerNum; playerNumber++)
                {
                    foreach (Unit thisUnit in gameData.Players[playerNumber].UnitsList)
                    {
                        // 初始化所有棋子状态机相关属性
                        thisUnit.ofPlayer = playerNumber;
                        thisUnit.OnTurnEnd();
                        units.Add(thisUnit);
                        Debug.Log($"Player_{playerNumber.ToString()} has unit: {units.Last()}");
                    }
                }

                gameData.CurrentPlayer = 0;
                foreach (Unit unit in gameData.GetCurrentPlayer().UnitsList)
                {
                    // 设置正开始回合的所有单位 hasMoved, hasAttacked 属性为 False
                    unit.OnTurnBegin();
                }
            }

            if (gameData.GetCurrentPlayer().IsRobot)
            {
                // TODO: robot 操作here
                // TODO: 直接fast back to default
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
            
            HighlightBlockUtil.HighlightSelectableUnitOnBlocks();

            Debug.Log("Turn end, now player is: " + gameData.CurrentPlayer);
        }
    }
}