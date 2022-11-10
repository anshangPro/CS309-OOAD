using GameData;
using Interfaces;
using UnityEngine;
using Util;

namespace GUI.StateUI
{
    public class UIBlockSelected : StateMachineBehaviour
    {
        private readonly GameDataManager gameData = GameDataManager.Instance;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // 设置路径
            OverlayGridUtil.SetOverlayGridToWhite(gameData.Path);

            gameData.Path.Clear();

            if (gameData.SelectedUnit != null)
            {
                Block currentBlock = gameData.SelectedUnit.onBlock;
                gameData.Path =
                    MapManager.Instance.FindPath(currentBlock, gameData.SelectedBlock, gameData.HighlightBlocks);
                MapManager.Instance.DisplayAlongPath(gameData.Path);
            }
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        // public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        // {
        // }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        // public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        // {
        // }

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