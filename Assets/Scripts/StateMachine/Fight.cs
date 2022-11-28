using GameData;
using UnityEngine;

namespace StateMachine
{
    public class Fight : StateMachineBehaviour
    {
        private GameDataManager gameData = GameDataManager.Instance;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            gameData.gameStatus = StateMachine.GameStatus.Fight;
            if (gameData.SelectedSkill is not null)
            {
                gameData.SelectedSkill.Skill.SkillUse(gameData.MovedUnit, gameData.SelectedEnemy);
            }
            gameData.MovedUnit.PlayAttackAnime();
            gameData.MovedUnit.Attack(gameData.SelectedEnemy);
            Debug.Log(gameData.MovedUnit + " Attack " + gameData.SelectedEnemy);
            gameData.MovedUnit.Attacked();
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            gameData.MovedUnit = null;
            gameData.SelectedEnemy = null;
            gameData.SelectedUnit = null;
            gameData.SelectedBlock = null;
            gameData.AttackAnimeFinished = false;
            gameData.TakeDamageAnimeFinished = false;
            if (gameData.IsGameOver())
            {
                gameData.GameOver();
            }
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