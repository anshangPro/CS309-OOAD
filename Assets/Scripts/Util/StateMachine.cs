using UnityEngine;

namespace Util
{
    public static class StateMachine
    {
        /// <summary>
        /// 放回状态机当前的状态
        /// </summary>
        /// <param name="stateMachine"></param>
        /// <returns>String 状态</returns>
        public static string GetCurrentStatus(Animator stateMachine) // 获取当前执行的动画
        {
            AnimatorClipInfo[] mCurrentClipInfo = stateMachine.GetCurrentAnimatorClipInfo(0);
            return mCurrentClipInfo[0].clip.name;
        }
    }
}