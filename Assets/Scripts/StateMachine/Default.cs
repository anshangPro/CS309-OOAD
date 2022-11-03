using System.Collections.Generic;
using System.Linq;
using GameData;
using Units;
using UnityEngine;

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
                for(int playerNumber = 0; playerNumber < GameData.GameDataManager.PlayerNum; playerNumber++)
                {
                    GameObject[] unitObjects = GameObject.FindGameObjectsWithTag($"Player_{playerNumber}");
                    units.Clear();
                    foreach(GameObject unitObject in unitObjects)
                    {
                        Unit thisUnit = unitObject.GetComponent<Unit>();
                        // 初始化所有棋子状态机相关属性
                        thisUnit.ofPlayer = playerNumber;
                        thisUnit.OnTurnEnd();
                        units.Add(thisUnit);
                        Debug.Log($"Player_{playerNumber.ToString()} has unit: {units.Last().ToString()}");
                    }
                    gameData.UnitsOfPlayers[playerNumber] = new List<Unit>(units);
                }
            }

            if (gameData.CurrentPlayer != gameData.MainPlayer)
            {
                if (gameData.CurrentPlayer != -1)
                {
                    foreach (Unit unit in gameData.UnitsOfPlayers[gameData.CurrentPlayer])
                    {
                        // 设置已结束回合的所有单位 hasMoved, hasAttacked 属性为 True
                        unit.OnTurnEnd();
                    }
                }

                //交换玩家 **设置下一玩家，确认回合结束不在此处**
                // TODO 这里的转换逻辑是没做完的 by 周凡卜
                gameData.CurrentPlayer = gameData.MainPlayer;
                foreach (Unit unit in gameData.UnitsOfPlayers[gameData.CurrentPlayer])
                {
                    // 设置正开始回合的所有单位 hasMoved, hasAttacked 属性为 False
                    unit.OnTurnBegin();
                }

                Debug.Log("Turn end, now player is: " + gameData.CurrentPlayer);
            }
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }
}
