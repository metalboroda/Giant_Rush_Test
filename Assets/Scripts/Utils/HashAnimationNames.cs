using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class HashAnimationNames
    {
        public int IdleHash = Animator.StringToHash("Idle");
        public int RunHash = Animator.StringToHash("Run");
        public int DeathHash = Animator.StringToHash("Death");
    }
}