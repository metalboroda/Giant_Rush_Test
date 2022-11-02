using UnityEngine;
using Assets.Scripts.Character;

namespace Assets.Scripts.Level
{
    [ExecuteInEditMode]
    public class PortalColor : ColorChangeBase
    {
        public bool needColorAtStart = true;

        private void Update()
        {
            // Make change it in editor
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