using UnityEngine;
using GUI;
using Util;
using Units;
using GameData;

namespace StateMachine
{
    public class Move : StateMachineBehaviour
    {
        GameDataManager gameData = GameDataManager.Instance;
        Unit selectedUnit;
        Block selectedBlock;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            selectedUnit = GameDataManager.Instance.SelectedUnit;
            selectedBlock = GameDataManager.Instance.SelectedBlock;
            //将可移动的方块清空,选中的方块清空
            OverlayGridUtil.SetOverlayGridToNone(gameData.MovableBlocks);
            gameData.MovableBlocks.Clear();
            gameData.SelectedBlock = null;
            gameData.Path.ForEach(item => gameData.CopyPath.Add(item));
            selectedUnit.GetComponent<Animator>().SetBool("running", true);
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (selectedUnit.onBlock == selectedBlock)
            {
                GameManager.gameManager.GetComponent<Animator>().SetTrigger("moveFinished");
            }
            else
            {
                selectedUnit.MoveAlongPath(GameData.GameDataManager.Instance.Path);
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            UIManager.Instance.ShowMenuAfterMove();
            selectedUnit.GetComponent<Animator>().SetBool("running", false);
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