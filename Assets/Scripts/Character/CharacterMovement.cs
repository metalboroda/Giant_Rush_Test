using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField]
        private float movementSpeed = 0;

        // Private refs
        private CharacterAnimation characterAnimation;

        private void Awake()
        {
            characterAnimation = GetComponent<CharacterAnimation>();
        }

        private void Start()
        {
            Idle();
        }

        private void OnEnable()
        {
            GameHandler.OnGameStart += MoveForward;
        }

        private void OnDisable()
        {
            GameHandler.OnGameStart -= MoveForward;
        }

        private void Idle()
        {
            if (characterAnimation != null)
                characterAnimation.IdleAnimation();
        }

        private void MoveForward()
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

            if (characterAnimation != null)
                characterAnimation.RunAnimation();
        }
    }
}