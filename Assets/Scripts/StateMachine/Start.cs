using GameData;
using GUI;
using UnityEngine;

namespace StateMachine
{
    public class Start : StateMachineBehaviour
    {
        private readonly GameDataManager _gameData = GameDataManager.Instance;
        private readonly UIManager _uiManager = UIManager.Instance;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _gameData.gameStatus = GameStatus.Start;
        }

        // public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        // {
        // }
    }
}