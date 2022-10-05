using Assets.Scripts.Character;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public class Portal : MonoBehaviour
    {
        [SerializeField]
        private List<Material> materials = new List<Material>();

        [SerializeField]
        private MeshRenderer meshRenderer;

        public ColorState colorState;

        public enum ColorState
        {
            ColorA,
            ColorB
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SetPlayerColor(other);
            }
        }

        public void SetPortal(int portalIndex)
        {
            switch (portalIndex)
            {
                case 0:
                    colorState = ColorState.ColorA;
                    meshRenderer.material = materials[0];
                    break;
                case 1:
                    colorState = ColorState.ColorB;
                    meshRenderer.material = materials[1];
                    break;
                default:
                    break;
            }
        }

        private void SetPlayerColor(Collider other)
        {
            var charColor = other.GetComponent<CharacterColor>();

            switch (colorState)
            {
                case ColorState.ColorA:
                    charColor.SetCharacterColor(materials[0], CharacterColor.CharacterColorState.ColorA);
                    break;
                case ColorState.ColorB:
                    charColor.SetCharacterColor(materials[1], CharacterColor.CharacterColorState.ColorB);
                    break;
                default:
                    break;
            }
        }
    }
}