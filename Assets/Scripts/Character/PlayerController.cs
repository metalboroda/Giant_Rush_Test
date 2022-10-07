using Assets.Scripts.Managers;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private float movementSpeed = 5;
        [SerializeField]
        private float sideMovementSpeed = 8;
        [SerializeField]
        private float sideConstraints;

        // UniRx
        private CompositeDisposable _disposable = new CompositeDisposable();

        private void OnEnable()
        {
            GameManager.OnGameStateChanged += RxMovementHandle;
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChanged -= RxMovementHandle;

            _disposable.Clear();
        }

        private void RxMovementHandle(GameState state)
        {
            Observable.EveryUpdate().Subscribe(_ =>
            {
                if (state == GameState.Started)
                {
                    MoveForward();
                    JoystickSideMovementHandle();
                    ConstraintHandle();
                }
                else
                {
                    _disposable.Clear();
                }
            }
            ).AddTo(_disposable);
        }

        private void MoveForward()
        {
            if (GameManager.instance.State == GameState.Started)
            {
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            }
        }

        private void JoystickSideMovementHandle()
        {
            float xMovement = DynamicJoystick.instance.Horizontal;

            transform.position += new Vector3(xMovement, 0, 0) * sideMovementSpeed;
        }

        private void ConstraintHandle()
        {
            var playerPos = new Vector3(Mathf.Clamp(transform.position.x, -sideConstraints, sideConstraints), transform.position.y, transform.position.z);

            transform.position = playerPos;
        }
    }
}