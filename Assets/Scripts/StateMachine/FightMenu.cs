using GameData;
using GUI;
using Units;
using UnityEngine;

namespace StateMachine
{
    public class FightMenu : StateMachineBehaviour
    {
        private GameDataManager _gameData = GameDataManager.Instance;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _gameData.gameStatus = StateMachine.GameStatus.FightMenu;
            
            if (_gameData.ShouldAgentOperate())
            {
                _gameData.Agent.ClickEnemyToAttack();
            }
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

         // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
         public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        // OnStateMove is called right after Animator.OnAnimatorMove()
        // override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        // {
        //     MapManager.Instance.HighlightUnitAtkRangeExit(_highlightedUnit);
        // }

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }
}
