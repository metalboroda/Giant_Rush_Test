using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class CharacterAnimation : MonoBehaviour
    {
        // Private comp
        private Animator animator;

        // Vars Once
        private bool idleOnce;
        private bool runOnce;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
        }

        private void OnEnable()
        {
            GameHandler.OnLose += IdleAnimation;
        }

        private void OnDisable()
        {
            GameHandler.OnLose -= IdleAnimation;
        }

        public void IdleAnimation()
        {
            runOnce = false;

            if (!idleOnce)
            {
                idleOnce = true;

                animator.SetTrigger("Idle");
            }
        }

        public void RunAnimation()
        {
            idleOnce = false;

            if (!runOnce)
            {
                runOnce = true;

                animator.SetTrigger("Run");
            }
        }
    }
}