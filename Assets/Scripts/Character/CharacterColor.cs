using UnityEngine;

namespace Assets.Scripts.Character
{
    public class CharacterColor : MonoBehaviour
    {
        [SerializeField]
        private SkinnedMeshRenderer skinnedMeshRenderer;

        public CharacterColorState characterColorState;

        public enum CharacterColorState
        {
            ColorA,
            ColorB
        }

        public void SetCharacterColor(Material mat, CharacterColorState state)
        {
            skinnedMeshRenderer.material = mat;
            characterColorState = state;
        }
    }
}