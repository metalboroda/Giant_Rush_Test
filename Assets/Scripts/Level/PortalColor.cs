using UnityEngine;
using Assets.Scripts.Character;

namespace Assets.Scripts.Level
{
    public class PortalColor : ColorChangeBase
    {
        public bool needColorAtStart = true;

        private void Awake()
        {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent<ColorChangeBase>();
        }

        private void Start()
        {
            if (needColorAtStart)
            {
                SetColor(colorState);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var playerController = other.GetComponent<PlayerController>();

                playerController.SetColor((int)colorState);
            }
        }
    }
}