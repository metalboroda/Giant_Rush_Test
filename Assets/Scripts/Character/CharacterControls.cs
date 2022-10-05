using UnityEngine;

namespace Assets.Scripts.Character
{
    public class CharacterControls : MonoBehaviour
    {
        [SerializeField]
        private float touchMovementSpeed = 0.125f;
        [SerializeField]
        private float sideMovementSpeed = 8;
        [SerializeField]
        private float sideConstraints;

        [SerializeField]
        private ControlType controlType;

        private enum ControlType
        {
            Joystick,
            Touch
        }

        // Private refs
        private FloatingJoystick joystick;

        private void Awake()
        {
            joystick = FindObjectOfType<FloatingJoystick>();
        }

        private void Update()
        {
            switch (controlType)
            {
                case ControlType.Joystick:
                    JoystickMovement();
                    break;
                case ControlType.Touch:
                    TouchMovement();
                    break;
            }

            ConstraintHandle();
        }

        private void TouchMovement()
        {
            Touch touch;

            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * touchMovementSpeed,
                        transform.position.y,
                        transform.position.z);
                }
            }
        }

        private void JoystickMovement()
        {
            float xMovement = joystick.Horizontal;

            transform.position += new Vector3(xMovement, 0, 0) * sideMovementSpeed * Time.deltaTime;
        }

        private void ConstraintHandle()
        {
            var playerPos = new Vector3(Mathf.Clamp(transform.position.x, -sideConstraints, sideConstraints), transform.position.y, transform.position.z);

            transform.position = playerPos;
        }
    }
}