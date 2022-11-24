using GameData;
using UnityEngine;

namespace StateMachine.UnitAnimator
{
    public class AttackAnim : StateMachineBehaviour
    {
        private static readonly int attackFinished = Animator.StringToHash("attackFinished");
        
        // public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
        //     int layerIndex)
        // {
        //     
        // }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            GameDataManager.Instance.AttackAnimeFinished = true;
            if (GameDataManager.Instance.TakeDamageAnimeFinished)
            {
                Animator animator2 = GameManager.gameManager.GetComponent<Animator>();
                animator2.SetTrigger(attackFinished);
            }
        }

        // public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
        //     int layerIndex)
        // {
        // }
        //
        // public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo,
        //     int layerIndex)
        // {
        // }
        //
        // public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo,
        //     int layerIndex)
        // {
        // }
    }
}