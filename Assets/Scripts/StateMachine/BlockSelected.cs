using UnityEngine;
using Util;
using GameData;

namespace StateMachine
{
    public class BlockSelected : StateMachineBehaviour
    {
        GameDataManager gameData = GameDataManager.Instance;
        
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            gameData.gameStatus = StateMachine.GameStatus.BlockSelected;
            
            if (gameData.RobotTest)
            {
                // TODO: RobotTest 需要换成 gameDta.GetCurrentPlayer().IsRobot
                gameData.agent.ConfirmBlockToMoveOn();
            }
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        // public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        // {
        // }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            gameData.SelectableUnitOnBlocks.ForEach(block => block.SetOverlayGridType(OverlayGrid.OverlayGridType.None));
            gameData.SelectableUnitOnBlocks.Clear();
        }

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