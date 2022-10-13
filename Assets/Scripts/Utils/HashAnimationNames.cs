using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class HashAnimationNames
    {
        public int IdleHash = Animator.StringToHash("Idle");
        public int RunHash = Animator.StringToHash("Run");
        public int DeathHash = Animator.StringToHash("Death");
        public int DeathHash2 = Animator.StringToHash("Death_2");
        public int DeathHash3 = Animator.StringToHash("Death_3");
        public int DeathHash4 = Animator.StringToHash("Death_4");
        public int FightIdleHash = Animator.StringToHash("Fight_Idle");
        public int PunchHash = Animator.StringToHash("Punch");
        public int PunchHash2 = Animator.StringToHash("Punch_2");
        public int PunchHash3 = Animator.StringToHash("Punch_3");
        public int PunchHash4 = Animator.StringToHash("Punch_4");
        public int ReceivePunchHash = Animator.StringToHash("Receive_Punch");
        public int ReceivePunchHash_2 = Animator.StringToHash("Receive_Punch_2");
    }
}