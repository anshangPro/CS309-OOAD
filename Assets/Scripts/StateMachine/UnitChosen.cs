using UnityEngine;
using GameData;
using GUI;
using Util;

namespace StateMachine
{
    public class UnitChosen : StateMachineBehaviour
    {
        private GameDataManager gameData = GameDataManager.Instance;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            gameData.gameStatus = GameStatus.UnitChosen;
            OverlayGridUtil.SetOverlayGridToNone(gameData.HighlightBlocks);
            gameData.HighlightBlocks.Clear();

            HighlightBlockUtil.HighlightSelectableUnitOnBlocks();

            gameData.Path.Clear();
            gameData.SelectedBlock = null;
            gameData.HighlightBlocks = MapManager.Instance.DisplayInRange(gameData.SelectedUnit);

            if (gameData.ShouldAgentOperate())
            {
                gameData.Agent.ClickBlockToMoveOn();
            }

            UIManager.Instance.CharacterUI.SetActive(true);
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            UIManager.Instance.CharacterUI.SetActive(false);
            HighlightBlockUtil.DeHighlightSelectableUnitOnBlocks();
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