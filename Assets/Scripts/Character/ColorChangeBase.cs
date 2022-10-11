using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class ColorChangeBase : MonoBehaviour
    {
        public ColorState colorState;

        [Header("")]
        [SerializeField]
        private List<Material> materialList = new List<Material>();

        [Header("")]
        [SerializeField]
        private Renderer rend;

        public virtual void SetColor(ColorState state)
        {
            switch (state)
            {
                case ColorState.ColorA:
                    colorState = ColorState.ColorA;
                    rend.material = materialList[0];
                    break;
                case ColorState.ColorB:
                    colorState = ColorState.ColorB;
                    rend.material = materialList[1];
                    break;
                default:
                    break;
            }
        }

        public virtual void SetColor(int colorIndex)
        {
            switch (colorIndex)
            {
                case 0:
                    colorState = ColorState.ColorA;
                    rend.material = materialList[0];
                    break;
                case 1:
                    colorState = ColorState.ColorB;
                    rend.material = materialList[1];
                    break;
                default:
                    break;
            }
        }
    }

    public enum ColorState
    {
        ColorA,
        ColorB
    }
}