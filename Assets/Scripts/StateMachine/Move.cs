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
        private static readonly int Running = Animator.StringToHash("running");
        private static readonly int MoveFinished = Animator.StringToHash("moveFinished");

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            HighlightBlockUtil.DeHighlightSelectableUnitOnBlocks();
            
            gameData.gameStatus = StateMachine.GameStatus.Move;
            selectedUnit = GameDataManager.Instance.SelectedUnit;
            selectedBlock = GameDataManager.Instance.SelectedBlock;
            //将可移动的方块清空,选中的方块清空
            OverlayGridUtil.SetOverlayGridToNone(gameData.HighlightBlocks);
            gameData.HighlightBlocks.Clear();
            gameData.SelectedBlock = null;
            selectedUnit.GetComponent<Animator>().SetBool(Running, true);
            GameDataManager.Instance.SelectedUnit.Moved();
            GameDataManager.Instance.MovedUnit = selectedUnit;
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (selectedUnit.onBlock == selectedBlock)
            {
                selectedBlock = null;
                GameManager.gameManager.GetComponent<Animator>().SetTrigger(MoveFinished);
            }
            else
            {
                selectedUnit.MoveAlongPath(GameData.GameDataManager.Instance.Path);
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            gameData.GetCurrentPlayer().UnitsList.ForEach(unit => unit.onBlock.standUnit = unit);
            selectedUnit.GetComponent<Animator>().SetBool(Running, false);
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